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
        bool pickedUp = false;

        public Itemstone(Item item,
                         Vector2? position = null,
                         Rectangle? sourceRectangle = null,
                         float rotation = 0f,
                         SpriteEffects effect = SpriteEffects.None)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Environment/Itemstone"),
               position,
               sourceRectangle,
               rotation,
               effect)
        {
            this.Item = item;
            Health = 10000;

            Scale = new Vector2(.3f);
        }

        public override void Update()
        {
            base.Update();
            
            if (Touches(Level.Player) && (Globals.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.W) ||
                                           Globals.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.A) ||
                                           Globals.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.S) ||
                                           Globals.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.D)))
            {
                Item.Effect();
                pickedUp = true;
                //Level.CurrentRoom.Remove(Item);
            }

            if (pickedUp == true)
            {
                
            }
        }

        public override void Draw()
        {
            if (pickedUp == false)
            {
                Item.Draw();
            }
            base.Draw();
        }
    }
}
