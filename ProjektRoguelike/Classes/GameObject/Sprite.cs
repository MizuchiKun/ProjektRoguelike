using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A Sprite in 2D space.
    /// </summary>
    public class Sprite : GameObject
    {
        /// <summary>
        /// This <see cref="Sprite"/>'s texture.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// This <see cref="Sprite"/>'s position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// This <see cref="Sprite"/>'s relative origin.
        /// </summary>
        public Vector2 Origin { get; }

        /// <summary>
        /// This <see cref="Sprite"/>'s source rectangle.
        /// </summary>
        public Rectangle? SourceRectangle { get; }

        /// <summary>
        /// This <see cref="Sprite"/>'s scale factors.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// This <see cref="Sprite"/>'s rotation (in degrees).
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// This <see cref="Sprite"/>'s layer depth.
        /// </summary>
        public float Layer { get; set; }

        /// <summary>
        /// This <see cref="Sprite"/>'s colour.
        /// </summary>
        public Color Colour { get; }

        /// <summary>
        /// This <see cref="Sprite"/>'s sprite effect.
        /// </summary>
        public SpriteEffects Effect { get; }

        /// <summary>
        /// The hitbox of this <see cref="Sprite"/>.
        /// </summary>
        public virtual Rectangle Hitbox
        {
            get
            {
                Vector2 actualSize = ((SourceRectangle != null)
                                     ? SourceRectangle.Value.Size.ToVector2()
                                     : Texture.Bounds.Size.ToVector2())
                                     * Scale;
                Vector2 absOrigin = Origin * actualSize;
                return new Rectangle(location: (Position - absOrigin).ToPoint(),
                                     size: actualSize.ToPoint());
            }
        }

        /// <summary>
        /// Creates a new Sprite with the given graphical parameters.
        /// </summary>
        /// <param name="texture">Its texture.</param>
        /// <param name="position">Its position. <br></br>If null, it will be <see cref="Vector2.Zero"/>.</param>
        /// <param name="origin">
        /// Its relative origin. <br></br>
        /// It's relative to the <see cref="Sprite"/>'s dimensions.<br></br>
        /// E.g. (0.5, 0.5) corresponds to any <see cref="Sprite"/>'s centre.
        /// </param>
        /// <param name="sourceRectangle">Its source rectangle. <br></br>If null, the whole texture will be drawn.</param>
        /// <param name="scale">Its scale factors. <br></br>If null, it will be <see cref="Vector2.One"/>.</param>
        /// <param name="rotation">Its rotation in degrees. <br></br>It's 0 by default.</param>
        /// <param name="layerDepth">Its layer depth. <br></br>It's 0 by default.</param>
        /// <param name="colour">Its colour. <br></br>If null, it will be <see cref="Color.White"/>.</param>
        /// <param name="effect">Its sprite effect. <br></br>It's <see cref="SpriteEffects.None"/> by default.</param>
        public Sprite(Texture2D texture,
                      Vector2? position = null,
                      Vector2? origin = null,
                      Rectangle? sourceRectangle = null,
                      Vector2? scale = null,
                      float rotation = 0f,
                      float layerDepth = 0f,
                      Color? colour = null,
                      SpriteEffects effect = SpriteEffects.None)
        {
            // Store the parameters.
            Texture = texture;
            Position = (position != null) ? position.Value : Vector2.Zero;
            Origin = (origin != null) ? origin.Value : Vector2.Zero;
            SourceRectangle = sourceRectangle;
            Scale = (scale != null) ? scale.Value : Vector2.One;
            Rotation = rotation;
            Layer = layerDepth;
            Colour = (colour != null) ? colour.Value : Color.White;
            Effect = effect;
        }

        /// <summary>
        /// A <see cref="Sprite"/>'s Update method.<br></br>
        /// Is empty if not overriden.
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Draws the <see cref="Sprite"/> with its current graphical parameters.
        /// </summary>
        public override void Draw()
        {
            // Draw the Sprite with its current graphical parameters.
            Globals.SpriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: SourceRectangle,
                color: Colour,
                rotation: MathHelper.ToRadians(Rotation),
                origin: Origin * ((SourceRectangle != null) ? SourceRectangle.Value.Size.ToVector2() : Texture.Bounds.Size.ToVector2()),
                scale: Scale,
                effects: Effect,
                layerDepth: Layer);
        }

        /// <summary>
        /// Gets whether this <see cref="Sprite"/> collides with the given other <see cref="Sprite"/>.
        /// </summary>
        /// <param name="otherSprite">The other <see cref="Sprite"/>.</param>
        /// <returns>True if they collide, false otherwise.</returns>
        public bool Collides (Sprite otherSprite)
        {
            // They collide it their hitboxes intersect.
            return Hitbox.Intersects(otherSprite.Hitbox);
        }

        /// <summary>
        /// Gets whether this <see cref="Sprite"/> collides with any of the given other <see cref="Sprite"/>s.
        /// </summary>
        /// <param name="otherSprites">The other <see cref="Sprite"/>s.</param>
        /// <returns>True if it collides with any of them, false otherwise.</returns>
        public bool Collides(IEnumerable<Sprite> otherSprites)
        {
            // Check every Sprite.
            foreach (Sprite sprite in otherSprites)
            {
                // If it collides with one of them.
                if (Collides(sprite))
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
        public bool Touches (Sprite otherSprite)
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
