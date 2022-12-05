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
    // [With(typeof(Movement))]
    public partial class CameraControllingSystem : AEntitySetSystem<GameTime>
    {
        private EntityCommandRecorder EntityCommandRecorder = new EntityCommandRecorder();
        
        [Update]
        private void Update(ref Camera _camera, ref Transform _transform, ref Controller _controller){
            if(_controller.IsHolding("CameraMoveLeft")){
                // _transform.Translate(-Vector3.UnitX * 1/10);
                _transform.Rotate(Vector3.UnitX * .1f);
            }
            if(_controller.IsHolding("CameraMoveRight")){
                // _transform.Translate(Vector3.UnitX * 1/10);
                _transform.Rotate(-Vector3.UnitX * .1f);
            }
            if(_controller.IsHolding("CameraMoveUp")){
                _transform.Translate(Vector3.UnitY * 1/10);
            }
            if(_controller.IsHolding("CameraMoveDown")){
                _transform.Translate(-Vector3.UnitY * 1/10);
            }
            if(_controller.IsHolding("CameraZoomIn")){
                _transform.Translate(-Vector3.UnitZ * 1/10);
            }
            if(_controller.IsHolding("CameraZoomOut")){
                _transform.Translate(Vector3.UnitZ * 1/10);
            }
        }
    }
}