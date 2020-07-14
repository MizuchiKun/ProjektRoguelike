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
            speed = 2;
            timer = new McTimer(1000);
        }

        public override void Update()
        {
            base.Update();

            timer.UpdateTimer();
            if (timer.Test())
            {
                GameGlobals.PassProjectile(new EnemyShot(Globals.Vector2ToDegrees(PlayerPos), Position, null, Globals.Vector2ToDegrees(PlayerPos), SpriteEffects.None));
                timer.ResetToZero();
            }
        }

        public override void AI(Vector2 focus)
        {
            //if you are not in range of the player, move towards them.
            if (Globals.GetDistance(Position, player.Position) >= 201)
            {
                Position += Globals.RadialMovement(focus, Position, speed);
            }
            //if the player closes in, move away from them
            else if (Globals.GetDistance(Position, player.Position) < 199)
            {
                Position -= Globals.RadialMovement(focus, Position, speed);
            }
            
        }
    }
}
