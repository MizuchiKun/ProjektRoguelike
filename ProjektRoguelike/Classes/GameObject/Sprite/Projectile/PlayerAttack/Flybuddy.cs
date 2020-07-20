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
        public bool isDestroyed;

        public Flybuddy(Vector2? position = null,
                        /*Rectangle? sourceRectangle = null,*/
                        float rotation = 0f,
                        SpriteEffects effects = SpriteEffects.None)
        : base(angle: 0f,
               texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Flysheet"),
               position: position,
               sourceRectangle: new Rectangle(0, 0, 256, 256)/*sourceRectangle*/,
               rotation: rotation,
               effects: effects)
        {

            Speed = 250.0f;

            timer = new McTimer(1000);

            OwnerID = 1;

            HitValue = 1;

            isDestroyed = false;

            // Set the animation.
            CurrentAnimation = new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Enemies/FLysheet"),
                                             frameDimensions: new Vector2(256),
                                             frameDuration: TimeSpan.FromSeconds(1f / 60f));
        }

        public override void Update(/*List<Enemy> entities*/)
        {
            if (Collides(Level.CurrentRoom.Enemies))
            {
                isDestroyed = true;
                //damage touching enemy
            }
            if (isDestroyed == false)
            {
                base.Update(/*entities*/);
            }
        }

        public override void ChangePosition()
        {
            Globals.RotateAboutOrigin(Position, Level.Player.Position, 0.1f);
        }

        public override void Draw()
        {
            if (isDestroyed == false)
            {
                base.Draw();
            }
        }
    }
}
