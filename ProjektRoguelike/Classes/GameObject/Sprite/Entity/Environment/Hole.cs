using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    
    /// <summary>
    /// An enemy
    /// </summary>
    public class Hole : Environment
    {
        public Hole(Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Environment/Hole"),
               position,
               sourceRectangle,
               rotation,
               effect)
        {
            Health = 10000;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
