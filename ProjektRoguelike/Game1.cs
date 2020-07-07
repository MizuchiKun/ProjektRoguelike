﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public Game1()
        {
            // Store yourself in Globals.
            Globals.Game1 = this;

            // Set Global's Graphics and Content.
            Globals.Content = Content;
            Globals.Content.RootDirectory = "Content";

            // Create the GraphicsDeviceManager.
            Globals.Graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set the GraphicsDevice's SamplerState and RasterizerState.
            //Globals.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            Globals.Graphics.GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.None };

            // Initialize the Camera.
            Globals.Camera = new Camera(new Vector3(0, 0, 0), Vector3.Forward);

            // Initialize the BasicEffect.
            Globals.BasicEffect = new BasicEffect(Globals.Graphics.GraphicsDevice);

            // Set the initial Scene.
            Globals.CurrentScene = new Level();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() { }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() { }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Save the current GameTime and states.
            Globals.GameTime = gameTime;
            Globals.CurrentKeyboardState = Keyboard.GetState();
            Globals.CurrentMouseState = Mouse.GetState();
            Globals.CurrentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Call the current Scene's Update method.
            Globals.CurrentScene.Update();

            // Save the current states as the previous states (so they will be accessable as previous states in the next Update()).
            Globals.PreviousKeyboardState = Globals.CurrentKeyboardState;
            Globals.PreviousMouseState = Globals.CurrentMouseState;
            Globals.PreviousGamePadState = Globals.CurrentGamePadState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the GraphicsDevice.
            Globals.Graphics.GraphicsDevice.Clear(Color.Navy);

            // Call the current Scene's Draw method.
            Globals.CurrentScene.Draw();

            base.Draw(gameTime);
        }
    }
}
