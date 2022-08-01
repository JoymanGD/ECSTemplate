using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using Genbox.VelcroPhysics;
using Genbox.VelcroPhysics.Utilities;

namespace Common.ECS.Systems
{
    [With(typeof(PhysicObject))]
    [With(typeof(Transform))]
    public partial class PhysicsSyncSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Transform transform, ref PhysicObject physicObject, GameTime gameTime)
        {
            var bodyPos = ConvertUnits.ToDisplayUnits(physicObject.Body.Position);
            transform.Position = new Vector3(bodyPos.X, bodyPos.Y, transform.Position.Z);
            transform.OneRotation = physicObject.Body.Rotation;
        }
    }
}