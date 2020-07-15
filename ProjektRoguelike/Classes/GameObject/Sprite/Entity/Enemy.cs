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
        protected Player player = new Player(Vector2.Zero + (Room.Dimensions / 2 + new Vector2(0.5f, -Room.Dimensions.Y)) * Tile.Size);
        protected Vector2 PlayerPos;
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
        { Speed = 2f; }

        public override void Update()
        {
            System.Reflection.PropertyInfo a = player.GetType().GetProperty("Position");
            PlayerPos = (Vector2)(a.GetValue(player, null));
            player.Update();
            AI();

            base.Update();
        }

        public virtual void ChangePosition()
        {
            Position += Globals.RadialMovement(PlayerPos, Position, Speed);
        }

        public virtual void AI()
        {
            ChangePosition();

            if (Collides(player))
            {
                //player.Health--;
            }
        }
    }
}
