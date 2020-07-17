using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An environmental object
    /// </summary>
    public abstract class Environment : Entity
    {
        Random rand = new Random();
        protected int Dropnumber;

        public Environment(Texture2D texture,
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

        public override void Update()
        {
            base.Update();

            if (Health >= 0)
            {
                // Use Random rand to look what drops
                rand.Next(1, 10 +1);
                // Drops item X with X% chance
                Dropchance(Dropnumber);


                Level.CurrentRoom.Remove(this);
            }
        }

        protected virtual void Dropchance(int dropnumber) { }
    }
}
