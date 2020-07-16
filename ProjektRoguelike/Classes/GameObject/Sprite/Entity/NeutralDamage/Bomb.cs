using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    class Bomb : NeutralDamage
    {
        McTimer timer;

        public Bomb(Vector2? position = null,
                    Rectangle? sourceRectangle = null,
                    float rotation = 0f,
                    SpriteEffects effects = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Pickups/Bomb"),
               position: position,
               sourceRectangle: sourceRectangle,
               rotation: rotation,
               effects: effects)
        {
            Health = 3;
            HitValue = 1;

            timer = new McTimer(1500);
        }

        public override void Update()
        {
            timer.UpdateTimer();
            if (timer.Test())
            {
                new Explosion(Position);
                Level.CurrentRoom.Remove(this);
            }

            base.Update();
        }
    }
}
