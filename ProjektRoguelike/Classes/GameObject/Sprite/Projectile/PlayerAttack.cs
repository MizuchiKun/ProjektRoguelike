using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    public class PlayerAttack : Projectile
    {

        public PlayerAttack(float angle,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effects = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Pickups/Key"),
               position: position,
               sourceRectangle: sourceRectangle,
               rotation: rotation,
               effects: effects)
        {
            Done = false;

            Speed = 250.0f;

            timer = new McTimer(1000);

            Direction = Globals.DegreesToVector2(angle);
            Direction.Normalize();

            OwnerID = 1;

            HitValue = 1;
        }

        public override void Update(List<Enemy> entities)
        {
            base.Update(entities);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
