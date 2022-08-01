using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Collision.ContactSystem;
using System;

namespace Common.ECS.Systems
{
    [WhenAdded(typeof(PhysicObject))]
    public partial class CollisionRegistrationSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref PhysicObject physicObject)
        {
            physicObject.Body.OnCollision += OnCollisionHandler;
            physicObject.Body.OnSeparation += OnSeparationHandler;
        }

        private void OnCollisionHandler(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            using(EntitySet aEntitiesSet = World.GetEntities().With((in PhysicObject po) => po.Body == fixtureA.Body).AsSet())
            {
                var aSpan = aEntitiesSet.GetEntities();

                foreach (var item in aSpan)
                {
                    var collision = new Collision(fixtureA, fixtureB);
                    item.Set(collision);

                    Console.WriteLine($"Collision has happened: {fixtureA.FixtureId} with {fixtureB.FixtureId}");
                }
            }
        }

        private void OnSeparationHandler(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            using(EntitySet aEntitiesSet = World.GetEntities().With((in Collision c) => c.FixtureA == fixtureA && c.FixtureB == fixtureB).AsSet())
            {
                var aSpan = aEntitiesSet.GetEntities();

                foreach (var item in aSpan)
                {
                    item.Remove<Collision>();

                    Console.WriteLine($"Collision has broke: {fixtureA.FixtureId} with {fixtureB.FixtureId}");
                }
            }
        }
    }
}