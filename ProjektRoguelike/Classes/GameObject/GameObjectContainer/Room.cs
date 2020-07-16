using System;
using System.Collections.Generic;
using System.Text;
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
        /// The dimensions of a room (measured in sprites).
        /// </summary>
        public static Vector2 Dimensions { get; } = new Vector2(15, 9);

        /// <summary>
        /// The position of the top-left corner of this <see cref="Room"/>.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// The walls of this <see cref="Room"/>.<br></br>
        /// The first index specifies the direction (0=top, 1=right, 2=bottom, 3=left).
        /// </summary>
        public Tile[][] Walls { get; } = new Tile[4][];

        /// <summary>
        /// The doors of this <see cref="Room"/>.<br></br>
        /// The index specifies the <see cref="Door"/>'s <see cref="Directions"/>.
        /// </summary>
        public Door[] Doors
        {
            get
            {
                return _doors;
            }
            set
            {
                // Store the value in _doors.
                for (byte i = 0; i < value.Length; i++)
                {
                    _doors[i] = value[i];
                }
                
                // Remove all Doors from _gameObjects.
                for (ushort i = 0; i < _gameObjects.Count; i++)
                {
                    if (_gameObjects[i].GetType().IsSubclassOf(typeof(Door)))
                    {
                        _gameObjects.Remove(_gameObjects[i]);
                    }
                }

                // Add the new Doors.
                for (byte i = 0; i < _doors.Length; i++)
                {
                    if (_doors[i] != null)
                    {
                        _gameObjects.Add(_doors[i]);
                    }
                }
            }
        }
        /// <summary>
        /// The doors of this room.
        /// </summary>
        private Door[] _doors = new Door[4];

        /// <summary>
        /// The entities of this <see cref="Room"/>.
        /// </summary>
        public List<Entity> Entities
        {
            get
            {
                // The entities.
                List<Entity> entities = new List<Entity>();

                // Add all Entities to entities.
                foreach (GameObject gameObject in _gameObjects)
                {
                    if (gameObject.GetType().IsSubclassOf(typeof(Entity)))
                    {
                        entities.Add((Entity)gameObject);
                    }
                }

                // Return the entities.
                return entities;
            }
        }

        /// <summary>
        /// The enemies of this <see cref="Room"/>.
        /// </summary>
        public List<Enemy> Enemies
        {
            get
            {
                // The enemies.
                List<Enemy> enemies = new List<Enemy>();

                // Add all Enemies to enemies.
                foreach (GameObject gameObject in _gameObjects)
                {
                    if (gameObject.GetType().IsSubclassOf(typeof(Enemy)))
                    {
                        enemies.Add((Enemy)gameObject);
                    }
                }

                // Return the enemies.
                return enemies;
            }
        }

        /// <summary>
        /// Creates a new room by the given room index.
        /// </summary>
        /// <param name="roomIndex">The room's index.</param>
        public Room(byte roomIndex, Vector2 position)
        {
            // Store the parameters.
            Position = position;

            // Load the Textures.
            Texture2D ground = Globals.Content.Load<Texture2D>("Sprites/Environment/Boden");
            Texture2D wall = Globals.Content.Load<Texture2D>("Sprites/Environment/Wand");
            Texture2D corner = Globals.Content.Load<Texture2D>("Sprites/Environment/Ecke");

            // Add the room background.
            Vector2 topLeftCorner = position;
            topLeftCorner += new Vector2(1, 0.5f) * Tile.Size * Globals.Scale;
            // The corners.
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + new Vector2(Dimensions.X - 1, 0) * Tile.Size * Globals.Scale,
                                      rotation: 90f));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + (Dimensions - Vector2.One) * Tile.Size * Globals.Scale,
                                      rotation: 180f));
            _gameObjects.Add(new Tile(corner, 
                                      topLeftCorner + new Vector2(0, Dimensions.Y - 1) * Tile.Size * Globals.Scale,
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
                                        topLeftCorner + new Vector2(x, 0) * Tile.Size * Globals.Scale,
                                        rotation: 0f);
                Walls[0][x-1] = topWall;

                // Bottom.
                Tile bottomWall = new Tile(wall,
                                           topLeftCorner + new Vector2(x, Dimensions.Y - 1) * Tile.Size * Globals.Scale,
                                           rotation: 180f);
                Walls[2][x-1] = bottomWall;
            }
            // Left and right.
            for (byte y = 1; y < Dimensions.Y - 1; y++)
            {
                // Left.
                Tile leftWall = new Tile(wall,
                                         topLeftCorner + new Vector2(0, y) * Tile.Size * Globals.Scale,
                                         rotation: -90f);
                Walls[3][y - 1] = leftWall;

                // Right.
                Tile rightWall = new Tile(wall,
                                          topLeftCorner + new Vector2(Dimensions.X - 1, y) * Tile.Size * Globals.Scale,
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
                                              topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale));
                }
            }

            //load room from file...
            //...
                        //TESTROOMINDICATOR
                        string roomPos = "";
                        switch (roomIndex)
                        {
                            case 0:
                                roomPos = "Top-left";
                                break;
                            case 1:
                                roomPos = "Top";
                                break;
                            case 2:
                                roomPos = "Top-right";
                                break;
                            case 3:
                                roomPos = "Left";
                                break;
                            case 4:
                                roomPos = "Centre";
                                break;
                            case 5:
                                roomPos = "Right";
                                break;
                            case 6:
                                roomPos = "Bottom-left";
                                break;
                            case 7:
                                roomPos = "Bottom";
                                break;
                            case 8:
                                roomPos = "Bottom-right";
                                break;
                        }
                        _gameObjects.Add(new Text(Globals.Content.Load<SpriteFont>("Fonts/Consolas24"),
                                                  new StringBuilder(roomPos),
                                                  Position + (Room.Dimensions / 2 + new Vector2(0.5f, 0)) * Tile.Size * Globals.Scale,
                                                  new Vector2(0.5f),
                                                  layerDepth: 0.999f));
        }

        /// <summary>
        /// A <b>temporary method</b> to add a GameObject to this Room.<br></br>
        /// <b>Will be replaced by the level generation.</b>
        /// </summary>
        /// <param name="gameObject">The GameObject that will be added.</param>
        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        /// <summary>
        /// Removes the given <see cref="GameObject"/> from this Room.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> that will be removed.</param>
        public void Remove(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }
    }
}
