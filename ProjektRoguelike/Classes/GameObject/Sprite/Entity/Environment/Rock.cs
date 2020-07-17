using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    
    /// <summary>
    /// An enemy
    /// </summary>
    public class Rock : Environment
    {
        public Rock(Texture2D texture,
                     Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprite/Environment/Rock"),
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
            if (dropnumber <= 4)
            {
                // Drop Gold
            }
            else if (dropnumber > 4 && dropnumber <= 6)
            {
                // Drop PickupHeart
            }
            else if (dropnumber > 6 && dropnumber <= 8)
            {
                // Drop Bomb
            }
            else if (dropnumber > 8 && dropnumber <= 9)
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
