// STILL Sprite.Draw()!
    // Prevent shifting to the right, it might be the only problem...
    // OR JUST GET THE ROTATION TO WORK... >_<
// standard layout of room!
// Level, Room, etc. classes!
// and more...

using System;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A Sprite in 3D space.
    /// </summary>
    public class Sprite : GameObject
    {
        /// <summary>
        /// The texture of this <see cref="Sprite"/>.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The position of this <see cref="Sprite"/>.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The origin of this <see cref="Sprite"/>.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The rotation of this <see cref="Sprite"/>.
        /// </summary>
        public Vector3 Rotation { get; set; }

        /// <summary>
        /// The scale of this <see cref="Sprite"/>.
        /// </summary>
        public Vector2 Scales { get; set; }

        /// <summary>
        /// The source rectangle of this <see cref="Sprite"/>.
        /// </summary>
        public Rectangle? SourceRectangle { get; set; }

        /// <summary>
        /// The sprite effect of this <see cref="Sprite"/>.
        /// </summary>
        public SpriteEffects SpriteEffect { get; set; }

        /// <summary>
        /// Creates a new Sprite with the given graphical parameters.
        /// </summary>
        /// <param name="texture">The texure of this Sprite.</param>
        /// <param name="position">The position of this Sprite.</param>
        /// <param name="origin">
        /// An optional <b>relative</b> origin of this Sprite. <br></br>
        /// For example: (0, 0) is at the top-left, (1, 1) is at the bottom-right.<br></br>
        /// If null, it will use <see cref="Vector2.Zero"/>.
        /// </param>
        /// <param name="rotation">An optional rotation of this Sprite. If null, it will use <see cref="Vector3.Zero"/>.</param>
        /// <param name="scales">Optional scaling values for the x- and y-axis of this Sprite. If null, it will use <see cref="Vector2.One"/>.</param>
        /// <param name="sourceRectangle">An optional rotation of this Sprite (in degrees). If null, it will use <see cref="Vector3.Zero"/>.</param>
        /// <param name="spriteEffect">An optional sprite effect of this Sprite. <see cref="SpriteEffects.None"/> by default.</param>
        public Sprite(Texture2D texture, 
                      Vector3 position,
                      Vector2? origin = null,
                      Vector3? rotation = null,
                      Vector2? scales = null,
                      Rectangle? sourceRectangle = null,
                      SpriteEffects spriteEffect = SpriteEffects.None)
        {
            // Store the parameters.
            Texture = texture;
            Position = position;
            Origin = (origin != null) ? origin.Value : Vector2.Zero;
            Rotation = (rotation != null) ? rotation.Value : Vector3.Zero;
            Scales = (scales != null) ? scales.Value : Vector2.One;
            SourceRectangle = sourceRectangle;
            SpriteEffect = spriteEffect;
        }

        /// <summary>
        /// The update method of a <see cref="Sprite"/>. It's empty if not overriden.
        /// </summary>
        public override void Update()
        {
            float speed = 45;
            if (Globals.GetKey(Keys.A))
            {
                Rotation += new Vector3(0, 0, -speed) * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Globals.GetKey(Keys.D))
            {
                Rotation += new Vector3(0, 0, speed) * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Draws this Sprite with its current graphical parameters.
        /// </summary>
        public override void Draw()
        {
            // Get the base vertices (without scaling, rotating or origin-repositioning) and apply the source rectangle if not null.
            VertexPositionTexture[] spriteVertices = new VertexPositionTexture[6];

            // Their positions.
            Vector3 absOrigin = new Vector3(Origin * new Vector2(Texture.Width, -Texture.Height), 0f);
            spriteVertices[0].Position = (Position - absOrigin);
            spriteVertices[1].Position = (Position - absOrigin) + new Vector3(Texture.Width, 0, 0);
            spriteVertices[2].Position = (Position - absOrigin) + new Vector3(0, -Texture.Height, 0);
            spriteVertices[3].Position = (Position - absOrigin) + new Vector3(Texture.Width, -Texture.Height, 0);
            spriteVertices[4].Position = spriteVertices[1].Position;
            spriteVertices[5].Position = spriteVertices[2].Position;

            // Their texture coordinates.
            if (SourceRectangle != null)
            {
                Rectangle sourceRect = SourceRectangle.Value;
                spriteVertices[0].TextureCoordinate = sourceRect.Location.ToVector2() / new Vector2(Texture.Width, Texture.Height);
                spriteVertices[1].TextureCoordinate = (sourceRect.Location.ToVector2() + new Vector2(sourceRect.Width, 0)) / new Vector2(Texture.Width, Texture.Height);
                spriteVertices[2].TextureCoordinate = (sourceRect.Location.ToVector2() + new Vector2(0, sourceRect.Height)) / new Vector2(Texture.Width, Texture.Height);
                spriteVertices[3].TextureCoordinate = (sourceRect.Location + sourceRect.Size).ToVector2() / new Vector2(Texture.Width, Texture.Height);
                spriteVertices[4].TextureCoordinate = spriteVertices[1].TextureCoordinate;
                spriteVertices[5].TextureCoordinate = spriteVertices[2].TextureCoordinate;
            }
            else
            {
                spriteVertices[0].TextureCoordinate = new Vector2(0, 0);
                spriteVertices[1].TextureCoordinate = new Vector2(1, 0);
                spriteVertices[2].TextureCoordinate = new Vector2(0, 1);
                spriteVertices[3].TextureCoordinate = new Vector2(1, 1);
                spriteVertices[4].TextureCoordinate = spriteVertices[1].TextureCoordinate;
                spriteVertices[5].TextureCoordinate = spriteVertices[2].TextureCoordinate;
            }


            // Rotating and Scaling.
            // For every corner.
            for (byte i = 0; i < 4; i++)
            {
                // Rotate.
                Vector2 delta, planePos;
                Vector3 position = spriteVertices[i].Position;
                // X.
                delta = new Vector2(position.Y, position.Z) - new Vector2(Position.Y, Position.Z);
                planePos = delta.Length() * Globals.DegreesToVector2(Globals.Vector2ToDegrees(delta) + Rotation.X);
                position = new Vector3(position.X, planePos.X, planePos.Y);
                // Y.
                delta = new Vector2(position.X, position.Z) - new Vector2(Position.X, Position.Z);
                planePos = delta.Length() * Globals.DegreesToVector2(Globals.Vector2ToDegrees(delta) + Rotation.Y);
                position = new Vector3(planePos.X, position.Y, planePos.Y);
                // Z.
                delta = new Vector2(position.X, position.Y) - new Vector2(Position.X, Position.Y);
                planePos = delta.Length() * Globals.DegreesToVector2(Globals.Vector2ToDegrees(delta) + Rotation.Z);
                position = new Vector3(planePos.X, planePos.Y, position.Z);
                // Apply the new position.
                spriteVertices[i].Position = position;

                // Scale.
                spriteVertices[i].Position =
                    Vector3.Transform(spriteVertices[i].Position,
                                      Matrix.CreateScale(new Vector3(Scales, 1)));
            }


            // Update the 5th and 6th vertex position.
            spriteVertices[4].Position = spriteVertices[1].Position;
            spriteVertices[5].Position = spriteVertices[2].Position;


            // Draw the Sprite.
            // Apply the projection, view and world matrices.
            Globals.BasicEffect.Projection = Globals.Camera.Projection;
            Globals.BasicEffect.View = Globals.Camera.View;
            Globals.BasicEffect.World = Globals.Camera.World;

            // Apply the texture.
            Globals.BasicEffect.TextureEnabled = true;
            Globals.BasicEffect.Texture = Texture;

            // Apply BasicEffect's passes and draw the Sprite.
            foreach (EffectPass pass in Globals.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                Globals.Graphics.GraphicsDevice.DrawUserPrimitives(
                    PrimitiveType.TriangleList,
                    spriteVertices,
                    0,
                    2);
            }
        }
    }
}
 