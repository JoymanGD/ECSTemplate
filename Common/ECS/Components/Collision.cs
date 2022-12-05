using Genbox.VelcroPhysics.Dynamics;

namespace Common.ECS.Components
{
    public struct Collision
    {
        public Fixture FixtureA { get; private set; }
        public Fixture FixtureB { get; private set; }

        public Collision(Fixture a, Fixture b)
        {
            FixtureA = a;
            FixtureB = b;
        }
    }
}