using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Library.Domain;
using Library.Content;
using Library.GameState;
using Library.Graphics;
using Library.Base;
using Library.World;
using Library.Cutscenes;
using Library.Controls;

namespace GameManager
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager Graphics { get; set; }
        private SpriteBatch SpriteBatch { get; set; }
        public int SaveSlot { get; private set; }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this) { PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8 };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.IsBorderless = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            GraphicsManager.Initialize(Graphics);

            Encyclopedia.Load(Content);
            ItemManager.Load(Content);
            MoveManager.Load(Content);
            TypeManager.Load(Content);
            TextureManager.Load(Content);
            WorldManager.Load(Content);
            LocationManager.Load(Content);
            DrawingManager.Initialize(Content);

            GameStateManager.Initialize();
            SpecialActionManager.Initialize();

            BaseDrawingManager.Initialize();
            StitchDrawingManager.Initialize();
            ControlsManager.Initialize();

            CutsceneManager.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            ControlsManager.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                FileHelper.SaveState(SaveSlot);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F7))
            {
                FileHelper.LoadState(SaveSlot);
            }

            if (!GameStateManager.Instance.Update())
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GameStateManager.Instance.Draw(SpriteBatch);

            base.Draw(gameTime);
        }
    }
}