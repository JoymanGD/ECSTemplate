using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Components
{
    public struct Player
    {
        public int Id { get; private set; }
        public float Speed { get; private set; }
        public float JumpPower { get; private set; }

        public Player(int _id = 0){
            Id = _id;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}