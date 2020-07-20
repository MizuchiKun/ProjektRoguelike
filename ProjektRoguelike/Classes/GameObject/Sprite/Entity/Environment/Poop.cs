using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    
    /// <summary>
    /// An enemy
    /// </summary>
    public class Poop : Environment
    {
        public Poop(Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprite/Environment/Poop"),
               position,
               sourceRectangle,
               rotation,
               effect)
        { 

        }

        public override void Update()
        {

            base.Update();
        }

        protected override void Dropchance(int dropnumber)
        {
            if (dropnumber <= 3)
            {
                // Drop PickupHeart
            }
            else if (dropnumber > 3 && dropnumber <= 5)
            {
                // Drop Gold
            }
            else if (dropnumber > 5 && dropnumber <= 6)
            {
                // Drop Bomb
            }
            else if (dropnumber > 6 && dropnumber <= 7)
            {
                // Drop Key
            }
            else
            {
                Level.CurrentRoom.Add(new Fly(Position));
            }
        }
    }
}
