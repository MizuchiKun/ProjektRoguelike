using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// The player.
    /// </summary>
    public class Player : Entity
    {
        /// <summary>
        /// The walking animations of the Player.
        /// </summary>
        //private static Animation[] _walkingAnimations;

        /// <summary>
        /// Creates a Player with the given graphical parameters.
        /// </summary>
        /// <param name="texture">Its texture.</param>
        /// <param name="position">Its position. <br></br>If null, it will be <see cref="Vector2.Zero"/>.</param>
        /// <param name="sourceRectangle">Its source rectangle. <br></br>If null, the whole texture will be drawn.</param>
        /// <param name="layerDepth">Its layer depth. <br></br>It's 0 by default.</param>
        /// <param name="effect">Its sprite effect. <br></br>It's <see cref="SpriteEffects.None"/> by default.</param>
        public Player(Vector2? position = null)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_front"),
               position: position,
               sourceRectangle: new Rectangle(0, 0, 256, 256))
        { }

        public override void Update()
        {
            // Handle movement input.
            // The movement speed.
            short speed = 200;
            // How close you have to be to a door to be able to walk through it.
            byte doorPadding = 25;
            // Up.
            if (Globals.GetKey(Keys.W))
            {
                // Move this Sprite.
                Position += new Vector2(0, -speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds);

                // If it collides with the current top door.
                if (Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) <= doorPadding * Scale.X)
                {
                    // It's allowed to move up.
                }
                // Else if it collides with the top wall or another Entity.
                else if (Collides(Level.CurrentRoom.Walls[0])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[1]) || Collides(Level.CurrentRoom.Doors[3]))
                             && Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) > doorPadding * Scale.Y))
                {
                    // It's not allowed to move up.
                    // Revert its position.
                    Position -= new Vector2(0, -speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds);
                }
            }

            // Right.
            if (Globals.GetKey(Keys.D))
            {
                // Move this Sprite.
                Position += new Vector2(speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds, 0);

                // If it collides with the current right door.
                if (Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) <= doorPadding * Scale.Y)
                {
                    // It's allowed to move right.
                }
                // Else if it collides with the right wall or another Entity.
                else if (Collides(Level.CurrentRoom.Walls[1])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[0]) || Collides(Level.CurrentRoom.Doors[2]))
                             && Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) > doorPadding * Scale.X))
                {
                    // It's not allowed to move right.
                    // Revert its position.
                    Position -= new Vector2(speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds, 0);
                }
            }

            // Down.
            if (Globals.GetKey(Keys.S))
            {
                // Move this Sprite.
                Position += new Vector2(0, speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds);

                // If it collides with the current bottom door.
                if (Math.Abs(Position.X - Level.CurrentRoom.Doors[2].Position.X) <= doorPadding * Scale.X)
                {
                    // It's allowed to move down.
                }
                // Else if it collides with the bottom wall or another Entity.
                else if (Collides(Level.CurrentRoom.Walls[2])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[1]) || Collides(Level.CurrentRoom.Doors[3]))
                             && Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) > doorPadding * Scale.Y))
                {
                    // It's not allowed to move down.
                    // Revert its position.
                    Position -= new Vector2(0, speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds);
                }
            }

            // Left.
            if (Globals.GetKey(Keys.A))
            {
                // Move this Sprite.
                Position += new Vector2(-speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds, 0);

                // If it collides with the current left door.
                if (Math.Abs(Position.Y - Level.CurrentRoom.Doors[3].Position.Y) <= doorPadding * Scale.Y)
                {
                    // It's allowed to move left.
                }
                // Else if it collides with the left wall or another Entity.
                else if (Collides(Level.CurrentRoom.Walls[3])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[0]) || Collides(Level.CurrentRoom.Doors[2]))
                             && Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) > doorPadding * Scale.X))
                {
                    // It's not allowed to move left.
                    // Revert its position.
                    Position -= new Vector2(-speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds, 0);
                }
            }
        }
    }
}
