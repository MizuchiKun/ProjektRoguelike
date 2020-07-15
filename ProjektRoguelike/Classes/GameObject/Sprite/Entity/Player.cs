﻿using System;
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
        McTimer timer;

        /// <summary>
        /// The walking animations of the Player.<br></br>
        /// (0=up, 1=right, 2=down, 3=left)
        /// </summary>
        private static Animation[] _walkingAnimations =
        {
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_up"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(200)),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_right"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(200)),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_down"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(200)),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_left"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(200))
        };
        /// <summary>
        /// The <see cref="Player"/>'s current animation.
        /// </summary>
        private Animation _currentAnimation = _walkingAnimations[2];

        /// <summary>
        /// Creates a Player with the given graphical parameters.
        /// </summary>
        /// <param name="texture">Its texture.</param>
        /// <param name="position">Its position. <br></br>If null, it will be <see cref="Vector2.Zero"/>.</param>
        /// <param name="sourceRectangle">Its source rectangle. <br></br>If null, the whole texture will be drawn.</param>
        /// <param name="layerDepth">Its layer depth. <br></br>It's 0 by default.</param>
        /// <param name="effect">Its sprite effect. <br></br>It's <see cref="SpriteEffects.None"/> by default.</param>
        public Player(Vector2? position = null)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_down"),
               position: position,
               sourceRectangle: new Rectangle(0, 0, 256, 256))
        { timer = new McTimer(600, true); }

        public override void Update()
        {
            // Handle movement input.
            // The movement speed.
            ushort speed = 200;
            // The velocity.
            Vector2 velocity = Vector2.Zero;
            // Up.
            if (Globals.GetKeyDown(Keys.W))
            {
                // Choose the "walking up" animation.
                _currentAnimation = _walkingAnimations[0];
            }
            if (Globals.GetKey(Keys.W))
            {
                // Move it up.
                velocity += -Vector2.UnitY * speed;
            }
            // Right.
            if (Globals.GetKeyDown(Keys.D))
            {
                // Choose the "walking right" animation.
                _currentAnimation = _walkingAnimations[1];
            }
            if (Globals.GetKey(Keys.D))
            {
                // Move it right.
                velocity += Vector2.UnitX * speed;
            }
            // Down.
            if (Globals.GetKeyDown(Keys.S))
            {
                // Choose the "walking down" animation.
                _currentAnimation = _walkingAnimations[2];
            }
            if (Globals.GetKey(Keys.S))
            {
                // Move it down.
                velocity += Vector2.UnitY * speed;
            }
            // Left.
            if (Globals.GetKeyDown(Keys.A))
            {
                // Choose the "walking left" animation.
                _currentAnimation = _walkingAnimations[3];
            }
            if (Globals.GetKey(Keys.A))
            {
                // Move it left.
                velocity += -Vector2.UnitX * speed;
            }
            // Move it.
            if (velocity != Vector2.Zero)
            {
                _currentAnimation.Resume();
                Move(Globals.DegreesToVector2(Globals.Vector2ToDegrees(velocity)) * speed);
            }
            else
            {
                _currentAnimation.Pause();
                _currentAnimation.SelectFrame(0);
            }


            // handle combat inputs

            timer.UpdateTimer();
            // up
            if (Globals.GetKey(Keys.Up) && timer.Test())
            {
                GameGlobals.PassProjectile(new PlayerAttack(0 - 90, Position));
                timer.ResetToZero();
            }
            // right
            if (Globals.GetKey(Keys.Right) && timer.Test())
            {
                GameGlobals.PassProjectile(new PlayerAttack(90 - 90, Position));
                timer.ResetToZero();
            }
            // down
            if (Globals.GetKey(Keys.Down) && timer.Test())
            {
                GameGlobals.PassProjectile(new PlayerAttack(180 - 90, Position));
                timer.ResetToZero();
            }
            // left
            if (Globals.GetKey(Keys.Left) && timer.Test())
            {
                GameGlobals.PassProjectile(new PlayerAttack(270 - 90, Position));
                timer.ResetToZero();
            }

            // Get the current animation frame.
            Texture = _currentAnimation.Sheet;
            SourceRectangle = _currentAnimation.CurrentFrame;

            base.Update();
        }

        /// <summary>
        /// Moves the Entity in the given direction with the given speed if possible.
        /// </summary>
        /// <param name="direciton">
        /// The direction in which the Entity shall move.<br></br>
        /// (0=up, 1=right, 2=down, 3=left)
        /// </param>
        /// <param name="speed">The given speed.</param>
        protected override void Move(byte direction, float speed)
        {
            // Get the Vector of the direction.
            Vector2 directionVector = Vector2.Zero;
            switch (direction)
            {
                // Up.
                case 0:
                    directionVector = -Vector2.UnitY;
                    break;
                // Right.
                case 1:
                    directionVector = Vector2.UnitX;
                    break;
                // Down.
                case 2:
                    directionVector = Vector2.UnitY;
                    break;
                // Left.
                case 3:
                    directionVector = -Vector2.UnitX;
                    break;
            }

            // The width of a door.
            byte doorWidth = 50;

            // Move this Sprite.
            Position += directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

            // If it collides with a door and is withing the door frame.
            if ((direction % 2 == 0
                 && Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) <= doorWidth * Scale.X)
                || (direction % 2 == 1
                    && Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) <= doorWidth * Scale.Y))
            {
                // It's allowed to move.
            }
            // Else if it collides with the top wall, another Entity or the frame of a door.
            else if (Collides(Level.CurrentRoom.Walls[direction])
                || Collides(Level.CurrentRoom.Entities)
                || (direction % 2 == 0
                    && (Collides(Level.CurrentRoom.Doors[1]) || Collides(Level.CurrentRoom.Doors[3]))
                        && Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) > doorWidth * Scale.Y)
                || (direction % 2 == 1
                    && (Collides(Level.CurrentRoom.Doors[0]) || Collides(Level.CurrentRoom.Doors[2]))
                        && Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) > doorWidth * Scale.X))
            {
                // It's not allowed to move up.
                // Revert its position.
                Position -= directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void IsDone()
        {
            
        }
    }
}
