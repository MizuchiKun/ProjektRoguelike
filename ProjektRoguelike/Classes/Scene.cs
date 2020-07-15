using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A container for <see cref="GameObject"/>s to update and draw to the screen.
    /// </summary>
    public abstract class Scene
    {
        private UI ui = new UI();
        /// <summary>
        /// This Scene's <see cref="GameObject"/>s.
        /// </summary>
        protected List<GameObject> _gameObjects;

        public List<Projectile> projectiles = new List<Projectile>();

        /// <summary>
        /// Creates a new <see cref="Scene"/> object with the given <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="gameObjects">
        /// The given List of <see cref="GameObject"/>s which will be added to the Scene.<br></br>
        /// If null, it will be set to an empty List of <see cref="GameObject"/>s.
        /// </param>
        public Scene(List<GameObject> gameObjects = null)
        {
            // Store the parameters.
            _gameObjects = (gameObjects != null) ? gameObjects : new List<GameObject>();

            GameGlobals.PassProjectile = AddProjectile;
        }

        /// <summary>
        /// Calls the <see cref="Scene"/>'s <see cref="GameObject"/>s' Update() methods.
        /// </summary>
        public virtual void Update()
        {
            // Call your GameObjects' Update() methods.
            foreach (GameObject gameObject in _gameObjects)
            {
                    gameObject.Update();
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(Level.CurrentRoom.Enemies);

                if (projectiles[i].Done)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Calls the <see cref="Scene"/>'s <see cref="GameObject"/>s' Draw() methods.
        /// </summary>
        public void Draw()
        {
            // Call your GameObjects' Draw() methods.
            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Draw();
            }

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw();
            }
        }

        public virtual void AddProjectile(object info)
        {
            projectiles.Add((Projectile)info);
        }
    }
}
