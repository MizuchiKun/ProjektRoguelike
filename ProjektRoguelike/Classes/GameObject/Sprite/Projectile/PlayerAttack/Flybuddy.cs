﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    public class Flybuddy : PlayerAttack
    {
        public bool isDestroyed;
        public Vector2 Velocity;
        float angle, DistanceToPlayer;

        public Flybuddy(Vector2? position = null,
                        /*Rectangle? sourceRectangle = null,*/
                        float rotation = 0f,
                        SpriteEffects effects = SpriteEffects.None)
        : base(angle: 0f,
               texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Flysheet"),
               position: position,
               sourceRectangle: new Rectangle(0, 0, 256, 256)/*sourceRectangle*/,
               rotation: rotation,
               effects: effects)
        {

            Speed = 250.0f;

            DistanceToPlayer = Globals.GetDistance(Position, Level.Player.Position);

            timer = new McTimer(1000);

            OwnerID = 1;

            HitValue = 1;

            isDestroyed = false;

            Scale = new Vector2(.1f, .1f);

            // Set the animation.
            CurrentAnimation = new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Enemies/FLysheet"),
                                             frameDimensions: new Vector2(256),
                                             frameDuration: TimeSpan.FromSeconds(1f / 60f));
        }

        public override void Update(/*List<Enemy> entities*/)
        {
            if (Collides(Level.CurrentRoom.Enemies))
            {
                isDestroyed = true;
                //damage touching enemy
            }
            if (isDestroyed == false)
            {
                ChangePosition();

                if (Collides(Level.CurrentRoom.Enemies) && (OwnerID == 1 || OwnerID == 0))
                {
                    bool isColliding = false;//NEW
                    for (int i = 0; i < Level.CurrentRoom.Enemies.Count; i++)
                    {
                        if (Collides(Level.CurrentRoom.Enemies[i])//NEW
                            && OwnerID == 1)
                        {
                            Level.CurrentRoom.Enemies[i].GetHit(HitValue);
                            isColliding = true;
                        }
                    }
                    //NEW
                    if (isColliding)
                    {
                        Level.CurrentRoom.Remove(this);
                    }
                }
            }
        }

        public override void ChangePosition()
        {
            //angle = Globals.Vector2ToDegrees(Level.Player.Position - Position);
            Position = Globals.RotateAboutOrigin(Position, Level.Player.Position, .05f);

            float SpeedFix;
            if ((Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.A) && Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.S)) ||
                (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.A) && Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.W)) ||
                (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.D) && Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.S)) ||
                (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.D) && Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.W)))
            {
                SpeedFix = 3f;
            }
            else
            {
                SpeedFix = 4f;
            }
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.W))
            {
                Position = new Vector2(Position.X, Position.Y - ((Level.Player.speed / Speed) * SpeedFix));
                Position.Normalize();
            }
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.S))
            {
                Position = new Vector2(Position.X, Position.Y + ((Level.Player.speed / Speed) * SpeedFix));
                Position.Normalize();
            }

            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.D))
            {
                Position = new Vector2(Position.X + ((Level.Player.speed / Speed) * SpeedFix), Position.Y);
                Position.Normalize();
            }
            if (Globals.GetKey(Microsoft.Xna.Framework.Input.Keys.A))
            {
                Position = new Vector2(Position.X - ((Level.Player.speed / Speed) * SpeedFix), Position.Y);
                Position.Normalize();
            }
        }

        protected void Move(Vector2 velocity)
        {
            // Multiply the velocity by Globals.Scale.
            velocity *= Globals.Scale;

            // Move horizontally.
            // If it moves right.
            if (velocity.X > 0)
            {
                Move(Directions.Right, (float)Math.Abs(velocity.X));
            }
            // Else it moves left.
            else
            {
                Move(Directions.Left, (float)Math.Abs(velocity.X));
            }

            // Move vertically.
            // If it moves down.
            if (velocity.Y > 0)
            {
                Move(Directions.Down, (float)Math.Abs(velocity.Y));
            }
            // Else it moves up.
            else
            {
                Move(Directions.Up, (float)Math.Abs(velocity.Y));
            }
        }

        private void Move(Directions direction, float speed)
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

            // Move this Sprite.
            Position += directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
        }
        }
    }
