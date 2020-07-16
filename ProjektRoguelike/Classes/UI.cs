using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    public class UI
    {
        public string gameOver = "Game Over";

        Texture2D heart = new Texture2D(Globals.Graphics.GraphicsDevice, 256, 256);

        public UI()
        {
            heart = Globals.Content.Load<Texture2D>("Sprites/Items/Heart");
        }

        public virtual void Update()
        {
            //tbc
        }

        public virtual void Draw()
        {
            if (Level.Player.Done)
            {
                new Text(Globals.Content.Load<SpriteFont>("Fonts/Consolas24"),
                                                  new StringBuilder(gameOver),
                                                  new Vector2(Globals.Graphics.PreferredBackBufferWidth / 2, Globals.Graphics.PreferredBackBufferHeight / 2),
                                                  new Vector2(.5f));
            }
            for (int i = 0; i < Level.Player.HealthMax; i++)
            {
                Globals.SpriteBatch.Draw(heart, new Rectangle(50 + (i * 50), 50, 25, 25), null, Color.Black, 0, new Vector2(.5f), new SpriteEffects(), 0);
            }
            for (int i = 0; i < Level.Player.Health; i++)
            {
                Globals.SpriteBatch.Draw(heart, new Rectangle(50 + (i * 50), 50, 25, 25), null, Color.White, 0, new Vector2(.5f), new SpriteEffects(), 0);
            }
        }
    }
}
