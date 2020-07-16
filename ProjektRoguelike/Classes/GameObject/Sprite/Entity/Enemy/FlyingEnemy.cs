using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An flying enemy
    /// </summary>
    public class FlyingEnemy : Enemy
    {
        public FlyingEnemy(Texture2D texture,
                           Vector2? position = null,
                           Rectangle? sourceRectangle = null,
                           float rotation = 0f,
                           SpriteEffects effect = SpriteEffects.None)
        : base(texture,
               position,
               sourceRectangle,
               rotation,
               effect)
        {  }
    }
}
