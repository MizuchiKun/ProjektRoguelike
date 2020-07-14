﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    public class BasicAttack : Projectile
    {

        public BasicAttack(float angle,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Pickups/Key"),
               angle: angle,
               position: position,
               sourceRectangle: sourceRectangle,
               rotation: rotation,
               effect: effect)
        {
            done = false;

            speed = 250.0f;

            timer = new McTimer(1000);

            direction = Globals.DegreesToVector2(angle);
            direction.Normalize();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
