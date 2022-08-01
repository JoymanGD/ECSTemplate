using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    //TODO: Turn to ForceApplyingSystem
    [WhenAdded(typeof(Collision))]
    public partial class OnEarthRegistrationSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Collision collision, in Entity entity)
        {
            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Clear();

            if(collision.FixtureA.Body.Position.Y > collision.FixtureB.Body.Position.Y)
            {
                EntityCommandRecorder.Record(entity).Set<OnEarth>();
            }

            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Execute();
        }
    }
}