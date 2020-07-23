using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    class Bomb : NeutralDamage
    {
        McTimer timer;

        float angle, speed;
        Vector2 direction;

        private const float _maxSpeed = 25;
        private const float _speedIncrease = 10;

        public Bomb(Vector2? position = null,
                    Rectangle? sourceRectangle = null,
                    float rotation = 0f,
                    SpriteEffects effects = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Pickups/Bomb"),
               position: position,
               sourceRectangle: sourceRectangle,
               rotation: rotation,
               effects: effects)
        {
            Health = 3;
            HitValue = 1;

            speed = 250f;

            timer = new McTimer(1500);

            angle = Globals.Vector2ToDegrees(Level.Player.Position - Position);
            direction = Globals.DegreesToVector2(-angle);
        }

        public override void Update()
        {
            timer.UpdateTimer();
            if (timer.Test())
            {
                Level.CurrentRoom.Add(new Explosion(Position));
                Level.CurrentRoom.Remove(this);
            }
            if (Collides(Level.Player))
            {
                if (speed < _maxSpeed)
                {
                    speed += _speedIncrease;
                }
                Position += speed * direction * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
            }
            if (speed > 0)
            {
                speed -= _speedIncrease;
            }

            base.Update();
        }
    }
}
