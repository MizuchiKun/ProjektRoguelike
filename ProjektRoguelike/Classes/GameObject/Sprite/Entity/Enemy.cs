using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public abstract class Enemy : Entity
    {
        protected float Speed;

        public Enemy(Texture2D texture,
                     Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture,
               position,
               sourceRectangle,
               rotation,
               effect)
        {
            Speed = 2f;
        }

        public override void Update()
        {
            AI();

            base.Update();
        }

        public virtual void ChangePosition()
        {
            /*
            if (Globals.Vector2ToDegrees(Level.Player.Position - Position) > 0 - 90 || Globals.Vector2ToDegrees(Level.Player.Position - Position) < 90 - 90)
            {
                Move(); 
            }
            else if (Globals.Vector2ToDegrees(Level.Player.Position - Position) > 90 - 90 || Globals.Vector2ToDegrees(Level.Player.Position - Position) < 180 - 90)
            {
                Move();
            }
            else if (Globals.Vector2ToDegrees(Level.Player.Position - Position) > 180 - 90 || Globals.Vector2ToDegrees(Level.Player.Position - Position) < 270 - 90)
            {
                Move();
            }
            else if (Globals.Vector2ToDegrees(Level.Player.Position - Position) > 270 - 90 || Globals.Vector2ToDegrees(Level.Player.Position - Position) < 360 - 90)
            {
                Move();
            }
            */

            Move(Level.Player.Position - Position);
            //if (!HitWall())
            //{
            //    Position += Globals.RadialMovement(Level.Player.Position, Position, Speed);
            //}
            //else
            //{
            //    Position -= Globals.RadialMovement(Level.Player.Position, Position, Speed);
            //}
        }

        public virtual void AI()
        {
            ChangePosition();
            CollidePlayer();
        }

        public virtual bool HitWall()
        {
            // up
            if (Collides(Level.CurrentRoom.Walls[0]))
            {
                return true;
            }
            // right
            else if (Collides(Level.CurrentRoom.Walls[1]))
            {
                return true;
            }
            // down
            else if (Collides(Level.CurrentRoom.Walls[2]))
            {
                return true;
            }
            // left
            else if (Collides(Level.CurrentRoom.Walls[3]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void CollidePlayer()
        {                                                                 
        if (Touches(Level.Player))                                        
            {
                //if (!Level.Player.Collides(Level.CurrentRoom.Walls[1])  
                //|| !Level.Player.Collides(Level.CurrentRoom.Walls[2])   
                //|| !Level.Player.Collides(Level.CurrentRoom.Walls[3])   
                //|| !Level.Player.Collides(Level.CurrentRoom.Walls[4]))
                /*
                //left
                for (int i = 0; i < Level.CurrentRoom.Walls[(byte)Directions.Up].Length; i++)
                {
                    if (Globals.GetDistance(Level.Player.Position, Level.CurrentRoom.Walls[(byte)Directions.Up][i].Position) > (Level.Player.Hitbox.Height * 1.2))
                    {
                        Level.Player.Position += -Globals.RadialMovement(Position, Level.Player.Position, Speed * 10);
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 10);
                    }
                    else
                    {
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 20);
                    }
                }

                //right
                for (int i = 0; i < Level.CurrentRoom.Walls[(byte)Directions.Right].Length; i++)
                {
                    if (Globals.GetDistance(Level.Player.Position, Level.CurrentRoom.Walls[(byte)Directions.Up][i].Position) > (Level.Player.Hitbox.Width * 1.2))
                    {
                        Level.Player.Position += -Globals.RadialMovement(Position, Level.Player.Position, Speed * 10);
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 10);
                    }
                    else
                    {
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 20);
                    }
                }

                //down
                for (int i = 0; i < Level.CurrentRoom.Walls[(byte)Directions.Down].Length; i++)
                {
                    if (Globals.GetDistance(Level.Player.Position, Level.CurrentRoom.Walls[(byte)Directions.Up][i].Position) > (Level.Player.Hitbox.Height * 1.2))
                    {
                        Level.Player.Position += -Globals.RadialMovement(Position, Level.Player.Position, Speed * 10);
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 10);
                    }
                    else
                    {
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 20);
                    }
                }

                //left
                for (int i = 0; i < Level.CurrentRoom.Walls[(byte)Directions.Left].Length; i++)
                {
                    if (Globals.GetDistance(Level.Player.Position, Level.CurrentRoom.Walls[(byte)Directions.Up][i].Position) > (Level.Player.Hitbox.Width * 1.2))
                    {
                        Level.Player.Position += -Globals.RadialMovement(Position, Level.Player.Position, Speed * 10);
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 10);
                    }
                    else
                    {
                        Position += -Globals.RadialMovement(Level.Player.Position, Position, Speed * 20);
                    }
                }
                */
                Level.Player.GetHit(1);
            }
        }
    }
}
