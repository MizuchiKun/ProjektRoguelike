using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A Door to another <see cref="Room"/>.
    /// </summary>
    public class Door : Sprite
    {
        /// <summary>
        /// The <see cref="Room"/> that lies behind this door.
        /// </summary>
        private Room _room;

        /// <summary>
        /// Creates a new <see cref="Door"/> with the given position, rotation and <see cref="Room"/>.
        /// </summary>
        /// <param name="position">The position of the <see cref="Door"/>.</param>
        /// <param name="direction">
        /// The direction in which the <see cref="Door"/> leads.<br></br>
        /// (0=up, 1=right, 2=down, 3=left)
        /// </param>
        /// <param name="roomBehindDoor">The <see cref="Room"/> that lies behind this <see cref="Door"/>.</param>
        public Door(Vector2 position,
                    byte direction,
                    Room roomBehindDoor)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Environment/Doorsheet"),
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: new Rectangle(new Point(0), new Point(256)),
               rotation: direction * 90f,
               scale: Tile.Size / new Vector2(256),
               layerDepth: 0.95f)
        {
            // Store the parameters.
            _room = roomBehindDoor;
        }

        public override void Update()
        {
            Console.WriteLine($"Door.cs: Update() is not implemented yet!");
        }
    }
}
