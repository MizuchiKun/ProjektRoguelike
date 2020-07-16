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
        }

        public override void Update()
        {
            base.Update();
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

        private bool HitWall()
        {
            // up
            if (Collides(Level.CurrentRoom.Walls[0]))

            {
                return true;
            }
            // right
            else if (Collides(Level.CurrentRoom.Walls[1]))
            {
                return true;
            }
            // down
            else if (Collides(Level.CurrentRoom.Walls[2]))
            {
                return true;
            }
            // left
            else if (Collides(Level.CurrentRoom.Walls[3]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
