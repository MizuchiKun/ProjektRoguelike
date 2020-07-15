using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    public class EnemyShot : Projectile
    {

        public EnemyShot(float angle,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effects = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Pickups/Coin"),
               angle: angle,
               position: position,
               sourceRectangle: sourceRectangle,
               rotation: rotation,
               effects: effects)
        {
            done = false;

            speed = 250.0f;

            timer = new McTimer(1000);

            direction = Globals.DegreesToVector2(angle);
            direction.Normalize();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
