using DefaultEcs;
using DefaultEcs.System;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    [With(typeof(Controller))]
    [With(typeof(Player))]
    public partial class PlayerControllingSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Controller controller, ref Player player, in Entity entity)
        {
            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Clear();

            if(controller.WasPressed("Jump"))
            {
                EntityCommandRecorder.Record(entity).Set(new Jump(player.JumpPower));
            }
            
            Vector2 inputVector = controller.GetInputVector("MoveLeft", "MoveRight");
            Vector3 moveDirection = new Vector3(inputVector, 0);

            EntityCommandRecorder.Record(entity).Set(new Translation(moveDirection, player.Speed));
            
            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Execute();
        }
    }
}