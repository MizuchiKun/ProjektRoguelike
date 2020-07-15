using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A level.
    /// </summary>
    public class Level : Scene
    {
        /// <summary>
        /// The generated <see cref="Room"/>s of this Level.
        /// </summary>
        private Room[,] _rooms = new Room[8, 8];

        /// <summary>
        /// The current room.
        /// </summary>
        public static Room CurrentRoom { get; set; }

        /// <summary>
        /// The <see cref="Player"/> of all levels.
        /// </summary>
        public static Player Player { get; } = new Player(Vector2.Zero + (Room.Dimensions / 2 + new Vector2(0.5f, -Room.Dimensions.Y)) * Tile.Size);

        /// <summary>
        /// Creates a new Level by the given level index.
        /// </summary>
        /// <param name="levelIndex">The index of the level you want to create.</param>
        public Level(byte levelIndex)
        {
            //TESTTESTTESTROOM
            Camera.Position += new Vector2(0f, 1f) * (Room.Dimensions * Tile.Size);
            _gameObjects.Add(new Room(0, Vector2.Zero));
            CurrentRoom = (Room)_gameObjects[0];

            //generate the level / rooms based on levelIndex
            //......
                    //TESTTESTLEVEL
                    for (byte i = 0; i < 9; i++)
                    {
                        Vector2 position = new Vector2((i / 3), (i % 3));
                        _rooms[(int)position.X, (int)position.Y] = new Room(i, position * Globals.WindowDimensions);
                    }
                    //GO THROUGH ARRAY AND ADD DOORS
                    for (byte x = 0; x < _rooms.GetLength(0); x++)
                    {
                        for (byte y = 0; y < _rooms.GetLength(1); y++)
                        {
                            // ADD DOORS IN DIRECTIONS WHERE THERE'S A ROOM
                            //.............
                        }
                    }

            // Add the player.
            _gameObjects.Add(Player);

            // Add enemies to test.
            CurrentRoom.Add(new Floater(Vector2.Zero + (Room.Dimensions / 5 + new Vector2(0.5f, -Room.Dimensions.Y)) * Tile.Size));
            CurrentRoom.Add(new Screamer(Vector2.Zero + (Room.Dimensions / 7 + new Vector2(0.5f, -Room.Dimensions.Y)) * Tile.Size));
        }
    }
}
