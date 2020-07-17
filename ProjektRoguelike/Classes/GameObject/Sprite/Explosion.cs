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

        McTimer timer;

        public Explosion(Vector2? position = null,
                         float rotation = 0f,
                         SpriteEffects effects = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Effects/Explosionsheet"),
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: new Rectangle(0, 0, 256, 256),
               scale: Tile.Size / new Vector2(256),
               rotation: rotation,
               layerDepth: 1.0f,
               effects: effects)
        {
            // Set the animation.
            CurrentAnimation = new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Effects/Explosionsheet"),
                                             frameDimensions: new Vector2(256),
                                             frameDuration: TimeSpan.FromMilliseconds(100),
                                             repetitions: 0);

            // Restart the animation.
            CurrentAnimation.Restart();
            timer = new McTimer(300);
        }

        public override void Update()
        {
            timer.UpdateTimer();
            if (Collides(Level.Player))
            {
                Level.Player.GetHit(HitValue);
            }
            if (timer.Test())
            {
                Level.CurrentRoom.Remove(this);
            }
        }
    }
}
