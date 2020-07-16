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
        public int OwnerID { get; set; }
        protected int HitValue;
        public float Speed;

        protected McTimer timer;

        protected Vector2 Direction;



        public Projectile(Texture2D texture,
                      Vector2? position = null,
                      Rectangle? sourceRectangle = null,
                      float rotation = 0f,
                      SpriteEffects effects = SpriteEffects.None)
        : base(texture: texture,
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: sourceRectangle,
               scale: Tile.Size / ((sourceRectangle != null) ? sourceRectangle.Value.Size.ToVector2() : texture.Bounds.Size.ToVector2()),
               rotation: rotation,
               effects: effects)
        {
            Speed = 10;

            Direction = Vector2.Zero;
            timer = new McTimer(1000);
        }

        public override void Update()
        {
            Update(Level.CurrentRoom.Enemies);
            base.Update();
        }

        private void Update(List<Enemy> enemies)
        {
            timer.UpdateTimer();
            ChangePosition();
            if (timer.Test())
            {
                Level.CurrentRoom.Remove(this);
            }
            if (HitWall())
            {
                Level.CurrentRoom.Remove(this);
            }
            if (Collides(Level.Player) && (OwnerID == 2 || OwnerID == 0))
            {
                Level.Player.GetHit(HitValue);
                Level.CurrentRoom.Remove(this);
            }
            if (Collides(enemies) && (OwnerID == 1 || OwnerID == 0))
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (OwnerID == 1)
                    {
                        //enemies[i].GetHit(HitValue);
                        Level.CurrentRoom.Enemies[i].GetHit(HitValue);
                        Level.CurrentRoom.Remove(this);
                    }
                }
            }
        }
        public virtual void ChangePosition()
        {
            Position += Speed * Direction * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual bool HitWall()
        {
            // up
            if (Collides(Level.CurrentRoom.Walls[0]))

            {
                return true;
            }
            // right
            else if (Collides(Level.CurrentRoom.Walls[1]))
            {
                return true;
            }
            // down
            else if (Collides(Level.CurrentRoom.Walls[2]))
            {
                return true;
            }
            // left
            else if (Collides(Level.CurrentRoom.Walls[3]))
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
