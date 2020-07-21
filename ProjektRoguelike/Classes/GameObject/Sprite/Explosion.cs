﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    class Explosion : Sprite
    {
        /// <summary>
        /// The blast radius of an explosion.
        /// </summary>
        private static readonly float BlastRadius = 1.1f;

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

            // Set the layer.
            Layer = 0.9f - (Position.Y / 10e6f);

            timer = new McTimer(300);
        }

        public override void Update()
        {
            // If it collides with a hidden, locked door.
            // Top.
            if ((Level.CurrentRoom.Doors[(byte)Directions.Up] != null 
                 && (Position - Level.CurrentRoom.Doors[(byte)Directions.Up].Position).Length() <= (BlastRadius * Tile.Size.X))
                && (Level.CurrentRoom.Doors[(byte)Directions.Up].Kind == DoorKind.Hidden
                    && Level.CurrentRoom.Doors[(byte)Directions.Up].State == DoorState.Locked))
            {
                // Unlock the top door.
                Level.CurrentRoom.Doors[(byte)Directions.Up].Unlock(true);
            }
            // Right.
            if ((Level.CurrentRoom.Doors[(byte)Directions.Right] != null
                 && (Position - Level.CurrentRoom.Doors[(byte)Directions.Right].Position).Length() <= (BlastRadius * Tile.Size.X))
                && (Level.CurrentRoom.Doors[(byte)Directions.Right].Kind == DoorKind.Hidden
                    && Level.CurrentRoom.Doors[(byte)Directions.Right].State == DoorState.Locked))
            {
                // Unlock the right door.
                Level.CurrentRoom.Doors[(byte)Directions.Right].Unlock(true);
            }
            // Bottom.
            if ((Level.CurrentRoom.Doors[(byte)Directions.Down] != null
                 && (Position - Level.CurrentRoom.Doors[(byte)Directions.Down].Position).Length() <= (BlastRadius * Tile.Size.X))
                && (Level.CurrentRoom.Doors[(byte)Directions.Down].Kind == DoorKind.Hidden
                    && Level.CurrentRoom.Doors[(byte)Directions.Down].State == DoorState.Locked))
            {
                // Unlock the bottom door.
                Level.CurrentRoom.Doors[(byte)Directions.Down].Unlock(true);
            }
            // Left.
            if ((Level.CurrentRoom.Doors[(byte)Directions.Left] != null
                 && (Position - Level.CurrentRoom.Doors[(byte)Directions.Left].Position).Length() <= (BlastRadius * Tile.Size.X))
                && (Level.CurrentRoom.Doors[(byte)Directions.Left].Kind == DoorKind.Hidden
                    && Level.CurrentRoom.Doors[(byte)Directions.Left].State == DoorState.Locked))
            {
                // Unlock the left door.
                Level.CurrentRoom.Doors[(byte)Directions.Left].Unlock(true);
            }

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
