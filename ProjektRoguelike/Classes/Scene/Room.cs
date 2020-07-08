using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A room of a level.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// This Room's parent Level.
        /// </summary>
        private Level _parentLevel;

        /// <summary>
        /// The room dimensions, relative to a standard room.
        /// </summary>
        private Vector2 _roomDimensions;

        /// <summary>
        /// Creates a new <see cref="Room"/> with the given dimensions.
        /// </summary>
        /// <param name="parentLevel">The <see cref="Level"/> this <see cref="Room"/> belongs to.</param>
        /// <param name="roomDimensions">
        /// The dimensions of the <see cref="Room"/>, relative to a standard room.<br></br>
        /// (2, 1) would be twice as wide as a standard room.
        /// </param>
        public Room(Level parentLevel, Vector2 roomDimensions)
        {
            // Store the parameters.
            _parentLevel = parentLevel;
            _roomDimensions = roomDimensions;
        }
    }
}
