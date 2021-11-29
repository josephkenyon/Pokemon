using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.Graphics
{
    public static class GraphicsManager
    {
        private static GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public static GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice; 

        public static void Initialize(GraphicsDeviceManager graphics) {
            GraphicsDeviceManager = graphics;

            GraphicsDeviceManager.PreferredBackBufferWidth = Constants.ResolutionWidth;
            GraphicsDeviceManager.PreferredBackBufferHeight = Constants.ResolutionHeight;
            GraphicsDeviceManager.IsFullScreen = false;
            GraphicsDeviceManager.ApplyChanges();
        }
    }
}
