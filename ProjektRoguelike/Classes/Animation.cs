using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Handles the animation of frames in an animationsheet.
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// The sheet containing the frames of the animation.
        /// </summary>
        public Texture2D Sheet { get; }
        /// <summary>
        /// The dimensions of a frame.
        /// </summary>
        private Vector2 _frameDimensions;
        /// <summary>
        /// The duration of a frame.
        /// </summary>
        private TimeSpan _frameDuration;

        /// <summary>
        /// The repetitions of this <see cref="Animation"/>.
        /// </summary>
        private sbyte _repetitions;
        /// <summary>
        /// The current repetition of the <see cref="Animation"/>.
        /// </summary>
        private sbyte _currentRepetition;

        /// <summary>
        /// Whether the <see cref="Animation"/> has ended.
        /// </summary>
        public bool HasEnded { get => _hasEnded; }
        /// <summary>
        /// Whether the animation has ended.
        /// </summary>
        private bool _hasEnded = false;
        /// <summary>
        /// Whether the animation is paused.
        /// </summary>
        private bool _isPaused = false;

        /// <summary>
        /// The current frame of this <see cref="Animation"/>.
        /// </summary>
        private byte _currentFrame = 0;
        /// <summary>
        /// The count of frame of this <see cref="Animation"/>.
        /// </summary>
        private byte _frameCount;
        /// <summary>
        /// The time when the current frame started.
        /// </summary>
        private DateTime _frameStart;

        /// <summary>
        /// The source rectangle of the current frame.
        /// </summary>
        public Rectangle CurrentFrame
        {
            get
            {
                // Is the current frame over?
                if (!_hasEnded 
                    && !_isPaused
                    && Globals.HasTimePassed(_frameDuration, _frameStart))
                {
                    // It's the next frame.
                    _currentFrame++;

                    // If the current repetition ended.
                    if (_currentFrame == _frameCount)
                    {
                        // And it shall repeat.
                        if (_repetitions < 0
                            || (_repetitions > 0
                                && _currentRepetition < _repetitions))
                        {
                            // Start from the beginning.
                            _currentFrame = 0;
                            // A new repetition starts.
                            _currentRepetition++;
                        }
                        // Else it shall not repeat.
                        else
                        {
                            _currentFrame = 0;
                            _hasEnded = true;
                        }
                    }

                    // Update _frameStart.
                    _frameStart = DateTime.Now;
                }

                // Return the source rectangle.
                return new Rectangle(location: new Point(_currentFrame * (int)_frameDimensions.X, 0),
                                     size: _frameDimensions.ToPoint());
            }
        }

        /// <summary>
        /// Creates a new <see cref="Animation"/> with the given parameters.
        /// </summary>
        /// <param name="animationSheet">The sheet that contains the frames of the animation.</param>
        /// <param name="frameDimensions">The dimensions of a frame.</param>
        /// <param name="frameDuration">The duration of a frame in milliseconds.</param>
        /// <param name="repetitions">How often it shall repeat. Set to -1 if it shall repeat continuously.</param>
        /// <param name="startingFrameIndex">An optional index of the starting frame. It's 0 by default.</param>
        public Animation(Texture2D animationSheet,
                         Vector2 frameDimensions,
                         TimeSpan frameDuration,
                         sbyte repetitions = -1,
                         byte startingFrameIndex = 0)
        {
            // Store the parameters.
            Sheet = animationSheet;
            _frameDimensions = frameDimensions;
            _frameDuration = frameDuration;
            _repetitions = repetitions;
            _currentFrame = startingFrameIndex;

            // Get the frame count.
            _frameCount = (byte)(Sheet.Width / _frameDimensions.X);

            // Let the first frame start.
            _frameStart = DateTime.Now;
        }

        /// <summary>
        /// Pauses the <see cref="Animation"/>.
        /// </summary>
        public void Pause()
        {
            _isPaused = true;
        }

        /// <summary>
        /// Resumes the <see cref="Animation"/>.
        /// </summary>
        public void Resume()
        {
            _isPaused = false;
        }

        /// <summary>
        /// Selects the frame at the given index. <br></br>
        /// The Animation will continue from that frame.
        /// </summary>
        /// <param name="frameIndex">The given frame index.</param>
        public void SelectFrame(byte frameIndex)
        {
            _currentFrame = frameIndex;
        }
    }
}
