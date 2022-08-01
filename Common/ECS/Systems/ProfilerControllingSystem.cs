using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    [With(typeof(Controller))]
    [With(typeof(ComponentMark<Profiler>))]
    public partial class ProfilerControllingSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Controller controller, in Entity entity)
        {
            if(controller.WasPressed("Profiler"))
            {
                if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Clear();

                if(entity.Has<Profiler>())
                {
                    EntityCommandRecorder.Record(entity).Remove<Profiler>();
                }
                else
                {
                    EntityCommandRecorder.Record(entity).Set(new Profiler());
                }

                if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Execute();
            }
        }
    }
}