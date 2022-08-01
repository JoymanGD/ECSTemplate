using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Common.ECS.Components;
using Genbox.VelcroPhysics.Utilities;

namespace Common.ECS.Systems
{
    [With(typeof(PhysicObject))]
    [With(typeof(Translation))]
    public partial class PhysicTranslationSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref PhysicObject physicObject, ref Translation movement)
        {
            var movementVector = movement.Direction * movement.Speed;
            var force = new Vector2(movementVector.X, movementVector.Y);

            physicObject.Body.LinearVelocity += ConvertUnits.ToSimUnits(force);
        }
    }
}