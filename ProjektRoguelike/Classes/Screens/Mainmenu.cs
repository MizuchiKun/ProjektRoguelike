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

        Button NewRun, Continue, Challenges, Stats, Options;

        PassObject NewRunObject, ContinueObject, ChallengeObject, StatsObject, OptionsObject;

        List<Button> buttons = new List<Button>();

        public Mainmenu()
        {
            //Mainmenu1 needs to be added to the project
            bkg = Globals.Content.Load<Texture2D>("Sprites/Misc/Mainmenu1");

            NewRunObject = NewGame;
            NewRun = new Button(Button.ButtonState.Selected, NewRunObject, null, new Vector2(1680, 940));
            buttons.Add(NewRun);

            ContinueObject = null;
            Continue = new Button(Button.ButtonState.Unselected, ContinueObject, null, new Vector2(1680, 1050));
            buttons.Add(Continue);

            ChallengeObject = null;
            Challenges = new Button(Button.ButtonState.Unselected, null, null, new Vector2(1680, 1130));
            buttons.Add(Challenges);

            StatsObject = null;
            Stats = new Button(Button.ButtonState.Unselected, null, null, new Vector2(1680, 1190));
            buttons.Add(Stats);

            OptionsObject = null;
            Options = new Button(Button.ButtonState.Unselected, null, null, new Vector2(1680, 1270));
            buttons.Add(Options);
        }

        public virtual void Update()
        {
            // Update the buttons and check which button is supposed to be selected.
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update();
                if (buttons[i].buttonState == Button.ButtonState.Selected && (Globals.GetKeyUp(Keys.W) || Globals.GetKeyUp(Keys.Up)) && i > 0)
                {       
                    buttons[i - 1].buttonState = Button.ButtonState.Selected;
                    buttons[i].buttonState = Button.ButtonState.Unselected;
                }
                if (buttons[i].buttonState == Button.ButtonState.Selected && (Globals.GetKeyUp(Keys.S) || Globals.GetKeyUp(Keys.Down)) && i < buttons.Count - 1)
                {
                    buttons[i + 1].buttonState = Button.ButtonState.Selected;
                    buttons[i].buttonState = Button.ButtonState.Unselected;

                    // does not work as intended
                }
            }

            // Close the game by hitting the escape key. 
            if (Globals.GetKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                System.Environment.Exit(0);
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw();
            }
            Globals.SpriteBatch.Draw(bkg, new Rectangle((int)Camera.Position.X, (int)Camera.Position.Y, Globals.Graphics.PreferredBackBufferWidth, Globals.Graphics.PreferredBackBufferHeight), null, Color.White, 0, new Vector2(.5f), SpriteEffects.None, 0.1f); 
        }

        /// <summary>
        /// Empty the Entities List, create a new level and set the gamestate to active.
        /// </summary>
        /// <param name="info"> Only here, so I can use my delegate. lol </param>
        private void NewGame(object info)
        {
            Level.CurrentRoom.Entities.Clear();
            Globals.CurrentScene = new Level(0);
            Globals.gamestate = Gamestate.Active;
        }
    }
}
