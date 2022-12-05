using Genbox.VelcroPhysics.Dynamics;

namespace Common.ECS.Components
{
    public struct PhysicWorld
    {
        public World Instance { get; private set; }

        public PhysicWorld(Vector2 gravity)
        {
            Instance = new World(gravity);
        }
    }
}