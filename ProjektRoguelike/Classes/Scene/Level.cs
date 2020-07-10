using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A level.
    /// </summary>
    public class Level : Scene
    {
        /// <summary>
        /// The Tilesheet with the floor and wall tiles.
        /// </summary>
        protected byte _levelIndex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
                        private Sprite _hero;

        /// <summary>
        /// Creates a Level.
        /// </summary>
        /// <param name="levelIndex">The index of the level you want to create.</param>
        public Level(byte levelIndex)
        {
            // Store the parameters.
            _levelIndex = levelIndex;

            // Set the camera's position.
            Globals.Camera.Position = new Vector3(1024, -150, 640);
            Globals.Camera.LookAtAngles = new Vector2(0, -60);

            //generate the level / rooms


            // roomTESTTESTTEST
            short spriteWidth = 256;
            //ground
            Texture2D ground = Globals.Content.Load<Texture2D>("Sprites/Boden");
            for (byte x = 0; x < 8; x++)
            {
                for (byte y = 0; y < 6; y++)
                {
                    _gameObjects.Add(new Sprite(ground,
                                                new Vector3(x * spriteWidth, y * spriteWidth, 0),
                                                sourceRectangle: new Rectangle(0, 0, spriteWidth, spriteWidth)));
                }
            }
            //wall
            Texture2D wall = Globals.Content.Load<Texture2D>("Sprites/Wand");
            //top&bottom
            for (byte x = 0; x < 8; x++)
            {
                //top
                _gameObjects.Add(new Sprite(wall,
                                            new Vector3(x * spriteWidth, 5 * spriteWidth, 0),
                                            new Vector2(0.0f, 1.0f),
                                            new Vector3(90, 0, 0)));
                //bottom
                _gameObjects.Add(new Sprite(wall,
                                            new Vector3(x * spriteWidth, -spriteWidth, 0),
                                            new Vector2(1.0f),
                                            new Vector3(-90, 0, 180)));
            }
            //left&right
            for (byte y = 0; y < 6; y++)
            {
                //left
                _gameObjects.Add(new Sprite(wall,
                                            new Vector3(0, y * spriteWidth, 0),
                                            new Vector2(1.0f, 1.0f),
                                            new Vector3(0, 90, 90)));
                //right
                _gameObjects.Add(new Sprite(wall,
                                            new Vector3(8 * spriteWidth, y * spriteWidth, 0),
                                            new Vector2(0.0f, 1.0f),
                                            new Vector3(0, -90, -90)));
            }
            _hero = new Sprite(Globals.Content.Load<Texture2D>("Sprites/Herosheet_front"),
                               new Vector3(4 * spriteWidth, 2 * spriteWidth, 0),
                               new Vector2(0.5f, 1.0f),
                               new Vector3(45, 0, 0),
                               sourceRectangle: new Rectangle(0, 0, spriteWidth, spriteWidth));
            _gameObjects.Add(_hero);
        }

        public override void Update()
        {
            Vector3 position = _hero.Position;
            short movementSpeed = 512;
            //movement
            //x
            if (Globals.GetKey(Keys.A))
                position.X -= movementSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            if (Globals.GetKey(Keys.D))
                position.X += movementSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            //y
            if (Globals.GetKey(Keys.W))
                position.Y += movementSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            if (Globals.GetKey(Keys.S))
                position.Y -= movementSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            ////z
            //if (Globals.GetKey(Keys.Space))
            //    position.Z += movementSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            //if (Globals.GetKey(Keys.LeftShift))
            //    position.Z -= movementSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            _hero.Position = position;
            //looking
            //Vector2 lookAtAngles = Globals.Camera.LookAtAngles;
            //byte rotationSpeed = 60;
            ////horizontal
            //if (Globals.GetKey(Keys.Left))
            //    lookAtAngles.X += rotationSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            //if (Globals.GetKey(Keys.Right))
            //    lookAtAngles.X -= rotationSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            ////vertical
            //if (Globals.GetKey(Keys.Up))
            //    lookAtAngles.Y += rotationSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            //if (Globals.GetKey(Keys.Down))
            //    lookAtAngles.Y -= rotationSpeed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            //Globals.Camera.LookAtAngles = lookAtAngles;

            base.Update();
        }
    }
}
