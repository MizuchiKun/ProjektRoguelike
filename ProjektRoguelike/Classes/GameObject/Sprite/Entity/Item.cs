using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// An item
    /// </summary>
    public abstract class Item : Entity
    {
        public Item(Texture2D texture,
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
            Effect();
            Level.CurrentRoom.Remove(this);
        }

        /// <summary>
        /// The effect that aquiring the item has on the player. 
        /// Removing the item mustnt be done in this method.
        /// </summary>
        public abstract void Effect();
    }
}
