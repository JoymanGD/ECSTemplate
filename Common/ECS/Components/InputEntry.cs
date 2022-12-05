using Common.Helpers.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Components
{
    public struct InputEntry
    {
        public ExtendedStates ExtendedStates { get; private set; }

        public InputEntry(ExtendedStates extendedStates)
        {
            ExtendedStates = extendedStates;
        }
    }
}