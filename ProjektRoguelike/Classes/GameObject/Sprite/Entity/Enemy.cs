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
        protected Vector2 PlayerPos;
        protected float speed;
        protected Player player = new Player(Vector2.Zero + (Room.Dimensions / 2 + new Vector2(0.5f, -Room.Dimensions.Y)) * Tile.Size);

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
        { }

        public override void Update()
        {
            System.Reflection.PropertyInfo a = player.GetType().GetProperty("Position");
            PlayerPos = (Vector2)(a.GetValue(player, null));
            player.Update();
            AI(PlayerPos);
        }

        public virtual void AI(Vector2 focus)
        {
            Position += Globals.RadialMovement(focus, Position, speed);

            if (Collides(player))
            {
                player.GetHit(this, 1);
            }
        }
    }
}
