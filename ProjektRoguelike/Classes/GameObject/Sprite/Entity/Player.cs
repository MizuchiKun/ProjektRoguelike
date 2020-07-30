using System;
using System.Collections.Generic;
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
        McTimer timer, damageImunity;
        public int HealthMax;
        public bool Done = false, poopsicle = false;

        /// <summary>
        /// The player's current velocity per second.
        /// </summary>
        private Vector2 _velocity = Vector2.Zero;
        public ushort speed = 350;
        public int HitValue { get; set; }

        public int Gold { get; set; }
        public int Bombs { get; set; }
        public int Keys { get; set; }

        public List<Item> items = new List<Item>();

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
            damageImunity = new McTimer(500, true);

            Health = 15;
            HealthMax = Health;

            // Set the initial animation.
            CurrentAnimation = _walkingAnimations[2];
        }

        /// <summary>
        /// The Update method of a Player. Handles input and "stuff".
        /// </summary>
        public override void Update()
        {
            if (Health <= 0)
            {
                Globals.gamestate = Gamestate.Dead;
            }

            if (Globals.GetKeyUp(Microsoft.Xna.Framework.Input.Keys.P))
            {
                Globals.gamestate = Gamestate.Paused;
            }

            //activating itemeffects on buttonpress for testing
            if (Globals.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.K))
            {
                poopsicle = true;
            }

            //just boomer things
            if (Globals.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && Level.Player.Bombs > 0)
            {
                //Level.CurrentRoom.Add(new Explosion(Position));
                Level.CurrentRoom.Add(new Bomb(Position));
                Level.Player.Bombs--;
            }

            //testing items and such
            if (Globals.GetKeyUp(Microsoft.Xna.Framework.Input.Keys.J))
            {
                //Level.CurrentRoom.Add(new Itemstone(new Syringe(Level.CurrentRoom.Position + (Room.Dimensions / 5) * Tile.Size * Globals.Scale),
                //                                               Level.CurrentRoom.Position + (Room.Dimensions / 5) * Tile.Size * Globals.Scale));

                Level.CurrentRoom.Add(new PickupBomb(Level.CurrentRoom.Position + (Room.Dimensions / 5) * Tile.Size * Globals.Scale));
                //Level.Player.Keys += 1;
            }

            //testing environment and enemies
            if (Globals.GetKeyUp(Microsoft.Xna.Framework.Input.Keys.L))
            {
                Level.CurrentRoom.Add(new Hole(Level.CurrentRoom.Position + (Room.Dimensions / 3) * Tile.Size * Globals.Scale));
            }





            if (poopsicle)
            {
                Level.CurrentRoom.Add(new Flybuddy(new Vector2(Position.X, Position.Y + 55), 0));
                Level.CurrentRoom.Add(new Flybuddy(new Vector2(Position.X + 40, Position.Y - 40)));
                Level.CurrentRoom.Add(new Flybuddy(new Vector2(Position.X - 40, Position.Y - 40)));
                poopsicle = false;
            }
            // Handle movement input.
            // The movement speed.
            //ushort speed = 350;
            // The velocity.
            _velocity = Vector2.Zero;
            // Up.
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.W))
            {
                // Move it up.
                _velocity += -Vector2.UnitY * speed;

                // If there's a top door.
                if (Level.CurrentRoom.Doors[(byte)Directions.Up] != null)
                {
                    // If it touches the top door, it's hidden and the player has more than 0 keys.
                    if (!(Level.CurrentRoom.Doors[(byte)Directions.Up].Kind == DoorKind.Hidden)
                        && (BumpsInto(Level.CurrentRoom.Doors[(byte)Directions.Up])
                            && Math.Abs(Level.CurrentRoom.Doors[(byte)Directions.Up].Position.X - Position.X) <= Door.Width * Scale.X)
                        && Level.CurrentRoom.Doors[(byte)Directions.Up].State == DoorState.Locked
                        && Keys > 0)
                    {
                        // The player uses a key.
                        Keys--;
                        // Unlock the door.
                        Level.CurrentRoom.Doors[(byte)Directions.Up].Unlock(true);
                    }
                }
            }
            // Right.
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.D))
            {
                // Move it right.
                _velocity += Vector2.UnitX * speed;

                // If there's a right door.
                if (Level.CurrentRoom.Doors[(byte)Directions.Right] != null)
                {
                    // If it touches the right door, it's hidden and the player has more than 0 keys.
                    if (!(Level.CurrentRoom.Doors[(byte)Directions.Right].Kind == DoorKind.Hidden)
                        && (BumpsInto(Level.CurrentRoom.Doors[(byte)Directions.Right])
                            && Math.Abs(Level.CurrentRoom.Doors[(byte)Directions.Right].Position.Y - Position.Y) <= Door.Width * Scale.Y)
                        && Level.CurrentRoom.Doors[(byte)Directions.Right].State == DoorState.Locked
                        && Keys > 0)
                    {
                        // The player uses a key.
                        Keys--;
                        // Unlock the door.
                        Level.CurrentRoom.Doors[(byte)Directions.Right].Unlock(true);
                    }
                }
            }
            // Down.
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.S))
            {
                // Move it down.
                _velocity += Vector2.UnitY * speed;

                // If there's a bottom door.
                if (Level.CurrentRoom.Doors[(byte)Directions.Down] != null)
                {
                    // If it touches the bottom door, it's hidden and the player has more than 0 keys.
                    if (!(Level.CurrentRoom.Doors[(byte)Directions.Down].Kind == DoorKind.Hidden)
                        && (BumpsInto(Level.CurrentRoom.Doors[(byte)Directions.Down])
                            && Math.Abs(Level.CurrentRoom.Doors[(byte)Directions.Down].Position.X - Position.X) <= Door.Width * Scale.X)
                        && Level.CurrentRoom.Doors[(byte)Directions.Down].State == DoorState.Locked
                        && Keys > 0)
                    {
                        // The player uses a key.
                        Keys--;
                        // Unlock the door.
                        Level.CurrentRoom.Doors[(byte)Directions.Down].Unlock(true);
                    }
                }
            }
            // Left.
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.A))
            {
                // Move it left.
                _velocity += -Vector2.UnitX * speed;

                // If there's a left door.
                if (Level.CurrentRoom.Doors[(byte)Directions.Left] != null)
                {
                    // If it touches the left door, it's hidden and the player has more than 0 keys.
                    if (!(Level.CurrentRoom.Doors[(byte)Directions.Left].Kind == DoorKind.Hidden)
                        && (BumpsInto(Level.CurrentRoom.Doors[(byte)Directions.Left])
                            && Math.Abs(Level.CurrentRoom.Doors[(byte)Directions.Left].Position.Y - Position.Y) <= Door.Width * Scale.Y)
                        && Level.CurrentRoom.Doors[(byte)Directions.Left].State == DoorState.Locked
                        && Keys > 0)
                    {
                        // The player uses a key.
                        Keys--;
                        // Unlock the door.
                        Level.CurrentRoom.Doors[(byte)Directions.Left].Unlock(true);
                    }
                }
            }
            // Choose the proper animation.
            if (Math.Abs(_velocity.X) > Math.Abs(_velocity.Y))
            {
                // If it moves left.
                if (_velocity.X < 0)
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
                if (_velocity.Y < 0)
                {
                    CurrentAnimation = _walkingAnimations[0];
                }
                // Else it moves down.
                else if (_velocity.Y > 0)
                {
                    CurrentAnimation = _walkingAnimations[2];
                }
            }
            // Move it.
            if (_velocity != Vector2.Zero)
            {
                CurrentAnimation.Resume();
                _velocity = Globals.DegreesToVector2(Globals.Vector2ToDegrees(_velocity)) * speed;
                Move(_velocity);
            }
            else
            {
                CurrentAnimation.Pause();
                CurrentAnimation.SelectFrame(0);
            }


            // handle combat inputs

            damageImunity.UpdateTimer();
            timer.UpdateTimer();
            // up
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.Up) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(0 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(0 - 90, Position));
                timer.ResetToZero();
            }
            // right
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.Right) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(90 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(90 - 90, Position));
                timer.ResetToZero();
            }
            // down
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.Down) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(180 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(180 - 90, Position));
                timer.ResetToZero();
            }
            // left
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.Left) && timer.Test())
            {
                Level.CurrentRoom.Add(new BasicAttack(270 - 90, Position));
                //GameGlobals.PassProjectile(new PlayerAttack(270 - 90, Position));
                timer.ResetToZero();
            }

            // Update the Player's layer and stuff.
            base.Update();
        }

        /// <summary>
        /// The <see cref="Player"/>'s Draw method.
        /// </summary>
        public override void Draw()
        {
            //testing string to look how much damage something does
            //Globals.SpriteBatch.DrawString(Globals.Content.Load<SpriteFont>("Fonts/Consolas24"), Health.ToString(), new Vector2(Position.X, Position.Y - 75), Color.White);

            base.Draw();
        }

        public override void GetHit(int hitValue)
        {
            if (damageImunity.Test())
            {
                // receive damage
                base.GetHit(hitValue);
                damageImunity.ResetToZero();
            }
            else
            {
                //ignore it
            }
        }

        protected override bool CanMove(Directions direction)
        {
            // If it collides with a door and is within the door frame.
            if ((direction == Directions.Up
                 && ((Level.CurrentRoom.Doors[(byte)Directions.Up] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Up].State == DoorState.Open
                      && Math.Abs(Position.X - Level.CurrentRoom.Doors[(byte)Directions.Up].Position.X) <= Door.Width * Scale.X)))
                || (direction == Directions.Right
                    && ((Level.CurrentRoom.Doors[(byte)Directions.Right] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Right].State == DoorState.Open
                         && Math.Abs(Position.Y - Level.CurrentRoom.Doors[(byte)Directions.Right].Position.Y) <= Door.Width * Scale.Y)))
                || (direction == Directions.Down
                 && ((Level.CurrentRoom.Doors[(byte)Directions.Down] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Down].State == DoorState.Open
                      && Math.Abs(Position.X - Level.CurrentRoom.Doors[(byte)Directions.Down].Position.X) <= Door.Width * Scale.X)))
                || (direction == Directions.Left
                    && ((Level.CurrentRoom.Doors[(byte)Directions.Left] != null
                      && Level.CurrentRoom.Doors[(byte)Directions.Left].State == DoorState.Open
                         && Math.Abs(Position.Y - Level.CurrentRoom.Doors[(byte)Directions.Left].Position.Y) <= Door.Width * Scale.Y))))
            {
                // It's allowed to move.
                return true;
            }
            // Else if it collides with the top wall.
            else if (Collides(Level.CurrentRoom.Walls[(byte)direction]))
            {
                // Adjust the position.
                Vector2 position = Position;
                switch (direction)
                {
                    case Directions.Up:
                        position.Y = Level.CurrentRoom.Walls[(byte)Directions.Up][0].Position.Y + (0.5f * Tile.Size.Y);
                        break;
                    case Directions.Right:
                        position.X = Level.CurrentRoom.Walls[(byte)Directions.Right][0].Position.X - (1.0f * Tile.Size.X);
                        break;
                    case Directions.Down:
                        position.Y = Level.CurrentRoom.Walls[(byte)Directions.Down][0].Position.Y - (1.0f * Tile.Size.Y);
                        break;
                    case Directions.Left:
                        position.X = Level.CurrentRoom.Walls[(byte)Directions.Left][0].Position.X + (1.0f * Tile.Size.X);
                        break;
                }
                Position = position;
                // Then it's allowed to move.
                return true;
            }
            // Else if it collides with another Entity or the frame of a door.
            else if (Collides(Level.CurrentRoom.Entities)
                     || ((byte)direction % 2 == 0
                         && ((Level.CurrentRoom.Doors[1] != null && Collides(Level.CurrentRoom.Doors[1])) 
                              || (Level.CurrentRoom.Doors[3] != null && Collides(Level.CurrentRoom.Doors[3])))
                         && ((Level.CurrentRoom.Doors[1] != null && Math.Abs(Position.Y - Level.CurrentRoom.Doors[1].Position.Y) > Door.Width * Scale.Y)
                             || (Level.CurrentRoom.Doors[3] != null && Math.Abs(Position.Y - Level.CurrentRoom.Doors[3].Position.Y) > Door.Width * Scale.Y)))
                     || ((byte)direction % 2 == 1
                         && ((Level.CurrentRoom.Doors[0] != null && Collides(Level.CurrentRoom.Doors[0]))
                              || (Level.CurrentRoom.Doors[2] != null && Collides(Level.CurrentRoom.Doors[2])))
                         && ((Level.CurrentRoom.Doors[0] != null && Math.Abs(Position.X - Level.CurrentRoom.Doors[0].Position.X) > Door.Width * Scale.X)
                             || (Level.CurrentRoom.Doors[2] != null && Math.Abs(Position.X - Level.CurrentRoom.Doors[2].Position.X) > Door.Width * Scale.X))))
            {
                // It's not allowed to move.
                return false;
            }

            // Default return.
            return true;
        }

        /// <summary>
        /// Gets whether the <see cref="Player"/> "bumps into" one of the given <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="otherGameObjects">The other <see cref="GameObject"/>s.</param>
        /// <returns>True if it bumps into (at least) one of them, false otherwise.</returns>
        public bool BumpsInto(IEnumerable<GameObject> otherGameObjects)
        {
            foreach (GameObject gameObject in otherGameObjects)
            {
                if (BumpsInto(gameObject))
                {
                    // It bumped into one of the objects.
                    return true;
                }
            }

            // It didn't bump into anything.
            return false;
        }

        /// <summary>
        /// Gets whether the <see cref="Player"/> would "bumps into" the given <see cref="GameObject"/>.
        /// </summary>
        /// <param name="otherGameObject">The other <see cref="GameObject"/>.</param>
        /// <returns>True if it bumps into the given <see cref="GameObject"/>, false otherwise.</returns>
        public bool BumpsInto(GameObject otherGameObject)
        {
            // Move the Player (temporarily).
            Position += _velocity * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

            // Get whether it bumps into otherGameObject.
            bool bumpsInto = base.Collides(otherGameObject);

            // Restore the Player's previous position.
            Position -= _velocity * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

            // Return whether it bumped into otherGameObject.
            return bumpsInto;
        }
    }
}
