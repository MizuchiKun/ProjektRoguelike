using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{

    /// <summary>
    /// A Door to another <see cref="Room"/>.
    /// </summary>
    public class Trapdoor : Sprite
    {
        /// <summary>
        /// The closing animations of all kinds of rooms.
        /// </summary>
        private Animation[] _closeAnimations = 
        {
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Environment/DoorSheet"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(125),
                          repetitions: 0),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Environment/BossDoorSheet"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(125),
                          repetitions: 0),
            new Animation(animationSheet: Globals.Content.Load<Texture2D>("Sprites/Environment/HiddenDoorSheet"),
                          frameDimensions: new Vector2(256),
                          frameDuration: TimeSpan.FromMilliseconds(125),
                          repetitions: 0)
        };
        /// <summary>
        /// The texture of a locked door.
        /// </summary>
        private static Texture2D _lockedDoor = Globals.Content.Load<Texture2D>("Sprites/Environment/LockedDoor");

        /// <summary>
        /// The inner width of a door.
        /// </summary>
        public static byte Width { get; } = 50;

        /// <summary>
        /// The direction in which the <see cref="Door"/> leads.
        /// </summary>
        public Directions Direction { get => _direction; }
        /// <summary>
        /// The direction in which the Door leads.
        /// </summary>
        private Directions _direction;

        /// <summary>
        /// The kind of this <see cref="Door"/>.
        /// </summary>
        public DoorKind Kind { get; }

        /// <summary>
        /// The state of this <see cref="Door"/>.
        /// </summary>
        public DoorState State { get; set; }

        /// <summary>
        /// A "poof" animation.
        /// </summary>
        private static readonly Animation _poofAnimation = new Animation(Globals.Content.Load<Texture2D>("Sprites/Effects/Poofsheet"),
                                                                         new Vector2(256),
                                                                         TimeSpan.FromMilliseconds(80),
                                                                         repetitions: 0);
        private Sprite _poofAnimationSprite = null;

        /// <summary>
        /// Creates a new <see cref="Trapdoor"/> with the given position, rotation and <see cref="Room"/>.
        /// </summary>
        /// <param name="position">The position of the <see cref="Door"/>.</param>

        public Trapdoor(Vector2 position)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Environment/Trapdoor"),
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: null,
               rotation: 0f,
               scale: Tile.Size / new Vector2(256),
               layerDepth: 0.95f)
        {
            _poofAnimationSprite = new Sprite(animation: _poofAnimation,
                                                 position: Position,
                                                 origin: new Vector2(0.5f),
                                                 scale: Tile.Size / new Vector2(256));
            Level.CurrentRoom.Add(_poofAnimationSprite);
        }

        public override void Update()
        {
            // Did the Player go through this Door?
            bool wentThroughDoor = false;
            if (Level.Player.BumpsInto(this))
            {
                wentThroughDoor = true;
            }

                // Initiate the level change if the player went through this trapdoor.
                if (wentThroughDoor)
                {
                    Level.LevelIndex += 1;
                    Globals.CurrentScene = new Level(Level.LevelIndex);
                }

                // Remove _poofAnimationSprite if its animation is over.
                if (_poofAnimationSprite != null
                    && _poofAnimationSprite.CurrentAnimation.HasEnded)
                {
                    Level.CurrentRoom.Remove(_poofAnimationSprite);
                    _poofAnimationSprite = null;
                }
            }
        }
    }
