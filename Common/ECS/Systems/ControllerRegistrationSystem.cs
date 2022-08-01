using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;

namespace Common.ECS.Systems
{
    [With(typeof(Bindings))]
    [WhenAdded(typeof(Controller))]
    public partial class ControllerRegistrationSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Controller controller, ref Bindings bindings)
        {
            controller.Init(bindings);
        }
    }
}