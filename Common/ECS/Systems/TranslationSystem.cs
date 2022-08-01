using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Common.ECS.Components;

namespace Common.ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(Translation))]
    [Without(typeof(PhysicObject))]
    public partial class TranslationSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Transform transform, ref Translation movement)
        {
            transform.Translate(movement.Direction * movement.Speed);
        }
    }
}