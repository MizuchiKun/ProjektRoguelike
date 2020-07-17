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
            base.Update();

            AI();
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

            //Move(new Vector2(Level.Player.Position.X - Position.X, Level.Player.Position.Y - Position.Y));

            Position += Globals.RadialMovement(Level.Player.Position, Position, Speed);
        }

        public virtual void AI()
        {
            ChangePosition();
        }
    }
}
