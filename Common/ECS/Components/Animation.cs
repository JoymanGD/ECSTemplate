namespace Common.ECS.Components
{
    public struct Animation
    {
        public string Name { get; private set; }

        public Animation(string name)
        {
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}