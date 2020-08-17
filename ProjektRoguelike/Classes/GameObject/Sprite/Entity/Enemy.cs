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
        public int HitValue { get; set; } = 1;

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

            // Close all doors.
            foreach (Door door in Level.CurrentRoom.Doors)
            {
                // If there's a door.
                if (door != null)
                {
                    // If the door is not hidden.
                    if (!(door.Kind == DoorKind.Hidden)
                        && door.State != DoorState.Closed)
                    {
                        door.Close();
                    }
                }
            }
        }

        public override void Update()
        {
            AI();

            base.Update();
        }

        public virtual void ChangePosition()
        {
            _velocity = Level.Player.Position - Position;
            // move towards the player
            Move(_velocity);
        }

        /// <summary>
        /// What the enemy is supposed to do. the only function in Update() is AI(), so change this to change Update().
        /// Base AI includes ChangePosition and CollidePlayer.
        /// </summary>
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
        if (BumpsInto(Level.Player))                                        
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
                Level.Player.GetHit(HitValue);
            }
        }
    }
}
