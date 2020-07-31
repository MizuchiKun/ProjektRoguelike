using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    public class Mainmenu
    {
        Texture2D bkg;

        public Mainmenu()
        {
            //Mainmenu1 needs to be added to the project
            bkg = Globals.Content.Load<Texture2D>("Sprites/Misc/Mainmenu1");
        }

        public virtual void Update()
        {
            // the buttons to continue
            //for now a keyboardinput instead, while button doesnt exist.
            if (Globals.GetKeyUp(Microsoft.Xna.Framework.Input.Keys.O))
            {
                Globals.gamestate = Gamestate.Active;
            }
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(bkg, new Rectangle((int)Camera.Position.X, (int)Camera.Position.Y, 200, 200), null, Color.White, 0, new Vector2(.5f), SpriteEffects.None, 0.1f);
        }
    }
}
