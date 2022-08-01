using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Common.ECS.Components;
using Genbox.VelcroPhysics.Utilities;

namespace Common.ECS.Systems
{
    [With(typeof(PhysicObject))]
    [With(typeof(Rotation))]
    public partial class PhysicRotationSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref PhysicObject physicObject, ref Rotation rotation)
        {
            physicObject.Body.AngularVelocity += ConvertUnits.ToSimUnits(rotation.Value);
        }
    }
}