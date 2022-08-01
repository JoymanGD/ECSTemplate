using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Common.ECS.Components;
using FontStashSharp;

namespace Common.ECS.Systems
{
    [With(typeof(Profiler))]
    [With(typeof(FontBase))]
    public partial class ProfilingSystem : AEntitySetSystem<GameTime>
    {
        [ConstructorParameter]
        private SpriteBatch spriteBatch;
        
        [Update]
        private void Update(in Profiler profiler, in FontBase fontBase, GameTime gameTime)
        {
            var playerEntites = World.GetEntities().With<Player>().With<Transform>().AsSet().GetEntities();
            var cameraEntites = World.GetEntities().With<Camera>().With<Transform>().AsSet().GetEntities();
            var playerTransform = playerEntites[0].Get<Transform>();
            var playerVelocity = playerTransform.DeltaPosition.Length();
            var playerAngularVelocity = playerTransform.OneRotation;
            var elapsedMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
            var elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            var cameraTransform = cameraEntites[0].Get<Transform>();
            var cameraSpeed = cameraTransform.DeltaPosition.Length();
            var cameraToPlayerDirection = playerTransform.Position - cameraTransform.Position;
            var fps = 1/elapsedSeconds;
            spriteBatch.Begin();
            spriteBatch.DrawString(fontBase.Font,

                $"FPS: {fps}" + 
                $"\nElapsed time(ms): {elapsedMilliseconds}" + 
                "\n" + 
                $"\nPlayer velocity: {playerVelocity}" + 
                $"\nPlayer rotation: {playerAngularVelocity}" + 
                $"\nPlayer position: {playerTransform.Position}" + 
                "\n" + 
                $"\nCamera speed: {cameraSpeed}" + 
                $"\nCamera position: {cameraTransform.Position}" + 
                "\n" + 
                $"\nCamera forward: {Vector3.Normalize(cameraTransform.Forward)}",
            
            Vector2.One * 20, Color.White);
            spriteBatch.End();
        }
    }
}