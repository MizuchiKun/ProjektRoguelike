using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public class Exploder : Enemy
    {
        public Exploder(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Explodersheet_front"),
               position,
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation,
               effect)
        {
            Speed = 2f;
            Health = 3;

            HitValue = 0;
        }

        public override void CollidePlayer()
        {
            // if you touch the player, spawn an explosion and disappear
            if (BumpsInto(Level.Player))
            {
                Level.CurrentRoom.Add(new Explosion(Position));
                Level.CurrentRoom.Remove(this);
            }
        }
    }
}
