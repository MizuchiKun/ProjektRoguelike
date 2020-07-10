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
        /// The position of the top-left corner of this <see cref="Room"/>.
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// The walls of this <see cref="Room"/>.<br></br>
        /// The first index specifies the direction (0=top, 1=right, 2=bottom, 3=left).
        /// </summary>
        public Tile[,] Walls = new Tile[4, 13];

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
            Vector2 dimensions = new Vector2(15, 9);
            // The corners.
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + new Vector2(dimensions.X - 1, 0) * Tile.Size,
                                      effect: SpriteEffects.FlipHorizontally));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + (dimensions - Vector2.One) * Tile.Size,
                                      effect: SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + new Vector2(0, dimensions.Y - 1) * Tile.Size,
                                      effect: SpriteEffects.FlipVertically));
            // The walls.
            for (byte x = 1; x < dimensions.X - 1; x++)
            {
                // Top.
                Tile topWall = new Tile(wall, 
                                        topLeftCorner + new Vector2(x, 0) * Tile.Size,
                                        rotation: 0f);
                Walls[0, x-1] = topWall;
                _gameObjects.Add(topWall);

                // Bottom.
                Tile bottomWall = new Tile(wall,
                                           topLeftCorner + new Vector2(x, dimensions.Y - 1) * Tile.Size,
                                           rotation: 180f);
                Walls[2, x-1] = bottomWall;
                _gameObjects.Add(bottomWall);
            }
            for (byte y = 1; y < dimensions.Y - 1; y++)
            {
                // Left.
                Tile leftWall = new Tile(wall,
                                         topLeftCorner + new Vector2(0, y) * Tile.Size,
                                         rotation: -90f);
                Walls[3, y - 1] = leftWall;
                _gameObjects.Add(leftWall);

                // Right.
                Tile rightWall = new Tile(wall,
                                          topLeftCorner + new Vector2(dimensions.X - 1, y) * Tile.Size,
                                          rotation: 90f);
                Walls[1, y - 1] = rightWall;
                _gameObjects.Add(rightWall);
            }
            // The ground.
            for (byte x = 1; x < dimensions.X - 1; x++)
            {
                for (byte y = 1; y < dimensions.Y - 1; y++)
                {
                    _gameObjects.Add(new Tile(ground,
                                              topLeftCorner + new Vector2(x, y) * Tile.Size));
                }
            }

            //load room from file...
            //...
        }
    }
}
