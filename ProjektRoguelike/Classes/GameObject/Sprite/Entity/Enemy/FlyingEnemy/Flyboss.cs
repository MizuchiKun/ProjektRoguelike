using System;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An Bossenemy
    /// </summary>
    public class Flyboss : FlyingEnemy
    {
        McTimer AttackTimer, SpawnTimer, MovementTimer;

        const float _maxSpeed = 145;
        const float _speedIncrease = 5;

        Vector2 direction;

        bool bla = true;

        public override Rectangle Hitbox
        {
            get
            {
                Vector2 actualSize = ((SourceRectangle != null)
                                     ? SourceRectangle.Value.Size.ToVector2()
                                     : Texture.Bounds.Size.ToVector2())
                                     * Scale * Globals.Scale;
                Vector2 absOrigin = Origin * actualSize;
                return new Rectangle(location: ((Position - absOrigin) + new Vector2(0.25f, 0.85f) * actualSize).ToPoint(),
                                     size: (new Vector2(0.5f, 0.15f) * actualSize).ToPoint());
            }
        }

        public Flyboss(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Flyboss_wo_shadow"),
               position,
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation,
               effect)
        {
            Speed = 2f;
            Health = 50;

            Scale = new Vector2(1f);

            AttackTimer = new McTimer(1500);
            SpawnTimer = new McTimer(3000);
            MovementTimer = new McTimer(4000);

            direction = Globals.DegreesToVector2(0);

            HitValue = 0;

            // Set the _shadowSprite.
            _shadowSprite.Scale = Scale;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void AI()
        {
            base.AI();

            Attack();
        }

        public override void ChangePosition()
        {

            // DIRECTIONAL LOOP
            MovementTimer.UpdateTimer();
            if (MovementTimer.Test())
            {
                // used to easily change the direction, in which the unit moves
                bla = !bla;
                switch (bla)
                {
                    case true:
                        direction = Globals.DegreesToVector2(0);
                        break;
                    case false:
                        direction = Globals.DegreesToVector2(180);
                        break;
                }
                // to restart the directional loop
                MovementTimer.ResetToZero();
            }

            // SPEED LOOP
            // speed up if youre not at maxspeed yet
            if (Speed < _maxSpeed)
            {
                Speed += _speedIncrease;
            }
            // slow down if you are at or higher than the speedlimit
            if (Speed >= _maxSpeed)
            {
                // if you have not stopped yet, slow down
                if (Speed > 0)
                {
                    Speed -= _speedIncrease;
                }
            }

            // move the unit
            Position += direction * Speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Attack()
        {
            // FLYSPAWN LOOP
            SpawnTimer.UpdateTimer();
            // Every X seconds, spawn some enemies.
            if (SpawnTimer.Test())
            {
                // Spawn 3 flies
                for (int i = 0; i < 3; i++)
                {
                    Level.CurrentRoom.Add(new Fly(new Vector2(Position.X - 25 + (25 * i), Position.Y)));
                }
                // Restart the thing.
                SpawnTimer.ResetToZero();
            }

            // ATTACK LOOP
            AttackTimer.UpdateTimer();
            // Every X seconds, attack the player
            if (AttackTimer.Test())
            {
                // Spawn 3 projectiles
                for (int i = 0; i < 3; i++)
                {
                    Level.CurrentRoom.Add(new EnemyAttack(Globals.Vector2ToDegrees(Level.Player.Position - Position) - 25 + (i * 25), Position));
                }
                // Restart the thing.
                AttackTimer.ResetToZero();
            }
        }
    }
}
