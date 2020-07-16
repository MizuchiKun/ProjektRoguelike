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
    public class Flybuddy : PlayerAttack
    {
        public Flybuddy(Vector2? position = null,
                        Rectangle? sourceRectangle = null,
                        float rotation = 0f,
                        SpriteEffects effects = SpriteEffects.None)
        : base(angle: 0f,
               texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Fly"),
               position: position,
               sourceRectangle: sourceRectangle,
               rotation: rotation,
               effects: effects)
        {

            Speed = 250.0f;

            timer = new McTimer(1000);

            OwnerID = 1;

            HitValue = 1;
        }

        public override void Update(/*List<Enemy> entities*/)
        {
            base.Update(/*entities*/);
        }

        public override void ChangePosition()
        {
            Globals.RotateAboutOrigin(Position, Level.Player.Position, 0.1f);
        }
    }
}
