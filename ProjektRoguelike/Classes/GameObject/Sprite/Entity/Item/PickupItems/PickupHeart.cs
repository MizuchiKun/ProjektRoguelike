using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public class PickupHeart : PickupItem
    {
        public PickupHeart(Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Pickups/PickupHeart"),
               position,
               sourceRectangle,
               rotation,
               effect)
        {  }

        // is made to increase players current healthpoints
        public override void Effect()
        {
            Level.Player.Health += 1;
        }
    }
}
