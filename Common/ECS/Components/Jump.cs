using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Components
{
    public struct Jump
    {
        public float Power { get; private set; }

        public Jump(float power)
        {
            Power = power;
        }
    }
}