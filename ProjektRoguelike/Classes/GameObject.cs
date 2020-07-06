using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Represents a GameObject.
    /// </summary>
    public abstract class GameObject
    {
        public abstract void Update();
        public abstract void Draw();
    }
}