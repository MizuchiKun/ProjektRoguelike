using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public class Screamer : Enemy
    {
        McTimer timer;

        public Screamer(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Screamersheet_front"),
               position,
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation,
               effect)
        {
            Speed = 2;
            timer = new McTimer(1000);

            Health = 1;
        }

        public override void Update()
        {
            base.Update();

            timer.UpdateTimer();
            if (timer.Test())
            {
                GameGlobals.PassProjectile(new EnemyAttack(Globals.Vector2ToDegrees(Level.Player.Position), Position, null, Globals.Vector2ToDegrees(Level.Player.Position), SpriteEffects.None));
                timer.ResetToZero();
            }
        }

        public override void ChangePosition()
        {
            //if you are not in range of the player, move towards them.
            if (Globals.GetDistance(Position, Level.Player.Position) >= 201)
            {
                Position += Globals.RadialMovement(Level.Player.Position, Position, Speed);
            }
            //if the player closes in, move away from them
            else if (Globals.GetDistance(Position, Level.Player.Position) < 199)
            {
                Position -= Globals.RadialMovement(Level.Player.Position, Position, Speed);
            }
        }

        public override void AI()
        {
            ChangePosition();

            if (Collides(Level.Player))
            {
                //player.Health--;
            }
        }
    }
}
