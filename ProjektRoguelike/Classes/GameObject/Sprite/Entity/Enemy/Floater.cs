using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An enemy
    /// </summary>
    public class Floater : Enemy
    {
        public Floater(Vector2? position = null,
                     float rotation = 0f,
                     SpriteEffects effect = SpriteEffects.None)

        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Enemies/Floatersheet_front"),
               position,
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation,
               effect)
        {
            Speed = 2f;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
