using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Common.ECS.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Systems
{
    [With(typeof(SpriteRenderer))]
    [With(typeof(Transform))]
    public partial class ShadersPassingSystem : AEntitySetSystem<GameTime>
    {
        [Update]
        private void Update(ref Transform transform, ref SpriteRenderer spriteRenderer)
        {
            if(spriteRenderer.Effect != null)
            {
                var cameras = World.Get<Camera>();

                foreach (var camera in cameras)
                {
                    spriteRenderer.Effect.Parameters["WorldMatrix"].SetValue(Matrix.Identity);
                    spriteRenderer.Effect.Parameters["ViewMatrix"].SetValue(camera.ViewMatrix);
                    spriteRenderer.Effect.Parameters["ProjectionMatrix"].SetValue(camera.ProjectionMatrix);
                }
            }
        }
    }
}