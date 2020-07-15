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
        /// <param name="effects">Its sprite effects. <br></br>It's <see cref="SpriteEffects.None"/> by default.</param>
        public Entity(Texture2D texture,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effects = SpriteEffects.None)
        : base(texture: texture,
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: sourceRectangle,
               scale: Tile.Size / ((sourceRectangle != null) ? sourceRectangle.Value.Size.ToVector2() : texture.Bounds.Size.ToVector2()),
               rotation: rotation,
               effects: effects)
        { }

        public override void Update()
        {
            // Update code.
            //...

            // Update the Layer.
            Layer = 0.9f - (Position.Y / 10e6f);
        }

        /// <summary>
        /// Moves the Entity with the given velocity if possible.
        /// </summary>
        /// <param name="velocity">The given velocity.</param>
        protected void Move(Vector2 velocity)
        {
            // Move horizontally.
            // If it moves right.
            if (velocity.X > 0)
            {
                Move(1, (float)Math.Abs(velocity.X));
            }
            // Else it moves right.
            else
            {
                Move(3, (float)Math.Abs(velocity.X));
            }

            // Move vertically.
            // If it moves down.
            if (velocity.Y > 0)
            {
                Move(2, (float)Math.Abs(velocity.Y));
            }
            // Else it moves up.
            else
            {
                Move(0, (float)Math.Abs(velocity.Y));
            }
        }

        /// <summary>
        /// Moves the Entity in the given direction with the given speed if possible.
        /// </summary>
        /// <param name="direciton">
        /// The direction in which the Entity shall move.<br></br>
        /// (0=up, 1=right, 2=down, 3=left)
        /// </param>
        /// <param name="speed">The given speed.</param>
        protected virtual void Move(byte direction, float speed)
        {
            // Get the Vector of the direction.
            Vector2 directionVector = Vector2.Zero;
            switch (direction)
            {
                // Up.
                case 0:
                    directionVector = -Vector2.UnitY;
                    break;
                // Right.
                case 1:
                    directionVector = Vector2.UnitX;
                    break;
                // Down.
                case 2:
                    directionVector = Vector2.UnitY;
                    break;
                // Left.
                case 3:
                    directionVector = -Vector2.UnitX;
                    break;

            }

            // Move this Sprite.
            Position += directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

            // If it collides with the top wall or another Entity.
            if (Collides(Level.CurrentRoom.Walls[direction])
                || Collides(Level.CurrentRoom.Entities)
                || Collides(Level.Player))
            {
                // It's not allowed to move up.
                // Revert its position.
                Position -= directionVector * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
