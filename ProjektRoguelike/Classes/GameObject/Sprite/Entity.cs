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
        public override Rectangle Hitbox
        {
            get
            {
                // Hitbox is bottom half of sprite.
                Vector2 size = Texture.Bounds.Size.ToVector2();
                return new Rectangle(location: (Position + new Vector2(0.5f, 0) * size).ToPoint(),
                                     size: (new Vector2(0.5f, 0) * size).ToPoint());
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
               scale: Tile.Size / new Vector2(texture.Width, texture.Height),
               rotation: rotation,
               layerDepth: 1.0f,
               effect: effect)
        { }
    }
}
