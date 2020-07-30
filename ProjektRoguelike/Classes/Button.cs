using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    class Button
    {
        public enum ButtonState
        {
            Unselected, Selected, Activated
        }

        /// <summary>
        /// The state of the button.
        /// </summary>
        public ButtonState buttonState { get; set; }

        public PassObject buttonActivated;

        Texture2D activated;

        public object Info;

        public Vector2 position { get; set; }

        public Button(ButtonState ButtonState, PassObject ButtonActivated, object info, Vector2 Position)
        {
            Info = info;
            position = Position;
            buttonState = ButtonState;
            buttonActivated = ButtonActivated;

            activated = Globals.Content.Load<Texture2D>("Sprites/Enemies/Shadow");
        }

        public virtual void Update()
        {
            switch (buttonState)
            {
                case ButtonState.Unselected:
                    break;
                case ButtonState.Selected:
                    if (Globals.GetKeyUp(Microsoft.Xna.Framework.Input.Keys.Enter))
                    {
                        buttonState = ButtonState.Activated;
                    }
                    break;
                case ButtonState.Activated:
                    ButtonUse();
                    break;
                default:
                    break;
            }
        }

        public virtual void ButtonUse()
        {
            if (buttonActivated != null)
            {
                buttonActivated(Info);
            }
        }

        public virtual void Draw()
        {
            switch (buttonState)
            {
                case ButtonState.Unselected:
                    break;
                case ButtonState.Selected:
                    Globals.SpriteBatch.Draw(activated, new Rectangle((int)position.X, (int)position.Y, 40, 40), null, Color.White, 0, new Vector2(.5f), new SpriteEffects(), 0.05f);
                    break;
                case ButtonState.Activated:
                    Globals.SpriteBatch.Draw(activated, new Rectangle((int)position.X, (int)position.Y, 40, 40), null, Color.White, 0, new Vector2(.5f), new SpriteEffects(), 0.05f);
                    break;
                default:
                    break;
            }
        }
    }
}
