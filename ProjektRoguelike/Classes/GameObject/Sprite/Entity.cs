using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Represents an entity.<br></br>
    /// (Entities include the Player, Enemies, Chest and other interactable objects.)
    /// </summary>
    public class Entity : Sprite
    {
        /// <summary>
        /// The hitbox of this <see cref="Entity"/>.
        /// </summary>
        public override Rectangle Hitbox
        {
            get
            {
                // Hitbox is bottom half of sprite.
                Vector2 actualSize = ((SourceRectangle != null)
                                     ? SourceRectangle.Value.Size.ToVector2()
                                     : Texture.Bounds.Size.ToVector2())
                                     * Scale;
                Vector2 absOrigin = Origin * actualSize;
                return new Rectangle(location: ((Position - absOrigin) + new Vector2(0f, 0.5f) * actualSize).ToPoint(),
                                     size: (new Vector2(1f, 0.5f) * actualSize).ToPoint());
            }
        }

        /// <summary>
        /// Creates an Entity with the given graphical parameters.
        /// </summary>
        /// <param name="texture">Its texture.</param>
        /// <param name="position">Its position. <br></br>If null, it will be <see cref="Vector2.Zero"/>.</param>
        /// <param name="sourceRectangle">Its source rectangle. <br></br>If null, the whole texture will be drawn.</param>
        /// <param name="layerDepth">Its layer depth. <br></br>It's 0 by default.</param>
        /// <param name="effect">Its sprite effect. <br></br>It's <see cref="SpriteEffects.None"/> by default.</param>
        public Entity(Texture2D texture,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effect = SpriteEffects.None)
        : base(texture: texture,
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: sourceRectangle,
               scale: Tile.Size / ((sourceRectangle != null) ? sourceRectangle.Value.Size.ToVector2() : texture.Bounds.Size.ToVector2()),
               rotation: rotation,
               effect: effect)
        { }

        public override void Update()
        {
            // Update code.
            //...

            // Update the Layer.
            //Layer = 
        }

        public virtual void GetHit(Enemy enemy, int hitValue)
        {
            if (Collides(enemy))
            {
                //enemy.health -= hitValue;
            }
        }
    }
}
