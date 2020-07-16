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
        McTimer timer;

        public Exploder(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Floatersheet_front"),
               position,
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation,
               effect)
        {
            Speed = 2f;
            Health = 3;

            timer = new McTimer(200);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void AI()
        {
            base.AI();

            timer.UpdateTimer();
            if (Collides(Level.Player))
            {
                new Explosion(Position);
                Level.CurrentRoom.Remove(this);
            }
        }
    }
}
