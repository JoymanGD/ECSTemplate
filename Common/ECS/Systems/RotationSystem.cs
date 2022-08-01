using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;

namespace Common.ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(Rotation))]
    [Without(typeof(PhysicObject))]
    public partial class RotationSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Transform transform, ref Rotation rotation)
        {
            transform.Rotate(rotation.Value);
        }
    }
}