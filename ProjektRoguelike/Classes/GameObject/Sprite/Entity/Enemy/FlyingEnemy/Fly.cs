using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public class Fly : FlyingEnemy
    {
        public Fly(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Flysheet"),
               position,
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation,
               effect)
        {
            Speed = 2f;
            Health = 3;

            // Set the animation.
            CurrentAnimation = new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Enemies/FLysheet"),
                                             frameDimensions: new Vector2(256),
                                             frameDuration: TimeSpan.FromSeconds(1f / 60f));
        }

        public override void Update()
        {
            // This whole Update() method can be removed (if you don't plan on adding something to it).
            //base.Update();
        }

        public override void ChangePosition()
        {
            // Fly decides to turn around if it hits a wall
            if (HitWall())
            {
                Speed = -Speed;
            }
            Position += new Vector2(Position.X + Speed, Position.Y + (Speed / 4));
        }

        public override void AI()
        {
            base.AI();

            if (Collides(Level.Player))
            {
                Level.Player.GetHit(1);
            }
        }
    }
}
