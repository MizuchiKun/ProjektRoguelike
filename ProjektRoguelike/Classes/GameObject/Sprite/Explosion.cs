using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    class Explosion : Sprite
    {
        public int HitValue { get; } = 2;

        public Explosion(Vector2? position = null,
                         float rotation = 0f,
                         SpriteEffects effects = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Effects/Explosionsheet"),
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               rotation: rotation,
               layerDepth: 1.0f,
               effects: effects)
        {  }

        public override void Update()
        {
            base.Update();

            // Do the Animation

            if (Collides(Level.Player))
            {
                Level.Player.GetHit(HitValue);
            }
        }
    }
}
