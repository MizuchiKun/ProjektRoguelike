using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    public class QualityDisplayBar
    {
        Texture2D heart = new Texture2D(Globals.Graphics.GraphicsDevice, 256, 256);

        public QualityDisplayBar()
        {
            heart = Globals.Content.Load<Texture2D>("Sprites/Items/Heart");
        }

        public virtual void Update()
        {
            //tbc
        }

        public virtual void Draw()
        {
            //tbc
        }
    }
}
