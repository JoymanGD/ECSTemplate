using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    [With(typeof(PhysicObject))]
    [With(typeof(OnEarth))]
    public partial class JumpSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref PhysicObject physicObject, [Added] [Changed] ref Jump jump, in Entity entity)
        {
            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Clear();
            
            physicObject.Body.ApplyLinearImpulse(Vector2.UnitY * jump.Power);

            EntityCommandRecorder.Record(entity).Remove<Jump>();
            EntityCommandRecorder.Record(entity).Remove<OnEarth>();

            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Execute();
        }
    }
}