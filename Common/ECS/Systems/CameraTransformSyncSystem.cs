using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;

namespace Common.ECS.Systems
{
    [With(typeof(Camera))]
    [With(typeof(Transform))]
    public partial class CameraTransformSyncSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Camera camera, ref Transform transform)
        {
            camera.SyncWithTransform(transform);
            camera.UpdateProjectionMatrix(transform.WorldMatrix);
        }
    }
}