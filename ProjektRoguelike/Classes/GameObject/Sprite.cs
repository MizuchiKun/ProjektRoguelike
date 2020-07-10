﻿using System;
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
                return new Rectangle(location: Position.ToPoint(),
                                     size: (SourceRectangle != null) 
                                           ? SourceRectangle.Value.Size 
                                           : new Point(Texture.Width, Texture.Height));
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
    }
}
