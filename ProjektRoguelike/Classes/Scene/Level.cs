using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// A level.
    /// </summary>
    public class Level : Scene
    {
        /// <summary>
        /// The generated <see cref="Room"/>s of this Level.
        /// </summary>
        private static Room[,] _rooms = new Room[6, 6];

        /// <summary>
        /// The current room.
        /// </summary>
        public static Room CurrentRoom { get; set; }

        /// <summary>
        /// The next room. <br></br>
        /// Is used for transitions if not null.
        /// </summary>
        private static Room _nextRoom;

        /// <summary>
        /// The direction in which the transition goes. <br></br>
        /// Is used for transitions if not null.
        /// </summary>
        private static Directions? _transitionDirection = null;

        /// <summary>
        /// The <see cref="Player"/> of all levels.
        /// </summary>
        public static Player Player { get => _player; }
        private static Player _player;

        /// <summary>
        /// Creates a new Level by the given level index.
        /// </summary>
        /// <param name="levelIndex">The index of the level you want to create.</param>
        public Level(byte levelIndex)
        {
            //TESTTESTTESTROOM
            Camera.Position += new Vector2(0f, 1f) * (Room.Dimensions * Tile.Size);
            //_gameObjects.Add(new Room(0, Vector2.Zero));

            //generate the level / rooms based on levelIndex
            //......
                    //TESTTESTLEVEL
                    for (byte i = 0; i < 9; i++)
                    {
                        Vector2 position = new Vector2((i % 3), (i / 3));
                        _rooms[(int)position.X, (int)position.Y] = new Room(i, position * Globals.WindowDimensions, RoomKind.Normal);
                    }

            // Choose the starting room.
            Vector2 startRoomPos = Vector2.One;//Vector2.Zero;
            CurrentRoom = _rooms[(int)startRoomPos.X, (int)startRoomPos.Y];
            // Set the camera position accordingly.
            Camera.Position = startRoomPos * Globals.WindowDimensions;
            // Initialize the player.
            _player = new Player(CurrentRoom.Position + (Room.Dimensions / 2 + new Vector2(0.5f, 0)) * Tile.Size * Globals.Scale);

            // Add the doors.
            for (byte x = 0; x < _rooms.GetLength(0); x++)
            {
                for (byte y = 0; y < _rooms.GetLength(1); y++)
                {
                    // If there's a room at these indices.
                    if (_rooms[x, y] != null)
                    {
                        // Add doors in all directions in which there is an adjacent room.
                        Door[] doors = new Door[4];
                        DoorKind doorKind;
                        // Up.
                        if (y - 1 >= 0
                            && _rooms[x, y - 1] != null)
                        {
                            // Choose the door kind.
                            if (_rooms[x, y].Kind == RoomKind.Boss)
                            {
                                doorKind = DoorKind.Boss;
                            }
                            else if (_rooms[x, y].Kind == RoomKind.Hidden)
                            {
                                doorKind = DoorKind.Hidden;
                            }
                            else
                            {
                                if (_rooms[x, y - 1].Kind == RoomKind.Boss)
                                {
                                    doorKind = DoorKind.Boss;
                                }
                                else if (_rooms[x, y - 1].Kind == RoomKind.Hidden)
                                {
                                    doorKind = DoorKind.Hidden;
                                }
                                else
                                {
                                    doorKind = DoorKind.Normal;
                                }
                            }

                            // Add the Door.
                            doors[(byte)Directions.Up] = new Door(position: (new Vector2(x, y) * Globals.WindowDimensions
                                                                             + new Vector2(1, 0.5f) * Tile.Size * Globals.Scale)
                                                                            + new Vector2((Room.Dimensions.X - 1) / 2,
                                                                                          0)
                                                                              * Tile.Size * Globals.Scale,
                                                                  direction: Directions.Up,
                                                                  kindOfDoor: doorKind);
                        }
                        // Right.
                        if (x + 1 < _rooms.GetLength(0)
                            && _rooms[x + 1, y] != null)
                        {
                            // Choose the door kind.
                            if (_rooms[x, y].Kind == RoomKind.Boss)
                            {
                                doorKind = DoorKind.Boss;
                            }
                            else if (_rooms[x, y].Kind == RoomKind.Hidden)
                            {
                                doorKind = DoorKind.Hidden;
                            }
                            else
                            {
                                if (_rooms[x + 1, y].Kind == RoomKind.Boss)
                                {
                                    doorKind = DoorKind.Boss;
                                }
                                else if (_rooms[x + 1, y].Kind == RoomKind.Hidden)
                                {
                                    doorKind = DoorKind.Hidden;
                                }
                                else
                                {
                                    doorKind = DoorKind.Normal;
                                }
                            }

                            // Add the Door.
                            doors[(byte)Directions.Right] = new Door(position: (new Vector2(x, y) * Globals.WindowDimensions
                                                                                + new Vector2(1, 0.5f) * Tile.Size * Globals.Scale)
                                                                               + new Vector2(Room.Dimensions.X - 1,
                                                                                             (Room.Dimensions.Y - 1) / 2)
                                                                                 * Tile.Size * Globals.Scale,
                                                                     direction: Directions.Right,
                                                                     kindOfDoor: doorKind);
                        }
                        // Down.
                        if (y + 1 < _rooms.GetLength(1)
                            && _rooms[x, y + 1] != null)
                        {
                            // Choose the door kind.
                            if (_rooms[x, y].Kind == RoomKind.Boss)
                            {
                                doorKind = DoorKind.Boss;  
                            }
                            else if (_rooms[x, y].Kind == RoomKind.Hidden)
                            {
                                doorKind = DoorKind.Hidden;
                            }
                            else
                            {
                                if (_rooms[x, y + 1].Kind == RoomKind.Boss)
                                {
                                    doorKind = DoorKind.Boss;
                                }
                                else if (_rooms[x, y + 1].Kind == RoomKind.Hidden)
                                {
                                    doorKind = DoorKind.Hidden;
                                }
                                else
                                {
                                    doorKind = DoorKind.Normal;
                                }
                            }

                            // Add the Door.
                            doors[(byte)Directions.Down] = new Door(position: (new Vector2(x, y) * Globals.WindowDimensions
                                                                               + new Vector2(1, 0.5f) * Tile.Size * Globals.Scale)
                                                                              + new Vector2((Room.Dimensions.X - 1) / 2,
                                                                                            Room.Dimensions.Y - 1)
                                                                                * Tile.Size * Globals.Scale,
                                                                    direction: Directions.Down,
                                                                    kindOfDoor: doorKind);
                        }
                        // Left.
                        if (x - 1 >= 0
                            && _rooms[x - 1, y] != null)
                        {
                            // Choose the door kind.
                            if (_rooms[x, y].Kind == RoomKind.Boss)
                            {
                                doorKind = DoorKind.Boss;
                            }
                            else if (_rooms[x, y].Kind == RoomKind.Hidden)
                            {
                                doorKind = DoorKind.Hidden;
                            }
                            else
                            {
                                if (_rooms[x - 1, y].Kind == RoomKind.Boss)
                                {
                                    doorKind = DoorKind.Boss;
                                }
                                else if (_rooms[x - 1, y].Kind == RoomKind.Hidden)
                                {
                                    doorKind = DoorKind.Hidden;
                                }
                                else
                                {
                                    doorKind = DoorKind.Normal;
                                }
                            }

                            // Add the Door.
                            doors[(byte)Directions.Left] = new Door(position: (new Vector2(x, y) * Globals.WindowDimensions
                                                                               + new Vector2(1, 0.5f) * Tile.Size * Globals.Scale)
                                                                              + new Vector2(0,
                                                                                            (Room.Dimensions.Y - 1) / 2)
                                                                                * Tile.Size * Globals.Scale,
                                                                    direction: Directions.Left,
                                                                    kindOfDoor: doorKind);
                        }

                        // Add the doors to the room.
                        _rooms[x, y].Doors = doors;
                    }
                }
            }

            // Add enemies to test.
                                //CurrentRoom.Add(new Floater(CurrentRoom.Position + (Room.Dimensions / 5) * Tile.Size * Globals.Scale));
                                //CurrentRoom.Add(new Screamer(CurrentRoom.Position + (Room.Dimensions / 7) * Tile.Size * Globals.Scale));
        }

        /// <summary>
        /// Updates the currently active <see cref="Room"/> and the <see cref="Player"/>.
        /// </summary>
        public override void Update()
        {
            // If not transition is happening.
            if (_transitionDirection == null)
            {
                // Update the current room if no transition is happening.
                CurrentRoom.Update();

                // Update the Player.
                Player.Update();
            }
            // Else a transition is happening.
            else
            {
                // Get the direction.
                Vector2 direction = Vector2.Zero;
                switch (_transitionDirection)
                {
                    case Directions.Up:
                        direction = -Vector2.UnitY;
                        break;
                    case Directions.Right:
                        direction = Vector2.UnitX;
                        break;
                    case Directions.Down:
                        direction = Vector2.UnitY;
                        break;
                    case Directions.Left:
                        direction = -Vector2.UnitX;
                        break;
                }

                // Place the player in the next room near the corresponding door.
                Player.Position = _nextRoom.Doors[((byte)_transitionDirection + 2) % 4].Position + (direction * 0.5f * Tile.Size);

                // Move the camera.
                ushort durationInMs = 250;
                float speed = ((byte)_transitionDirection % 2 == 0 ? Globals.WindowDimensions.Y : Globals.WindowDimensions.X) / (durationInMs / 1e3f);
                Camera.Position += direction * speed * (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

                // Move the camera back if it moved too far.
                bool transitionEnded = false;
                switch (_transitionDirection)
                {
                    case Directions.Up:
                        if (Camera.Position.Y <= CurrentRoom.Position.Y - Globals.WindowDimensions.Y)
                        {
                            transitionEnded = true;
                            Camera.Position = CurrentRoom.Position + direction * Globals.WindowDimensions;
                        }
                        break;
                    case Directions.Right:
                        if (Camera.Position.X >= CurrentRoom.Position.X + Globals.WindowDimensions.X)
                        {
                            transitionEnded = true;
                            Camera.Position = CurrentRoom.Position + direction * Globals.WindowDimensions;
                        }
                        break;
                    case Directions.Down:
                        if (Camera.Position.Y >= CurrentRoom.Position.Y + Globals.WindowDimensions.Y)
                        {
                            transitionEnded = true;
                            Camera.Position = CurrentRoom.Position + direction * Globals.WindowDimensions;
                        }
                        break;
                    case Directions.Left:
                        if (Camera.Position.X <= CurrentRoom.Position.X - Globals.WindowDimensions.X)
                        {
                            transitionEnded = true;
                            Camera.Position = CurrentRoom.Position + direction * Globals.WindowDimensions;
                        }
                        break;
                }

                // If the transition ended.
                if (transitionEnded)
                {
                    // The next room becomes the current room.
                    CurrentRoom = _nextRoom;

                    // Set the next room and the transition direction to null.
                    _nextRoom = null;
                    _transitionDirection = null;
                }
            }
        }

        public override void Draw()
        {
            // Draw the current room.
            CurrentRoom.Draw();

            // Draw the next room if it's not null.
            if (_nextRoom != null)
            {
                _nextRoom.Draw();
            }

            // Draw the UI.
            Globals.ui.Draw();

            // Draw the player.
            Player.Draw();
        }

        /// <summary>
        /// Initiates the room switching transition.
        /// </summary>
        /// <param name="direction">The direction of the next room.</param>
        public static void SwitchRoom(Directions direction)
        {
            // Store the transition direction.
            _transitionDirection = direction;

            // Set the next room.
            Vector2 currentRoomPos = CurrentRoom.Position / Globals.WindowDimensions;
            switch (direction)
            {
                case Directions.Up:
                    _nextRoom = _rooms[(int)(currentRoomPos.X), (int)(currentRoomPos.Y - 1)];
                    break;
                case Directions.Right:
                    _nextRoom = _rooms[(int)(currentRoomPos.X + 1), (int)(currentRoomPos.Y)];
                    break;
                case Directions.Down:
                    _nextRoom = _rooms[(int)(currentRoomPos.X), (int)(currentRoomPos.Y + 1)];
                    break;
                case Directions.Left:
                    _nextRoom = _rooms[(int)(currentRoomPos.X - 1), (int)(currentRoomPos.Y)];
                    break;
            }
        }

        /// <summary>
        /// Unlocks the counterpart of the given door.
        /// </summary>
        /// <param name="originDoor">The given door.</param>
        public static void UnlockCounterpartDoor(Door originDoor)
        {
            // Get the room indices.
            Vector2 currentRoomPos = new Vector2((float)Math.Floor(originDoor.Position.X / Globals.WindowDimensions.X),
                                                 (float)Math.Floor(originDoor.Position.Y / Globals.WindowDimensions.Y));

            // Get the direction vector.
            Vector2 directionVector = Vector2.Zero;
            switch (originDoor.Direction)
            {
                case Directions.Up:
                    directionVector = -Vector2.UnitY;
                    break;
                case Directions.Right:
                    directionVector = Vector2.UnitX;
                    break;
                case Directions.Down:
                    directionVector = Vector2.UnitY;
                    break;
                case Directions.Left:
                    directionVector = -Vector2.UnitX;
                    break;
            }

            // Unlock the counterpart.
            _rooms[(int)(currentRoomPos.X + directionVector.X), (int)(currentRoomPos.Y + directionVector.Y)].Doors[((byte)originDoor.Direction + 2) % 4].Unlock();
        }
    }
}
