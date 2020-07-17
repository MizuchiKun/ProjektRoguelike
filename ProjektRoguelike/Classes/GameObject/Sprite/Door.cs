using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Contains the different kinds of <see cref="Door"/>s.
    /// </summary>
    public enum DoorKind : byte
    {
        Normal, Boss, Hidden
    }

    /// <summary>
    /// Contains the possible states of a <see cref="Door"/>.
    /// </summary>
    public enum DoorState : byte
    {
        Open, Closed, Locked
    }

    /// <summary>
    /// A Door to another <see cref="Room"/>.
    /// </summary>
    public class Door : Sprite
    {
        /// <summary>
        /// The closing animations of all kinds of rooms.
        /// </summary>
        private static Animation[] _closeAnimations = 
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
        public DoorState State { get => _state; }
        private DoorState _state;

        /// <summary>
        /// Creates a new <see cref="Door"/> with the given position, rotation and <see cref="Room"/>.
        /// </summary>
        /// <param name="position">The position of the <see cref="Door"/>.</param>
        /// <param name="direction">
        /// The direction in which the <see cref="Door"/> leads.
        /// </param>
        /// <param name="roomBehindDoor">The <see cref="Room"/> that lies behind this <see cref="Door"/>.</param>
        public Door(Vector2 position,
                    Directions direction,
                    DoorKind kindOfDoor = DoorKind.Normal,
                    DoorState doorState = DoorState.Open)
        : base(texture: Globals.Content.Load<Texture2D>("Sprites/Environment/Doorsheet"),
               position: position,
               origin: new Vector2(0.5f),
               sourceRectangle: new Rectangle(new Point(0), new Point(256)),
               rotation: (byte)direction * 90f,
               scale: Tile.Size / new Vector2(256),
               layerDepth: 0.95f)
        {
            // Store the parameters. 
            _direction = direction;
            Kind = kindOfDoor;
            _state = doorState;

            // Set the initial animation.
            CurrentAnimation = _closeAnimations[(byte)Kind];
            CurrentAnimation.IsReversed = true;
        }

        public override void Update()
        {
            // Unlock the door.
            if (State == DoorState.Locked)
            {
                // if hidden and hit by bomb.
                // OR if colliding with player and player has more than 0 keys.
                    // change state to closed.
                    // change state of counterpart (in next room) to closed. (maybe by public method of Level?)
            }

            // If the door is open and there are enemies in the room.
            if (_state == DoorState.Open
                && Level.CurrentRoom.Enemies.Count > 0)
            {
                // Close the Door.
                _state = DoorState.Closed;
                // Start closing animation.
                CurrentAnimation.IsReversed = false;
                CurrentAnimation.Restart();
                CurrentAnimation.SelectFrame(1);
            }
            // Else if the door is closed and there are no enemies in the room.
            else if (_state == DoorState.Closed
                     && Level.CurrentRoom.Enemies.Count == 0)
            {
                // Open the door.
                _state = DoorState.Open;
                // Start opening animation.
                CurrentAnimation.IsReversed = true;
                CurrentAnimation.Restart();
                CurrentAnimation.SelectFrame(1);
            }

            // If this Door is open.
            if (State == DoorState.Open)
            {
                // Did the Player go through this Door?
                bool wentThroughDoor = false;
                switch (_direction)
                {
                    case Directions.Up:
                        if (Level.Player.Position.Y < Position.Y)
                        {
                            wentThroughDoor = true;
                        }
                        break;
                    case Directions.Right:
                        if (Level.Player.Position.X > Position.X)
                        {
                            wentThroughDoor = true;
                        }
                        break;
                    case Directions.Down:
                        if (Level.Player.Position.Y > Position.Y)
                        {
                            wentThroughDoor = true;
                        }
                        break;
                    case Directions.Left:
                        if (Level.Player.Position.X < Position.X)
                        {
                            wentThroughDoor = true;
                        }
                        break;
                }

                // Initiate the room change if the player went through this door.
                if (wentThroughDoor)
                {
                    Level.SwitchRoom(_direction);
                }
            }
        }
    }
}
