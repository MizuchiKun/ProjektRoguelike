using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektRoguelike
{
    /// <summary>
    /// Contains camera information.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// The camera's position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The angles in which the camera is looking horizontally and vertically.
        /// </summary>
        public Vector2 LookAtAngles { get; set; }

        /// <summary>
        /// Gets the camera's projection matrix.
        /// </summary>
        public Matrix Projection {  
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView: MathHelper.ToRadians(90f),
                    aspectRatio: Globals.Graphics.GraphicsDevice.DisplayMode.AspectRatio,
                    nearPlaneDistance: 10f,
                    farPlaneDistance: 2560f
                );
            }
        }

        /// <summary>
        /// Gets the camera's view matrix.
        /// </summary>
        public Matrix View {
            get 
            {
                return Matrix.CreateLookAt(
                    cameraPosition: Position,
                    cameraTarget: Position + Globals.DegreesToVector3(LookAtAngles),
                    cameraUpVector: Vector3.UnitZ
                );
            }
        }

        /// <summary>
        /// Gets the camera's world matrix.
        /// </summary>
        public Matrix World { 
            get
            {
                return Matrix.CreateWorld(
                    position: Globals.DegreesToVector3(LookAtAngles),
                    forward: Vector3.Forward,
                    up: Vector3.Up
                );
            }
        }

        /// <summary>
        /// Creates a new <see cref="Camera"/> with the given position and look-at vector.
        /// </summary>
        /// <param name="position">The Camera's position.</param>
        /// <param name="lookAtVector">The direction in which the camera is looking.</param>
        public Camera(Vector3 position, Vector2 lookAtVector)
        {
            Position = position;
            LookAtAngles = lookAtVector;
        }
    }
}
