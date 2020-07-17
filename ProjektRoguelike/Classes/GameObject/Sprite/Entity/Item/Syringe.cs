﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public abstract class Syringe : Item
    {
        public Syringe(Vector2? position = null,
                     Rectangle? sourceRectangle = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Items/Syringe"),
               position,
               sourceRectangle,
               rotation,
               effect)
        {  }
        public override void Effect()
        {
            Level.Player.speed *= 3;
        }
    }
}
