using Common.Settings;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace Common.ECS.Components
{
    public struct SpriteRenderer
    {
        public Sprite Sprite { get; private set; }
        public Effect Effect { get; private set; }

        public SpriteRenderer(Sprite sprite)
        {
            Sprite = sprite;
            Effect = GameSettings.Instance.DefaultSpriteShader?.Clone();
        }
        
        public SpriteRenderer(Sprite sprite, Effect effect)
        {
            Sprite = sprite;
            Effect = effect;
        }
    }
}