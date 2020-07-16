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
        /// Whether this animation is playing in reversed order.
        /// </summary>
        public bool IsReversed { get; set; } = false;

        /// <summary>
        /// The current frame of this <see cref="Animation"/>.
        /// </summary>
        private sbyte _currentFrame;
        /// <summary>
        /// The count of frame of this <see cref="Animation"/>.
        /// </summary>
        private sbyte _frameCount;
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
                    // If it's not reversed.
                    if (!IsReversed)
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
                                if (_repetitions > 0)
                                {
                                    _currentRepetition++;
                                }
                            }
                            // Else it shall not repeat.
                            else
                            {
                                _currentFrame = (sbyte)(_frameCount - 1);
                                _hasEnded = true;
                            }
                        }
                    }
                    // Else it is reversed.
                    else
                    {
                        // It's the next frame.
                        _currentFrame--;

                        // If the current repetition ended.
                        if (_currentFrame == -1)
                        {
                            // And it shall repeat.
                            if (_repetitions < 0
                                || (_repetitions > 0
                                    && _currentRepetition < _repetitions))
                            {
                                // Start from the beginning.
                                _currentFrame = (sbyte)(_frameCount - 1);
                                // A new repetition starts.
                                if (_repetitions > 0)
                                {
                                    _currentRepetition++;
                                }
                            }
                            // Else it shall not repeat.
                            else
                            {
                                _currentFrame = 0;
                                _hasEnded = true;
                            }
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
        /// The sprite effects of this <see cref="Animation"/>.
        /// </summary>
        public SpriteEffects Effects { get; }

        /// <summary>
        /// Creates a new <see cref="Animation"/> with the given parameters.
        /// </summary>
        /// <param name="animationSheet">The sheet that contains the frames of the animation.</param>
        /// <param name="frameDimensions">The dimensions of a frame.</param>
        /// <param name="frameDuration">The duration of a frame in milliseconds.</param>
        /// <param name="effects">The sprite effects of this animation.</param>
        /// <param name="repetitions">How often it shall repeat. Set to -1 if it shall repeat continuously.</param>
        /// <param name="orderIsReversed">Whether this animation shall be player in reverse order.</param>
        /// <param name="startingFrameIndex">An optional index of the starting frame. It's 0 by default.</param>
        public Animation(Texture2D animationSheet,
                         Vector2 frameDimensions,
                         TimeSpan frameDuration,
                         SpriteEffects effects = SpriteEffects.None,
                         sbyte repetitions = -1,
                         bool orderIsReversed = false)
        {
            // Store the parameters.
            Sheet = animationSheet;
            _frameDimensions = frameDimensions;
            _frameDuration = frameDuration;
            Effects = effects;
            _repetitions = repetitions;
            IsReversed = orderIsReversed;

            // Get the frame count.
            _frameCount = (sbyte)(Sheet.Width / _frameDimensions.X);

            // Set the starting frame.
            _currentFrame = (!IsReversed) ? (sbyte)0 : (sbyte)(_frameCount - 1);

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
        /// Restarts the animation from repetition 0.
        /// </summary>
        public void Restart()
        {
            // Select the first frame.
            _currentFrame = (!IsReversed) ? (sbyte)0 : (sbyte)(_frameCount - 1);

            // It's the first repetition, again.
            _currentRepetition = 0;

            // It neither ended, nor is it paused.
            _hasEnded = false;
            _isPaused = false;

            // The current frame starts.
            _frameStart = DateTime.Now;
        }

        /// <summary>
        /// Selects the frame at the given index. <br></br>
        /// The Animation will continue from that frame. <br></br>
        /// 0 corresponds to the first frame in the current order.
        /// </summary>
        /// <param name="frameIndex">The given frame index.</param>
        public void SelectFrame(sbyte frameIndex)
        {
            _currentFrame = (!IsReversed) ? frameIndex : (sbyte)((_frameCount - 1) - frameIndex);
        }
    }
}
