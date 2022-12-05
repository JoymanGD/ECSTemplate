using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using Common.Helpers.System;
using MonoGame.Extended.Input;
using Common.Settings;

namespace Common.ECS.Systems
{
    [With(typeof(Controller))]
    [With(typeof(Bindings))]
    public partial class InputSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Controller _controller, ref Bindings _bindings){
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();
            ControlButtonsPressing(mouseState, keyboardState, ref _controller, ref _bindings);
        }

        void ControlButtonsPressing(MouseStateExtended _mouseState, KeyboardStateExtended _keyboardState, ref Controller _controller, ref Bindings _bindings){
            ExtendedStates states = new ExtendedStates(_mouseState, _keyboardState);
            
            foreach (var item in _controller.Holdings.ToList())
            {
                var value = WaldemInput.IsButtonDown(states, (WaldemButtons)bindings.Pairs[item.Key]);
                controller.SetHolding(item.Key, value);
            }

            foreach (var item in controller.Pressings.ToList())
            {
                var value = WaldemInput.WasButtonJustDown(states, (WaldemButtons)bindings.Pairs[item.Key]);
                controller.SetPressing(item.Key, value);
            }

            foreach (var item in controller.Unpressings.ToList())
            {
                var value = WaldemInput.WasButtonJustUp(states, (WaldemButtons)bindings.Pairs[item.Key]);
                controller.SetUnpressing(item.Key, value);
            }
        }
    }
}