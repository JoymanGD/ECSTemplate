using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Components
{
    public struct Translation
    {
        public Vector3 Direction { get; private set; }
        public float Speed { get; private set; }

        public Translation(Vector3 direction, float speed)
        {
            Direction = direction;
            Speed = speed;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public void SetDirection(Vector3 direction)
        {
            Direction = direction;
        }
    }
}