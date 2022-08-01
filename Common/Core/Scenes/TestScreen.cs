using Common.Settings;
using Common.ECS.Components;
using Common.ECS.Systems;
using DefaultEcs.System;
using Common.ECS.SceneManagement;
using MonoGame.Extended.Sprites;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Utilities;
using MonoGame.Extended.Input;
using Common.Helpers.System;
using Genbox.VelcroPhysics.Collision.Filtering;

namespace Common.Core.Scenes
{
    public class TestScreen : ECSScreen
    {
        public TestScreen(ECSGame game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public override void SetStartEntities()
        {
            var gameSession = World.CreateEntity();
            var gameSessionBindings = new Bindings("GameSession");
        #if DEBUG
            gameSessionBindings += new Bindings("Developer");
            gameSession.Set(new DebugMode());
        #endif
            gameSession.Set(gameSessionBindings);
            gameSession.Set(new Controller());
            gameSession.Set(new FontBase("Default.otf", 20));
            gameSession.Set(new ComponentMark<Profiler>());
            var physicWorld = new PhysicWorld(new Vector2(0, -25));
            gameSession.Set(physicWorld);

            var graphics = GameSettings.Instance.Graphics;

            var background = World.CreateEntity();
            var backgroundTransform = new Transform(Vector3.Zero, 0, 100);
            background.Set(backgroundTransform);
            var bgSprite = new Sprite(Content.Load<Texture2D>("Sprites/Background"));
            background.Set(new SpriteRenderer(bgSprite));

            var player = World.CreateEntity();
            var playerTransform = new Transform(new Vector3(-200,200,0));
            var playerSprite = new Sprite(Content.Load<Texture2D>("Sprites/Ball"));
            player.Set(playerTransform);
            player.Set(new PhysicObject(physicWorld, playerTransform, 50, 1, BodyType.Dynamic, 1));
            player.Set(new Bindings("Player"));
            player.Set(new Controller());
            player.Set(new Player(0, 25, 18));
            player.Set(new SpriteRenderer(playerSprite));

            var block = World.CreateEntity();
            var blockTransform = new Transform(new Vector3(-200,-800,0));
            var blockSprite = new Sprite(Content.Load<Texture2D>("Sprites/Block"));
            block.Set(blockTransform);
            block.Set(new PhysicObject(physicWorld, blockTransform, 100, 100, 3, BodyType.Static, 1, Category.Cat10));
            block.Set(new SpriteRenderer(blockSprite));

            var block2 = World.CreateEntity();
            var blockTransform2 = new Transform(new Vector3(600,-600,0));
            block2.Set(blockTransform2);
            block2.Set(new PhysicObject(physicWorld, blockTransform2, 100, 100, 3, BodyType.Static, 1, Category.Cat10));
            block2.Set(new SpriteRenderer(blockSprite));

            var block3 = World.CreateEntity();
            var blockTransform3 = new Transform(new Vector3(600,-200,0));
            block3.Set(blockTransform3);
            block3.Set(new PhysicObject(physicWorld, blockTransform3, 100, 100, 3, BodyType.Dynamic, 1));
            block3.Set(new SpriteRenderer(blockSprite));

            var camera = World.CreateEntity();
            var cameraTransform = new Transform(new Vector3(800, 0, 6), Vector3.Forward);
            camera.Set(cameraTransform);
            camera.Set(new Bindings("Camera"));
            camera.Set(new Controller());
            camera.Set(new MainCamera());
            // camera.Set(new Follower(1000f, true, true, false, playerTransform, blockTransform, blockTransform2, blockTransform3));
            camera.Set(new Follower(2600f, true, true, false, playerTransform));
            camera.Set(new Camera(cameraTransform, Camera.CameraType.Orthographic));
        }

        public override ISystem<GameTime> InitializeUpdateSystems()
        {
            return new SequentialSystem<GameTime>(
                //core
                new PhysicsUpdateSystem(World, MainRunner),
                new ActionSystem<GameTime>(UpdateInput),

                //registration
                new ControllerRegistrationSystem(World, MainRunner),
                new CollisionRegistrationSystem(World, MainRunner),
                new OnEarthRegistrationSystem(World, MainRunner),

                //input
                new InputSystem(World, MainRunner),
                new CameraControllingSystem(World, MainRunner),
                new PlayerControllingSystem(World, MainRunner),
                new ProfilerControllingSystem(World, MainRunner),

                //changing in world space
                new FollowingSystem(World, MainRunner),
                new CameraFollowingZoomSystem(World, MainRunner),
                new TranslationSystem(World, MainRunner),
                new RotationSystem(World, MainRunner),
                new PhysicTranslationSystem(World, MainRunner),
                new PhysicRotationSystem(World, MainRunner),
                new JumpSystem(World, MainRunner),

                //sync
                new CameraTransformSyncSystem(World, MainRunner),
                new PhysicsSyncSystem(World, MainRunner)
            );
        }

        public override ISystem<GameTime> InitializeDrawSystems()
        {
            return new SequentialSystem<GameTime>(
                new ShadersPassingSystem(World, MainRunner),
                new SpriteRenderingSystem(World, MainRunner, SpriteBatch),
                new ProfilingSystem(World, MainRunner, SpriteBatch)
            );
        }

        private void UpdateInput(GameTime gameTime)
        {
            GameSettings.Instance.ExtendedStates = new ExtendedStates(MouseExtended.GetState(), KeyboardExtended.GetState());
        }
    }
}