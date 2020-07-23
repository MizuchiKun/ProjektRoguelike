using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An item
    /// </summary>
    public abstract class PickupItem : Item
    {
        Vector2 direction;

        float speed, angle;
        const float _maxSpeed = 25;
        const float _speedIncrease = 10;

        public PickupItem(Texture2D texture,
                     Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture,
               position,
               sourceRectangle,
               rotation,
               effect)
        {
            angle = Globals.Vector2ToDegrees(Level.Player.Position - Position);
            direction = Globals.DegreesToVector2(-angle);

            speed = 250f;
        }

        public override void Update()
        {
            base.Update();

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
        }

        /// <summary>
        /// The effect that aquiring the item has on the player. 
        /// Removing the item mustnt be done in this method.
        /// </summary>
        public override void Effect() { }
    }
}
