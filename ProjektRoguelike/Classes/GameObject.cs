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
        public bool Collides(GameObject otherGameObject)
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
        /// Gets whether this <see cref="Sprite"/> is just touching the given other <see cref="Sprite"/>.
        /// </summary>
        /// <param name="otherSprite">The other <see cref="Sprite"/>.</param>
        /// <returns>True if they are just touching, false otherwise.</returns>
        public bool Touches(Sprite otherSprite)
        {
            // Get an inflated copy of this Sprite's hitbox.
            Rectangle inflatedHitbox = Hitbox;
            inflatedHitbox.Location += new Point(-1);
            inflatedHitbox.Size += new Point(1);

            // It just touches if it isn't colliding unless this hitbox is inflated by 1.
            return (!Hitbox.Intersects(otherSprite.Hitbox)
                    && inflatedHitbox.Intersects(otherSprite.Hitbox));
        }
    }
}