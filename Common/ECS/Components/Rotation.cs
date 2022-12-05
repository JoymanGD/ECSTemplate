namespace Common.ECS.Components
{
    public struct Rotation
    {
        public float Value { get; private set; }

        public Rotation(float value)
        {
            Value = value;
        }
    }
}