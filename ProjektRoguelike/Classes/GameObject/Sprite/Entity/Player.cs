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
        McTimer timer;
        public int HealthMax;
        public bool Done = false;
        public ushort speed = 350;

        /// <summary>
        /// The walking animations of the Player.<br></br>
        /// (0=up, 1=right, 2=down, 3=left)
        /// </summary>
        private static Animation[] _walkingAnimations =
        {
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_up"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(150)),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_left"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(150),
                          effects: SpriteEffects.FlipHorizontally),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_down"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(150)),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Playable Char/Herosheet_left"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(150))
        };

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
        { 
            timer = new McTimer(600, true);
            HealthMax = Health;

            // Set the initial animation.
            CurrentAnimation = _walkingAnimations[2];
        }

        /// <summary>
        /// The Update method of a Player. Handles input and "stuff".
        /// </summary>
        public override void Update()
        {
            // Handle movement input.
            // The movement speed.
            //ushort speed = 350;
            // The velocity.
            Vector2 velocity = Vector2.Zero;
            // Up.
            if (Globals.GetKey(Keys.W))
            {
                // Move it up.
                velocity += -Vector2.UnitY * speed;
            }
            // Right.
            if (Globals.GetKey(Keys.D))
            {
                // Move it right.
                velocity += Vector2.UnitX * speed;
            }
            // Down.
            if (Globals.GetKey(Keys.S))
            {
                // Move it down.
                velocity += Vector2.UnitY * speed;
            }
            // Left.
            if (Globals.GetKey(Keys.A))
            {
                // Move it left.
                velocity += -Vector2.UnitX * speed;
            }
            // Choose the proper antimation.
            if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
            {
                // If it moves left.
                if (velocity.X < 0)
                {
                    CurrentAnimation = _walkingAnimations[3];
                }
                // Else it moves right.
                else
                {
                    CurrentAnimation = _walkingAnimations[1];
                }
            }
            // Else it moves up, down or diagonally.
            else
            {
                // If it moves up.
                if (velocity.Y < 0)
                {
                    CurrentAnimation = _walkingAnimations[0];
                }
                // Else it moves down.
                else if (velocity.Y > 0)
                {
                    CurrentAnimation = _walkingAnimations[2];
                }
            }
            // Move it.
            if (velocity != Vector2.Zero)
            {
                CurrentAnimation.Resume();
                Move(Globals.DegreesToVector2(Globals.Vector2ToDegrees(velocity)) * speed);
            }
            else
            {
                CurrentAnimation.Pause();
                CurrentAnimation.SelectFrame(0);
            }


            // handle combat inputs

            timer.UpdateTimer();
            // up
            if (Globals.GetKey(Keys.Up) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(0 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(0 - 90, Position));
                timer.ResetToZero();
            }
            // right
            if (Globals.GetKey(Keys.Right) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(90 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(90 - 90, Position));
                timer.ResetToZero();
            }
            // down
            if (Globals.GetKey(Keys.Down) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(180 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(180 - 90, Position));
                timer.ResetToZero();
            }
            // left
            if (Globals.GetKey(Keys.Left) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(270 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(270 - 90, Position));
                timer.ResetToZero();
            }
        }

        /// <summary>
        /// Moves the Entity in the given direction with the given speed if possible.
        /// </summary>
        /// <param name="direciton">
        /// The direction in which the Entity shall move.
        /// </param>
        /// <param name="speed">The given speed.</param>
        protected override void Move(Directions direction, float speed)
        {
            // Get the Vector of the direction.
            Vector2 directionVector = Vector2.Zero;
            switch (direction)
            {
                case Directions.Up:
                    directionVector = -Vector2.UnitY;
                    break;
                case Directions.Right:
                    directionVector = Vector2.UnitX;
                    break;
                case Directions.Down:
                    directionVector = Vector2.UnitY;
                    break;
                case Directions.Left:
                    directionVector = -Vector2.UnitX;
                    break;
            }

            // The width of a door.
            byte doorWidth = 50;

            // Move this Sprite.
            Position += directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

            // If it collides with a door and is within the door frame.
            if ((direction == Directions.Up
                 && ((Level.CurrentRoom.Doors[(byte)Directions.Up] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Up].State == DoorState.Open
                      && Math.Abs(Position.X - Level.CurrentRoom.Doors[(byte)Directions.Up].Position.X) <= doorWidth * Scale.X)))
                || (direction == Directions.Right
                    && ((Level.CurrentRoom.Doors[(byte)Directions.Right] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Right].State == DoorState.Open
                         && Math.Abs(Position.Y - Level.CurrentRoom.Doors[(byte)Directions.Right].Position.Y) <= doorWidth * Scale.Y)))
                || (direction == Directions.Down
                 && ((Level.CurrentRoom.Doors[(byte)Directions.Down] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Down].State == DoorState.Open
                      && Math.Abs(Position.X - Level.CurrentRoom.Doors[(byte)Directions.Down].Position.X) <= doorWidth * Scale.X)))
                || (direction == Directions.Left
                    && ((Level.CurrentRoom.Doors[(byte)Directions.Left] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Left].State == DoorState.Open
                         && Math.Abs(Position.Y - Level.CurrentRoom.Doors[(byte)Directions.Left].Position.Y) <= doorWidth * Scale.Y))))
            {
                // It's allowed to move.
            }
            // Else if it collides with the top wall, another Entity or the frame of a door.
            else if (Collides(Level.CurrentRoom.Walls[(byte)direction])
                     || Collides(Level.CurrentRoom.Entities)
                     || ((byte)direction % 2 == 0
                         && ((Level.CurrentRoom.Doors[1] != null && Collides(Level.CurrentRoom.Doors[1])) 
                              || (Level.CurrentRoom.Doors[3] != null && Collides(Level.CurrentRoom.Doors[3])))
                         && ((Level.CurrentRoom.Doors[1] != null && Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) > doorWidth * Scale.Y)
                             || (Level.CurrentRoom.Doors[3] != null && Math.Abs(Position.Y - Level.CurrentRoom.Doors[3].Position.Y) > doorWidth * Scale.Y)))
                     || ((byte)direction % 2 == 1
                         && ((Level.CurrentRoom.Doors[0] != null && Collides(Level.CurrentRoom.Doors[0]))
                              || (Level.CurrentRoom.Doors[2] != null && Collides(Level.CurrentRoom.Doors[2])))
                         && ((Level.CurrentRoom.Doors[0] != null && Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) > doorWidth * Scale.X)
                             || (Level.CurrentRoom.Doors[2] != null && Math.Abs(Position.X - Level.CurrentRoom.Doors[2].Position.X) > doorWidth * Scale.X))))
            {
                // It's not allowed to move up.
                // Revert its position.
                Position -= directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
