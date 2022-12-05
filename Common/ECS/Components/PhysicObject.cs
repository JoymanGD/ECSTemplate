using ECSGame.Common.Helpers.Utils;
using Genbox.VelcroPhysics.Collision.Filtering;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;
using Genbox.VelcroPhysics.Utilities;
using MonoGame.Extended.Sprites;

namespace Common.ECS.Components
{
    public struct PhysicObject
    {
        public Body Body { get; private set; }
        public BodyShape Shape { get; private set; }

        /// <summary>
        /// Creates body physic object
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="bodyType"></param>
        /// <param name="mass"></param>
        public PhysicObject(PhysicWorld physicWorld, Transform transform, BodyType bodyType = BodyType.Dynamic, float mass = 1f, Category category = Category.Cat1)
        {
            Vector2 vector2Position = new Vector2(transform.Position.X, transform.Position.Y);
            Body = BodyFactory.CreateBody(physicWorld.Instance,
                                          ConvertUnits.ToSimUnits(vector2Position),
                                          0,
                                          bodyType);
            Body.Mass = mass;
            Body.CollisionCategories = category;

            Shape = BodyShape.Body;
        }

        /// <summary>
        /// Creates circle physic object
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="radius"></param>
        /// <param name="density"></param>
        /// <param name="bodyType"></param>
        /// <param name="mass"></param>
        public PhysicObject(PhysicWorld physicWorld, Transform transform, float radius, float density, BodyType bodyType = BodyType.Dynamic, float mass = 1f, Category category = Category.Cat1)
        {
            Vector2 vector2Position = new Vector2(transform.Position.X, transform.Position.Y);
            Body = BodyFactory.CreateCircle(physicWorld.Instance,
                                            ConvertUnits.ToSimUnits(radius),
                                            density,
                                            ConvertUnits.ToSimUnits(vector2Position),
                                            bodyType);
            Body.Mass = mass;
            Body.CollisionCategories = category;

            Shape = BodyShape.Circle;
        }

        /// <summary>
        /// Creates rectangle physic object
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="density"></param>
        /// <param name="bodyType"></param>
        /// <param name="mass"></param>
        public PhysicObject(PhysicWorld physicWorld, Transform transform, float width, float height, float density, BodyType bodyType = BodyType.Dynamic, float mass = 1f, Category category = Category.Cat1)
        {
            Vector2 vector2Position = new Vector2(transform.Position.X, transform.Position.Y);
            Body = BodyFactory.CreateRectangle(physicWorld.Instance,
                                            ConvertUnits.ToSimUnits(width),
                                            ConvertUnits.ToSimUnits(height),
                                            density,
                                            ConvertUnits.ToSimUnits(vector2Position),
                                            0,
                                            bodyType);
            Body.Mass = mass;
            Body.CollisionCategories = category;

            Shape = BodyShape.Rectangle;
        }

        /// <summary>
        /// Creates polygon physic object
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="sprite"></param>
        /// <param name="density"></param>
        /// <param name="bodyType"></param>
        /// <param name="mass"></param>
        public PhysicObject(PhysicWorld physicWorld, Transform transform, Sprite sprite, float density, BodyType bodyType = BodyType.Dynamic, float mass = 1f, Category category = Category.Cat1)
        {
            var vertices = PhysicsUtils.SpriteToVertices(sprite);
            
            Vector2 vector2Position = new Vector2(transform.Position.X, transform.Position.Y);

            Body = BodyFactory.CreatePolygon(physicWorld.Instance,
                                            vertices,
                                            density,
                                            ConvertUnits.ToSimUnits(vector2Position),
                                            0,
                                            bodyType);
            Body.Mass = mass;
            Body.CollisionCategories = category;

            Shape = BodyShape.Polygon;
        }
    } 

    public enum BodyShape
    {
        Body = 0,
        Circle = 1,
        Rectangle = 2,
        Polygon = 3,
    }
}