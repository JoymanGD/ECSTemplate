using Common.Settings;
using Microsoft.Xna.Framework;

namespace Common.ECS.Components
{
    public struct Camera
    {
        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }
        public CameraType ProjectiveType { get; private set; }
        public float MinZ { get; private set; }
        public float MaxZ { get; private set; }
        public float ZoomRatio { get; private set; }

        public Camera(Transform cameraTransform, CameraType projectiveType, float minZ = 10, float maxZ = 100, float zoomRatio = .016f)
        {
            MinZ = minZ;
            MaxZ = maxZ;
            ZoomRatio = zoomRatio;

            var worldMatrix = cameraTransform.WorldMatrix;

            ViewMatrix = Matrix.CreateLookAt(worldMatrix.Translation, worldMatrix.Forward + worldMatrix.Translation, worldMatrix.Down);
            ProjectionMatrix = Matrix.Identity;

            ProjectiveType = projectiveType;
            
            UpdateProjectionMatrix(worldMatrix);
        }

        public void SyncWithTransform(Transform transform)
        {
            ViewMatrix = Matrix.Invert(transform.WorldMatrix);
        }

        public void UpdateProjectionMatrix(Matrix worldMatrix)
        {
            var zoom = 10/worldMatrix.Translation.Z;

            var graphics = GameSettings.Instance.Graphics;
            var graphicsDevice = graphics.GraphicsDevice;
            
            var viewPort = graphicsDevice.Viewport;

            switch (ProjectiveType)
            {
                case CameraType.Orthographic:
                    ProjectionMatrix = Matrix.CreateOrthographicOffCenter(-viewPort.Width/zoom, viewPort.Width/zoom, viewPort.Height/zoom, -viewPort.Height/zoom, MinZ, MaxZ);
                    break;
                case CameraType.Perspective:
                    ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight, MinZ, MaxZ);
                    break;
                default:
                    ProjectionMatrix = Matrix.Identity;
                    break;
            }
        }

        public void ApplyWorldMatrix(Matrix _worldMatrix){
            ViewMatrix = Matrix.Invert(_worldMatrix);
            // var target = _worldMatrix.Translation + _worldMatrix.Forward;
            // ViewMatrix = Matrix.CreateLookAt(_worldMatrix.Translation, target, _worldMatrix.Up);
        }
    }
}