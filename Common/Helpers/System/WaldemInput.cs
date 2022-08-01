using System;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace Common.Helpers.System
{
    public static class WaldemInput
    {
        public static bool IsButtonDown(ExtendedStates states, WaldemButtons button)
        {
            var keyboardState = states.KeyboardState;
            var mouseState = states.MouseState;

            var intButton = (int)button;
            if(intButton < 8)
            {
                return MouseExtended.GetState().IsButtonDown((MouseButton)intButton);
            }
            else if(intButton > 7)
            {
                return KeyboardExtended.GetState().IsKeyDown((Keys)intButton);
            }

            return false;
        }

        public static bool IsButtonUp(ExtendedStates states, WaldemButtons button)
        {
            var keyboardState = states.KeyboardState;
            var mouseState = states.MouseState;

            var intButton = (int)button;
            if(intButton < 8)
            {
                return MouseExtended.GetState().IsButtonUp((MouseButton)intButton);
            }
            else if(intButton > 7)
            {
                return KeyboardExtended.GetState().IsKeyUp((Keys)intButton);
            }

            return false;
        }

        public static bool WasButtonJustDown(ExtendedStates states, WaldemButtons button)
        {
            var keyboardState = states.KeyboardState;
            var mouseState = states.MouseState;

            var intButton = (int)button;
            var key = (Keys)button;

            if(intButton < 8)
            {
                return mouseState.WasButtonJustDown((MouseButton)intButton);
            }
            else if(intButton > 7)
            {
                return keyboardState.WasKeyJustUp((Keys)intButton);
            }

            return false;
        }
        
        public static bool WasButtonJustUp(ExtendedStates states, WaldemButtons button)
        {
            var keyboardState = states.KeyboardState;
            var mouseState = states.MouseState;

            var intButton = (int)button;
            
            if(intButton < 8)
            {
                return mouseState.WasButtonJustUp((MouseButton)intButton);
            }
            else if(intButton > 7)
            {
                return keyboardState.WasKeyJustDown((Keys)intButton);
            }

            return false;
        }
        
        public static bool WasAnyButtonJustDown(ExtendedStates states)
        {
            var keyboardState = states.KeyboardState;
            var mouseState = states.MouseState;

            var mouseButtonWasDown =  mouseState.WasButtonJustDown(MouseButton.Left) ||
                                      mouseState.WasButtonJustDown(MouseButton.Right) ||
                                      mouseState.WasButtonJustDown(MouseButton.Middle) ||
                                      mouseState.WasButtonJustDown(MouseButton.XButton1) ||
                                      mouseState.WasButtonJustDown(MouseButton.XButton2);

            var keyWasDown = keyboardState.WasAnyKeyJustDown();

            return mouseButtonWasDown || keyWasDown;
        }
    }

    public struct ExtendedStates
    {
        public MouseStateExtended MouseState;
        public KeyboardStateExtended KeyboardState;

        public ExtendedStates(MouseStateExtended mouseState, KeyboardStateExtended keyboardState)
        {
            MouseState = mouseState;
            KeyboardState = keyboardState;
        }
    }
}