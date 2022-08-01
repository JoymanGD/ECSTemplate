using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Common.ECS.Components;
using DefaultEcs.Command;

namespace Common.ECS.Systems
{
    [With(typeof(Camera))]
    [With(typeof(Transform))]
    [With(typeof(Controller))]
    public partial class CameraControllingSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Controller controller, ref Transform transform, in Entity entity)
        {
            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Clear();

            Vector2 inputVector = controller.GetInputVector("CameraMoveLeft", "CameraMoveRight", "CameraMoveUp", "CameraMoveDown");

            Vector3 moveDirection = transform.WorldMatrix.Up * inputVector.Y + transform.WorldMatrix.Right * inputVector.X;

            EntityCommandRecorder.Record(entity).Set(new Translation(moveDirection, 10f));
            
            if(EntityCommandRecorder.Size > 0) EntityCommandRecorder.Execute();
        }
    }
}