using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Represents an object of the game.
    /// </summary>
    public abstract class GameObject
    {
        public abstract Rectangle Hitbox { get; }

        public abstract void Update();
        public abstract void Draw();

        /// <summary>
        /// Gets whether this <see cref="GameObject"/> collides with the given other <see cref="GameObject"/>.
        /// </summary>
        /// <param name="otherGameObject">The other <see cref="GameObject"/>.</param>
        /// <returns>True if they collide, false otherwise.</returns>
        public virtual bool Collides(GameObject otherGameObject)
        {
            // They collide it their hitboxes intersect.
            return Hitbox.Intersects(otherGameObject.Hitbox);
        }

        /// <summary>
        /// Gets whether this <see cref="GameObject"/> collides with any of the given other <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="otherGameobjects">The other <see cref="GameObject"/>s.</param>
        /// <returns>True if it collides with any of them, false otherwise.</returns>
        public bool Collides(IEnumerable<GameObject> otherGameObjects)
        {
            // Check every Sprite.
            foreach (GameObject gameObject in otherGameObjects)
            {
                // If it collides with one of them.
                if (Collides(gameObject))
                {
                    // Return true.
                    return true;
                }
            }

            // It seemingly doesn't collide with any of them.
            return false;
        }

        /// <summary>
        /// Gets whether this <see cref="GameObject"/> collides with any of the given other <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="otherGameobjects">The other <see cref="GameObject"/>s.</param>
        /// <param name="collidingGameObjects">The out parameter that will contain a List of all <see cref="GameObject"/>s that collide with this one.</param>
        /// <returns>True if it collides with any of them, false otherwise.</returns>
        public bool Collides(IEnumerable<GameObject> otherGameObjects, out List<GameObject> collidingGameObjects)
        {
            // The return variables.
            List<GameObject> collidingObjects = new List<GameObject>();
            bool collides = false;
            
            // Check every Sprite.
            foreach (GameObject gameObject in otherGameObjects)
            {
                // If it collides with one of them.
                if (Collides(gameObject))
                {
                    // Add this GameObject.
                    collidingObjects.Add(gameObject);
                    // This GameObject collides with one of the given.
                    collides = true;
                }
            }

            // Return the return variables.
            collidingGameObjects = collidingObjects;
            return collides;
        }
        
        /// <summary>
        /// Get whether this <see cref="GameObject"/> touches one of the other <see cref="GameObject"/>s.
        /// </summary>
        /// <param name="otherGameObjects">The other <see cref="GameObject"/>s.</param>
        /// <returns>True if it touches (at least) one of the other <see cref="GameObject"/>s.</returns>
        public bool Touches(IEnumerable<GameObject> otherGameObjects)
        {
            foreach (GameObject gameObject in otherGameObjects)
            {
                if (Touches(gameObject))
                {
                    // It touches one of the objects.
                    return true;
                }
            }

            // It seems that it doesn't touch any of the given objects.
            return false;
        }

        /// <summary>
        /// Gets whether this <see cref="GameObject"/> touches the given other <see cref="GameObject"/>.
        /// </summary>
        /// <param name="otherGameObject">The other <see cref="GameObject"/>.</param>
        /// <returns>True if they are touching, false otherwise.</returns>
        public bool Touches(GameObject otherGameObject)
        {
            // Get an inflated copy of the GameObject's hitbox.
            Rectangle inflatedHitbox = Hitbox;
            inflatedHitbox.Location += new Point(-1);
            inflatedHitbox.Size += new Point(2);

            // It just touches if it isn't colliding unless this hitbox is inflated by 1.
            return (!Hitbox.Intersects(otherGameObject.Hitbox)
                    && inflatedHitbox.Intersects(otherGameObject.Hitbox));
        }
    }
}