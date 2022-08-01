using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Components
{
    public struct Player
    {
        public int Id { get; private set; }
        public float Speed { get; private set; }
        public float JumpPower { get; private set; }

        public Player(int id = 0, float speed = 1f, float jumpPower = 10f)
        {
            Id = id;
            Speed = speed;
            JumpPower = jumpPower;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}