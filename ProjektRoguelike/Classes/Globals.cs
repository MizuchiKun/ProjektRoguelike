using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Contains information and functions of global importance.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// The <see cref="ContentManager"/> of this project.
        /// </summary>
        public static ContentManager Content { get; set; }

        /// <summary>
        /// The <see cref="GraphicsDeviceManager"/> of this project.
        /// </summary>
        public static GraphicsDeviceManager Graphics { get; set; }

        /// <summary>
        /// The <see cref="Camera"/> of this project.
        /// </summary>
        public static Camera Camera { get; set; }

        /// <summary>
        /// The <see cref="BasicEffect"/> of this project.
        /// </summary>
        public static BasicEffect BasicEffect { get; set; }

        /// <summary>
        /// The current <see cref="Scene"/>.
        /// </summary>
        public static Scene CurrentScene { get; set; }

        /// <summary>
        /// The current <see cref="GameTime"/>.
        /// </summary>
        public static GameTime GameTime { get; set; }

        /// <summary>
        /// The previous state of the keyboard.
        /// </summary>
        public static KeyboardState PreviousKeyboardState { get; set; }

        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        public static KeyboardState CurrentKeyboardState { get; set; }

        /// <summary>
        /// The previous state of the mouse.
        /// </summary>
        public static MouseState PreviousMouseState { get; set; }

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        public static MouseState CurrentMouseState { get; set; }

        /// <summary>
        /// The previous state of the gamepad.
        /// </summary>
        public static GamePadState PreviousGamePadState { get; set; }

        /// <summary>
        /// The current state of the gamepad.
        /// </summary>
        public static GamePadState CurrentGamePadState { get; set; }


        /// <summary>
        /// Gets whether the given key is currently pressed.
        /// </summary>
        /// <param name="key">The given key.</param>
        /// <return>True if the key is currently pressed, false otherwise.</return>
        public static bool GetKey(Keys key)
        {
            return (PreviousKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyDown(key));
        }

        /// <summary>
        /// Gets whether the given key was just pressed down.
        /// </summary>
        /// <param name="key">The given key.</param>
        /// <returns>True if it was just pressed down, false otherwise.</returns>
        public static bool GetKeyDown(Keys key)
        {
            return (PreviousKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key));
        }

        /// <summary>
        /// Gets whether the given key was just released.
        /// </summary>
        /// <param name="key">The given key.</param>
        /// <returns>True if it was just released, false otherwise.</returns>
        public static bool GetKeyUp(Keys key)
        {
            return (PreviousKeyboardState.IsKeyDown(key) && CurrentKeyboardState.IsKeyUp(key));
        }

        /// <summary>
        /// Gets whether the given mouse button is currently pressed.
        /// </summary>
        /// <param name="button">The given mouse button.</param>
        /// <returns>True if the mouse button is currently pressed, false otherwise.</returns>
        public static bool GetMouseButton(MouseButton button)
        {
            // Which button?
            switch (button)
            {
                case MouseButton.Left:
                    return (PreviousMouseState.LeftButton == ButtonState.Pressed && CurrentMouseState.LeftButton == ButtonState.Pressed);
                case MouseButton.Middle:
                    return (PreviousMouseState.MiddleButton == ButtonState.Pressed && CurrentMouseState.MiddleButton == ButtonState.Pressed);
                case MouseButton.Right:
                    return (PreviousMouseState.RightButton == ButtonState.Pressed && CurrentMouseState.RightButton == ButtonState.Pressed);
                case MouseButton.XButton1:
                    return (PreviousMouseState.XButton1 == ButtonState.Pressed && CurrentMouseState.XButton1 == ButtonState.Pressed);
                case MouseButton.XButton2:
                    return (PreviousMouseState.XButton2 == ButtonState.Pressed && CurrentMouseState.XButton2 == ButtonState.Pressed);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets whether the given mouse button was just pressed down.
        /// </summary>
        /// <param name="button">The given mouse button.</param>
        /// <returns>True if the mouse button was just pressed down, false otherwise.</returns>
        public static bool GetMouseButtonDown(MouseButton button)
        {
            // Which button?
            switch (button)
            {
                case MouseButton.Left:
                    return (PreviousMouseState.LeftButton == ButtonState.Released && CurrentMouseState.LeftButton == ButtonState.Pressed);
                case MouseButton.Middle:
                    return (PreviousMouseState.MiddleButton == ButtonState.Released && CurrentMouseState.MiddleButton == ButtonState.Pressed);
                case MouseButton.Right:
                    return (PreviousMouseState.RightButton == ButtonState.Released && CurrentMouseState.RightButton == ButtonState.Pressed);
                case MouseButton.XButton1:
                    return (PreviousMouseState.XButton1 == ButtonState.Released && CurrentMouseState.XButton1 == ButtonState.Pressed);
                case MouseButton.XButton2:
                    return (PreviousMouseState.XButton2 == ButtonState.Released && CurrentMouseState.XButton2 == ButtonState.Pressed);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets whether the given mouse button was just released.
        /// </summary>
        /// <param name="button">The given mouse button.</param>
        /// <returns>True if the mouse button was just released, false otherwise.</returns>
        public static bool GetMouseButtonUp(MouseButton button)
        {
            // Which button?
            switch (button)
            {
                case MouseButton.Left:
                    return (PreviousMouseState.LeftButton == ButtonState.Pressed && CurrentMouseState.LeftButton == ButtonState.Released);
                case MouseButton.Middle:
                    return (PreviousMouseState.MiddleButton == ButtonState.Pressed && CurrentMouseState.MiddleButton == ButtonState.Released);
                case MouseButton.Right:
                    return (PreviousMouseState.RightButton == ButtonState.Pressed && CurrentMouseState.RightButton == ButtonState.Released);
                case MouseButton.XButton1:
                    return (PreviousMouseState.XButton1 == ButtonState.Pressed && CurrentMouseState.XButton1 == ButtonState.Released);
                case MouseButton.XButton2:
                    return (PreviousMouseState.XButton2 == ButtonState.Pressed && CurrentMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets whether the given button is currently pressed.
        /// </summary>
        /// <param name="button">The given button.</param>
        /// <returns>True if the button is currently pressed, false otherwise.</returns>
        public static bool GetButton(Buttons button)
        {
            return (PreviousGamePadState.IsButtonDown(button) && CurrentGamePadState.IsButtonDown(button));
        }

        /// <summary>
        /// Gets whether the given button was just pressed down.
        /// </summary>
        /// <param name="button">The given button.</param>
        /// <returns>True if the button was just pressed down, false otherwise.</returns>
        public static bool GetButtonDown(Buttons button)
        {
            return (PreviousGamePadState.IsButtonUp(button) && CurrentGamePadState.IsButtonDown(button));
        }

        /// <summary>
        /// Gets whether the given button was just released.
        /// </summary>
        /// <param name="button">The given button.</param>
        /// <returns>True if the button was just released, false otherwise.</returns>
        public static bool GetButtonDownUp(Buttons button)
        {
            return (PreviousGamePadState.IsButtonDown(button) && CurrentGamePadState.IsButtonUp(button));
        }

        /// <summary>
        /// Gets whether the given time span has passed since the given point in time.
        /// </summary>
        /// <param name="time">The given time span.</param>
        /// <param name="pointInTime">The given point in time.</param>
        /// <returns></returns>
        public static bool HasTimePassed(TimeSpan time, DateTime pointInTime)
        {
            return (DateTime.Now - pointInTime >= time);
        }
    }

    /// <summary>
    /// Represents the buttons of a typical mouse.
    /// </summary>
    public enum MouseButton : byte
    {
        Left, Middle, Right, XButton1, XButton2
    }
}
