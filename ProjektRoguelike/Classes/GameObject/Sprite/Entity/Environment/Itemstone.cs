using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    
    /// <summary>
    /// An enemy
    /// </summary>
    public class Itemstone : Environment
    {
        Item Item;

        public Itemstone(Item item,
                         Vector2? position = null,
                         Rectangle? sourceRectangle = null,
                         float rotation = 0f,
                         SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprite/Environment/Itemstone"),
               position,
               sourceRectangle,
               rotation,
               effect)
        {
            this.Item = item;
        }

        public override void Update()
        {
            base.Update();

            if (Collides(Level.Player))
            {
                Item.Effect();
                Level.CurrentRoom.Remove(this);
            }
        }
    }
}
