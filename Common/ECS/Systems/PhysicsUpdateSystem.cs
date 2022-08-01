using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using Genbox.VelcroPhysics;
using Genbox.VelcroPhysics.Utilities;

namespace Common.ECS.Systems
{
    [With(typeof(PhysicWorld))]
    public partial class PhysicsUpdateSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref PhysicWorld physicWorld, GameTime gameTime)
        {
            physicWorld.Instance.Step(MathHelper.Min((float)gameTime.ElapsedGameTime.TotalSeconds, 1f / 30f));
        }
    }
}