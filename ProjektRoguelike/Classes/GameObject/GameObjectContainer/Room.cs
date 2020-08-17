using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Contains the different kinds of rooms.
    /// </summary>
    public enum RoomKind : byte
    {
        Start,
        Normal,
        Hidden,
        //Arcade,
        Boss
    }

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
        public Vector2 Position { get; set; }

        /// <summary>
        /// The position of the top-left corner.
        /// </summary>
        private Vector2 _topLeftCorner;

        /// <summary>
        /// The kind of this <see cref="Room"/>.
        /// </summary>
        public RoomKind Kind { get; }

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
        /// Creates a new room by the given paramters.
        /// </summary>
        /// <param name="roomIndex">The room's index which is used to load the room.</param>
        /// <param name="position">The room's grid position in the level.</param>
        /// <param name="kind">The kind of the door.</param>
        public Room(byte roomIndex, Vector2 gridPosition, RoomKind kind)
        {
            // Store the parameters.
            Position = gridPosition * Globals.WindowDimensions;
            Kind = kind;

            // Load the Textures.
            Texture2D ground = Globals.Content.Load<Texture2D>("Sprites/Environment/Boden");
            Texture2D wall = Globals.Content.Load<Texture2D>("Sprites/Environment/Wand");
            Texture2D corner = Globals.Content.Load<Texture2D>("Sprites/Environment/Ecke");

            // Add the room background.
            _topLeftCorner = Position;
            _topLeftCorner += new Vector2(1, 0.5f) * Tile.Size * Globals.Scale;
            // The corners.
            _gameObjects.Add(new Tile(corner,
                                      _topLeftCorner));
            _gameObjects.Add(new Tile(corner,
                                      _topLeftCorner + new Vector2(Dimensions.X - 1, 0) * Tile.Size * Globals.Scale,
                                      rotation: 90f));
            _gameObjects.Add(new Tile(corner,
                                      _topLeftCorner + (Dimensions - Vector2.One) * Tile.Size * Globals.Scale,
                                      rotation: 180f));
            _gameObjects.Add(new Tile(corner,
                                      _topLeftCorner + new Vector2(0, Dimensions.Y - 1) * Tile.Size * Globals.Scale,
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
                                        _topLeftCorner + new Vector2(x, 0) * Tile.Size * Globals.Scale,
                                        rotation: 0f);
                Walls[0][x - 1] = topWall;

                // Bottom.
                Tile bottomWall = new Tile(wall,
                                           _topLeftCorner + new Vector2(x, Dimensions.Y - 1) * Tile.Size * Globals.Scale,
                                           rotation: 180f);
                Walls[2][x - 1] = bottomWall;
            }
            // Left and right.
            for (byte y = 1; y < Dimensions.Y - 1; y++)
            {
                // Left.
                Tile leftWall = new Tile(wall,
                                         _topLeftCorner + new Vector2(0, y) * Tile.Size * Globals.Scale,
                                         rotation: -90f);
                Walls[3][y - 1] = leftWall;

                // Right.
                Tile rightWall = new Tile(wall,
                                          _topLeftCorner + new Vector2(Dimensions.X - 1, y) * Tile.Size * Globals.Scale,
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
                                              _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale));
                }
            }

            // Add the room's content.
            switch (Kind)
            {
                case RoomKind.Start:
                    // Add controls instructions in the centre of the room.
                    Texture2D controlsInstructions = Globals.Content.Load<Texture2D>("Sprites/Misc/ControlsInstructions");
                    _gameObjects.Add(new Sprite(texture: controlsInstructions,
                                                position: Position + (Dimensions / 2 + new Vector2(0.5f, 0)) * Tile.Size * Globals.Scale,
                                                scale: new Vector2(5) * (Tile.Size / controlsInstructions.Bounds.Size.ToVector2()) * Globals.Scale,
                                                layerDepth: 0.99999f));
                    break;
                case RoomKind.Normal:
                    // Load the content from a file by using the roomIndex.
                    _gameObjects.AddRange(RoomfileToList(RoomKind.Normal, roomIndex));
                    break;
                case RoomKind.Hidden:
                    // Load the content from a file by using the roomIndex.
                    _gameObjects.AddRange(RoomfileToList(RoomKind.Hidden, roomIndex));
                    break;
                case RoomKind.Boss:
                    // Load the content from a file by using the roomIndex.
                    _gameObjects.AddRange(RoomfileToList(RoomKind.Boss, roomIndex));
                    break;
            }
        }

        /// <summary>
        /// A method to add a <see cref="GameObject"/> to this <see cref="Room"/>.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject"/> that will be added.</param>
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

        /// <summary>
        /// Converts the .room file of the given <see cref="RoomKind"/> and number to a List of <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="roomKind">The given room kind.</param>
        /// <param name="roomIndex">The given room index</param>
        /// <returns>A List of all GameObjects, that are specified in the .room file.</returns>
        private List<GameObject> RoomfileToList(RoomKind roomKind, byte roomIndex)
        {
            // The list of GameObjects.
            List<GameObject> gameObjects = new List<GameObject>();

            // Get the lines of the file.
            string[] lines = File.ReadAllLines($"..\\..\\..\\..\\Content\\Rooms\\{roomKind}\\{roomKind.ToString().ToLower()}_{roomIndex}.room");

            // Go through all lines.
            for (byte y = 0; y < 9; y++)
            {
                // Split the line in substrings at every space (' ').
                string[] splittedLine = lines[y].Split(' ');

                // Get and add all GameObjects of this line.
                byte index, metadata;
                GameObject loadedGameObject = null;
                for (byte x = 0; x < 15; x++)
                {
                    // Get the index and metadata.
                    // If the metadata is defined.
                    if (splittedLine[x].Contains(":"))
                    {
                        // Split the string again.
                        string[] values = splittedLine[x].Split(':');

                        // Get the index and metadata.
                        index = Byte.Parse(values[0]);
                        metadata = Byte.Parse(values[1]);
                    }
                    // Else the metadata isn't defined.
                    else
                    {
                        // Get the index and set the metadata to 0.
                        index = Byte.Parse(splittedLine[x]);
                        metadata = 0;
                    }

                    // Get the corresponding GameObject.
                    switch (index)
                    {
                        // Nothing.
                        case 0:
                            // Add nothing.
                            break;
                        // Enemy.
                        case 1:
                            switch (metadata)
                            {
                                // Exploder.
                                case 0:
                                    loadedGameObject = new Exploder(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Floater.
                                case 1:
                                    loadedGameObject = new Floater(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Fly.
                                case 2:
                                    loadedGameObject = new Fly(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Flyboss.
                                case 3:
                                    loadedGameObject = new Flyboss(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Flytrap.
                                case 4:
                                    loadedGameObject = new Flytrap(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Screamer.
                                case 5:
                                    loadedGameObject = new Screamer(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                            }
                            break;
                        // Environment.
                        case 2:
                            switch (metadata)
                            {
                                // Campfire.
                                case 0:
                                    loadedGameObject = new Campfire(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Chest.
                                case 1:
                                    loadedGameObject = new Chest(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Hole.
                                case 2:
                                    loadedGameObject = new Hole(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Poop.
                                case 3:
                                    loadedGameObject = new Poop(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Pot.
                                case 4:
                                    loadedGameObject = new Pot(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Rock.
                                case 5:
                                    loadedGameObject = new Rock(_topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                            }
                            break;
                        // Itemstone.
                        case 3:
                            switch (metadata)
                            {
                                // Bomb.
                                case 0:
                                    loadedGameObject = new Itemstone(new PickupBomb(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Coin.
                                case 1:
                                    loadedGameObject = new Itemstone(new PickupCoin(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Heart.
                                case 2:
                                    loadedGameObject = new Itemstone(new PickupHeart(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Key.
                                case 3:
                                    loadedGameObject = new Itemstone(new PickupKey(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Poopsicle.
                                case 4:
                                    loadedGameObject = new Itemstone(new Poopsicle(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Shroom.
                                case 5:
                                    loadedGameObject = new Itemstone(new Shroom(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                                // Syringe.
                                case 6:
                                    loadedGameObject = new Itemstone(new Syringe(), _topLeftCorner + new Vector2(x, y) * Tile.Size * Globals.Scale);
                                    break;
                            }
                            break;
                    }

                    // Add the GameObject to gameObjects.
                    if (loadedGameObject != null)
                    {
                        gameObjects.Add(loadedGameObject);
                    }
                }
            }

            // Return the GameObject List.
            return gameObjects;
        }
    }
}