using System.IO;
using Common.Settings;
using FontStashSharp;

namespace Common.ECS.Components
{
    public struct FontBase
    {
        private FontSystem fontSystem;

        public SpriteFontBase Font;

        public FontBase(string fontName, int fontSize)
        {
            fontSystem = FontSystemFactory.Create(GameSettings.Instance.GraphicsDevice);
            fontSystem.AddFont(File.ReadAllBytes($@"Content/Fonts/{fontName}"));
            Font = fontSystem.GetFont(fontSize);
        }
    }
}