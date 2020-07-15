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
            /*
             * Something of the past
            if (Done == true)
            {
                Level.CurrentRoom.Remove(this);
            }
            */
            base.Update();

            AI();
        }

        public virtual void ChangePosition()
        {
            Position += Globals.RadialMovement(Level.Player.Position, Position, Speed);
        }

        public virtual void AI()
        {
            ChangePosition();

            if (Collides(Level.Player))
            {
                //player.Health--;
            }
        }
    }
}
