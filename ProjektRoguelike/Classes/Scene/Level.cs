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
        /// The index of this level.
        /// </summary>
        public byte LevelIndex { get; set; }

        /// <summary>
        /// The current room.
        /// </summary>
        public Room CurrentRoom { get; set; }

        /// <summary>
        /// The next room.<br></br>
        /// Is used for the transition (?) if it's not null.
        /// </summary>
        public Room NextRoom { get; set; }

        /// <summary>
        /// Creates a new Level by the given level index.
        /// </summary>
        /// <param name="levelIndex">The index of the level you want to create.</param>
        public Level(byte levelIndex)
        {
            // Store the parameters.
            LevelIndex = levelIndex;

            //TESTTESTTESTROOM
            Room.CurrentLevel = this;
            _gameObjects.Add(new Room(0, Vector2.Zero));
            CurrentRoom = (Room)_gameObjects[0];

            //generate the level / rooms
            //......

            // Add the player.
            //_gameObjects.Add(new Player());
        }
    }
}
