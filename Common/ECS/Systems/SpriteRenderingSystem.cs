using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Common.ECS.Components;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace Common.ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(SpriteRenderer))]
    public partial class SpriteRenderingSystem : AEntitySetSystem<GameTime>
    {
        [ConstructorParameter]
        private SpriteBatch spriteBatch;
        
        [Update]
        private void Update(ref SpriteRenderer renderer, ref Transform transform)
        {
            var camera = World.Get<Camera>()[0];
            var vec2Position = new Vector2(transform.Position.X, transform.Position.Y);
            var vec2Scale = new Vector2(transform.Scale.X, transform.Scale.Y);

            //So basically, a few things were made to invert Y axis so it could grow going from down to up:
            //1) switching RasterizerState in spriteBatch begin call to cull clockwise
            //2) inversing scale of sprite in spriteBatch draw call
            //3) changing 'Up' vector by inversing M22 element of transform world matrix
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, RasterizerState.CullClockwise, renderer.Effect);
            spriteBatch.Draw(renderer.Sprite, vec2Position, transform.OneRotation, -vec2Scale);
            spriteBatch.End();
        }
    }
}