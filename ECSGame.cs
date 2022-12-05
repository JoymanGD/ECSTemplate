using System;
using Ecs = DefaultEcs;
using Physics = Genbox.VelcroPhysics.Dynamics;
using DefaultEcs.Threading;
using Common.Settings;
using MonoGame.Extended.Screens;
using Common.Core.Scenes;
using MonoGame.ImGui;
using ImGuiNET;

namespace Common
{
    public class ECSGame : Game
    {
        private GraphicsDeviceManager Graphics;
        private ScreenManager ScreenManager;
        private bool gameScreenUpdating = true;
        private bool gameScreenDrawing = true;
        private SpriteBatch spriteBatch;
        private ImGUIRenderer guiRenderer;

        public ECSGame()
        {
            SetBasicConfiguration();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            InitializeScreenManager();
            InitializeProperties();
            InitizalizeGameSettings();
            StartGame();
        }

        private void InitializeProperties()
        {
            guiRenderer = new ImGUIRenderer(this).Initialize().RebuildFontAtlas();
        }

        private void SetBasicConfiguration()
        {
            //Graphics settings
            Graphics = new GraphicsDeviceManager(this);
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            
            Graphics.PreferredBackBufferWidth = 1600;
            Graphics.PreferredBackBufferHeight = 900;
            Graphics.IsFullScreen = false;

            //Other
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void InitializeScreenManager()
        {
            ScreenManager = new ScreenManager();
        }

        private void InitizalizeGameSettings()
        {
            //initialize GameSetting class
            var settings = GameSettings.Instance;
            
            settings.GraphicsDevice = GraphicsDevice;
            settings.Graphics = Graphics;
            settings.ContentManager = Content;
            settings.Game = this;
            settings.EcsWorld = new Ecs.World();
            settings.MainRunner = new DefaultParallelRunner(Environment.ProcessorCount);
            settings.ScreenManager = ScreenManager;
            settings.DefaultSpriteShader = Content.Load<Effect>("Shaders/SpriteUnlit");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            settings.SpriteBatch = spriteBatch;
        }

        private void StartGame()
        {
            ScreenManager.LoadScreen(new MenuScreen(this));
        }

        protected override void Draw(GameTime gameTime)
        {
            if(gameScreenDrawing)
            {
                ScreenManager.Draw(gameTime);
            }

            guiRenderer.BeginLayout(gameTime);

                ImGui.BeginMainMenuBar();
                    ImGui.Button("File");
                    ImGui.Button("Edit");
                    ImGui.Button("About");
                ImGui.EndMainMenuBar();

            guiRenderer.EndLayout();
        }

        protected override void Update(GameTime gameTime)
        {
            if(gameScreenUpdating)
            {
                ScreenManager.Update(gameTime);
            }

            base.Update(gameTime);
        }
    }
}