// Sprite.Draw()!
// standard layout of room!
// Level, Room, etc. classes!
// and more...

using System;
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
        public Vector2? Origin { get; set; }

        /// <summary>
        /// The rotation of this <see cref="Sprite"/>.
        /// </summary>
        public Vector3? Rotation { get; set; }

        /// <summary>
        /// The scale of this <see cref="Sprite"/>.
        /// </summary>
        public Vector3? Scale { get; set; }

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
        /// If null, it will be <see cref="Vector2.Zero"/>.
        /// </param>
        /// <param name="rotation">An optional rotation of this Sprite. If null, it will be <see cref="Vector3.Zero"/>.</param>
        /// <param name="sourceRectangle">An optional rotation of this Sprite (in degrees). If null, it will be <see cref="Vector3.Zero"/>.</param>
        /// <param name="spriteEffect">An optional sprite effect of this Sprite. <see cref="SpriteEffects.None"/> by default.</param>
        public Sprite(Texture2D texture, 
                      Vector3 position,
                      Vector2? origin = null,
                      Vector3? rotation = null,
                      Rectangle? sourceRectangle = null,
                      SpriteEffects spriteEffect = SpriteEffects.None)
        {
            // Store the parameters.
            Texture = texture;
            Position = position;
            Origin = origin;
            Rotation = rotation;
            SourceRectangle = sourceRectangle;
            SpriteEffect = spriteEffect;
        }

        /// <summary>
        /// The update method of a <see cref="Sprite"/>. It's empty if not overriden.
        /// </summary>
        public override void Update()
        {

        }

        /// <summary>
        /// Draws this Sprite with its current graphical parameters.
        /// </summary>
        public override void Draw()
        {
            // Get the base vertices (without scaling, rotating or origin-repositioning) and apply the source rectangle if not null.
            VertexPositionTexture[] spriteVertices = new VertexPositionTexture[6];

            // Their positions.
            Vector3 origin = ((Origin != null) ? new Vector3(Origin.Value * new Vector2(Texture.Width, -Texture.Height), 0f) : Vector3.Zero);
            spriteVertices[0].Position = (Position - origin);
            spriteVertices[1].Position = (Position - origin) + new Vector3(Texture.Width, 0, 0);
            spriteVertices[2].Position = (Position - origin) + new Vector3(0, -Texture.Height, 0);
            spriteVertices[3].Position = (Position - origin) + new Vector3(Texture.Width, -Texture.Height, 0);
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


            // Rotating.
            //loop through the first 4 spriteVertices
            //Matrix.CreateRotation()??
            //get distance by deltaPos
            //DegreesToVector3(deltaPos)
            //add Rotation to above result
            //calculate new Vector3 in the new direction, at the calculated distance
            //update the 5th and 6th spriteVertices
            //...


            // Scaling.
            //loop through first 4 spriteVertices
            //Matrix.CreateScale()??
            //get delta to Position (vertexposition - Position)
            //new vertexposition is (Position + delta * Scale)
            //update the 5th and 6th spriteVertices
            //...


            // Apply the matrices.
            Globals.BasicEffect.View = Globals.Camera.View;
            Globals.BasicEffect.Projection = Globals.Camera.Projection;

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
 