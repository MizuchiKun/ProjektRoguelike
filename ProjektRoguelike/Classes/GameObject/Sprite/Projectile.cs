using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektRoguelike
{
    public class Projectile : Sprite
    {
        public int ownerID { get; set; }
        public float speed;
        public bool done;

        protected McTimer timer;

        protected Vector2 direction;


        public Projectile(Texture2D texture,
                      float angle,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effect = SpriteEffects.None)
        : base(texture: texture,
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: sourceRectangle,
               scale: Tile.Size / ((sourceRectangle != null) ? sourceRectangle.Value.Size.ToVector2() : texture.Bounds.Size.ToVector2()),
               rotation: rotation,
               effect: effect)
        {
            done = false;

            speed = 10;

            timer = new McTimer(1000);

            direction = Globals.DegreesToVector2(angle);
            direction.Normalize();
        }

        public override void Update()
        {
            timer.UpdateTimer();
            ChangePosition();
            if (timer.Test())
            {
                done = true;
            }
            if (HitWall())
            {
                done = true;
            }
            /*if (Collides(Level.CurrentRoom.Entities))
            {
                done = true;
            }*/
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

        public virtual void ChangePosition()
        {
            Position += speed * direction * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual bool HitWall()
        {
            // up
            if (Collides(Level.CurrentRoom.Walls[0])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[1]) || Collides(Level.CurrentRoom.Doors[3]))))
            {
                return true;
            }
            // right
            else if (Collides(Level.CurrentRoom.Walls[1])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[0]) || Collides(Level.CurrentRoom.Doors[2]))))
            {
                return true;
            }
            // down
            else if (Collides(Level.CurrentRoom.Walls[2])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[1]) || Collides(Level.CurrentRoom.Doors[3]))))
            {
                return true;
            }
            // left
            else if (Collides(Level.CurrentRoom.Walls[3])
                         || Collides(Level.CurrentRoom.Entities)
                         || ((Collides(Level.CurrentRoom.Doors[0]) || Collides(Level.CurrentRoom.Doors[2]))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
