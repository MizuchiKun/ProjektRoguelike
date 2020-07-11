using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A room of a <see cref="Level"/>.
    /// </summary>
    public class Room : GameObjectContainer
    {
        /// <summary>
        /// The currently active <see cref="Level"/>.
        /// </summary>
        public static Level CurrentLevel { get; set; }

        /// <summary>
        /// The dimensions of a room (measured in sprites).
        /// </summary>
        public static Vector2 Dimensions { get; } = new Vector2(15, 9);

        /// <summary>
        /// The position of the top-left corner of this <see cref="Room"/>.
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// The walls of this <see cref="Room"/>.<br></br>
        /// The first index specifies the direction (0=top, 1=right, 2=bottom, 3=left).
        /// </summary>
        public Tile[][] Walls { get; } = new Tile[4][];

        /// <summary>
        /// The doors of this <see cref="Room"/>.<br></br>
        /// The index specifies the direction (0=top, 1=right, 2=bottom, 3=left).
        /// </summary>
        public Door[] Doors { get; } = new Door[4];

        /// <summary>
        /// The entities of this <see cref="Room"/>.
        /// </summary>
        public List<Entity> Entities { get; } = new List<Entity>();

        /// <summary>
        /// Creates a new room by the given room index.
        /// </summary>
        /// <param name="roomIndex">The room's index.</param>
        public Room(byte roomIndex, Vector2 position)
        {
            // Store the parameters.
            _position = position;

            // Load the Textures.
            Texture2D ground = Globals.Content.Load<Texture2D>("Sprites/Environment/Boden");
            Texture2D wall = Globals.Content.Load<Texture2D>("Sprites/Environment/Wand");
            Texture2D corner = Globals.Content.Load<Texture2D>("Sprites/Environment/Ecke");

            // Add the room background.
            Vector2 topLeftCorner = position;
            topLeftCorner += new Vector2(1, 0.5f) * Tile.Size;
            topLeftCorner.Y -= Dimensions.Y * Tile.Size.Y;
            // The corners.
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + new Vector2(Dimensions.X - 1, 0) * Tile.Size,
                                      rotation: 90f));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + (Dimensions - Vector2.One) * Tile.Size,
                                      rotation: 180f));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + new Vector2(0, Dimensions.Y - 1) * Tile.Size,
                                      rotation: -90f));
            // The walls.
            // Initialize the Walls arrays.
            Walls[0] = new Tile[(int)Dimensions.X - 2];
            Walls[1] = new Tile[(int)Dimensions.Y - 2];
            Walls[2] = new Tile[(int)Dimensions.X - 2];
            Walls[3] = new Tile[(int)Dimensions.Y - 2];
            // Top and bottom.
            for (byte x = 1; x < Dimensions.X - 1; x++)
            {
                // Top.
                Tile topWall = new Tile(wall, 
                                        topLeftCorner + new Vector2(x, 0) * Tile.Size,
                                        rotation: 0f);
                Walls[0][x-1] = topWall;

                // Bottom.
                Tile bottomWall = new Tile(wall,
                                           topLeftCorner + new Vector2(x, Dimensions.Y - 1) * Tile.Size,
                                           rotation: 180f);
                Walls[2][x-1] = bottomWall;
            }
            // Left and right.
            for (byte y = 1; y < Dimensions.Y - 1; y++)
            {
                // Left.
                Tile leftWall = new Tile(wall,
                                         topLeftCorner + new Vector2(0, y) * Tile.Size,
                                         rotation: -90f);
                Walls[3][y - 1] = leftWall;

                // Right.
                Tile rightWall = new Tile(wall,
                                          topLeftCorner + new Vector2(Dimensions.X - 1, y) * Tile.Size,
                                          rotation: 90f);
                Walls[1][y - 1] = rightWall;
            }
            _gameObjects.AddRange(Walls[0]);
            _gameObjects.AddRange(Walls[1]);
            _gameObjects.AddRange(Walls[2]);
            _gameObjects.AddRange(Walls[3]);
            // The ground.
            for (byte x = 1; x < Dimensions.X - 1; x++)
            {
                for (byte y = 1; y < Dimensions.Y - 1; y++)
                {
                    _gameObjects.Add(new Tile(ground,
                                              topLeftCorner + new Vector2(x, y) * Tile.Size));
                }
            }

            //add the doors based on the 2D Room array of the current level
            //...
            //TESTTESTTESTDOORS
            Doors[0] = new Door(position: topLeftCorner + new Vector2((Dimensions.X - 1) / 2, 0) * Tile.Size,
                                direction: 0,
                                null);
            Doors[1] = new Door(position: topLeftCorner + new Vector2(Dimensions.X - 1, (Dimensions.Y - 1) / 2) * Tile.Size,
                                direction: 1,
                                null);
            Doors[2] = new Door(position: topLeftCorner + new Vector2((Dimensions.X - 1) / 2, Dimensions.Y - 1) * Tile.Size,
                                direction: 2,
                                null);
            Doors[3] = new Door(position: topLeftCorner + new Vector2(0, (Dimensions.Y - 1) / 2) * Tile.Size,
                                direction: 3,
                                null);
            _gameObjects.AddRange(Doors);

            //load room from file...
            //...
        }
    }
}
