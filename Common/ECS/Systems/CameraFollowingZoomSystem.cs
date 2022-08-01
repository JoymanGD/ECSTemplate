using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    [With(typeof(Follower))]
    [With(typeof(Camera))]
    [With(typeof(Transform))]
    public partial class CameraFollowingZoomSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Camera camera, ref Transform transform, ref Follower follower, GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var pos = transform.Position;
            pos.Z = MathHelper.Clamp(follower.TargetsDistance * camera.ZoomRatio, camera.MinZ, camera.MaxZ);
            transform.Position = pos;
        }
    }
}