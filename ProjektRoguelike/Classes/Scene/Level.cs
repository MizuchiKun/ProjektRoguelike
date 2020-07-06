using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// The Main Menu.
    /// </summary>
    public class Level : Scene
    {
        /// <summary>
        /// The Tilesheet with the floor and wall tiles.
        /// </summary>
        protected byte _levelIndex;

        /// <summary>
        /// Creates a Level.
        /// </summary>
        /// <param name="levelIndex">The index of the level you want to create.</param>
        public Level(/*byte levelIndex*/)
        {
            // Store the parameters.
            //_levelIndex = levelIndex;

            //TEST STUFF
            _gameObjects.Add(new Sprite(texture: Globals.Content.Load<Texture2D>("Sprites/test"),
                                        position: Vector3.Zero,
                                        origin: new Vector2(0.5f)));
        }
    }
}
