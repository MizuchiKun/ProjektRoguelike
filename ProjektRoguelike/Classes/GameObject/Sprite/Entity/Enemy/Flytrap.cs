using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public class Flytrap : Enemy
    {
        McTimer timer;

        public Flytrap(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Flytrapsheet_front"),
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
                Level.Player.GetHit(1);
                for (int i = 0; i < 3; i++)
                {
                    Level.CurrentRoom.Add(new Fly(new Vector2(Position.X, Position.Y - 10 + (10 * i))));
                }
                Level.CurrentRoom.Remove(this);
            }

            if (Health >= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Level.CurrentRoom.Add(new Fly(new Vector2(Position.X, Position.Y - 10 + (10 * i))));
                }
                Level.CurrentRoom.Remove(this);
            }
        }
    }
}
