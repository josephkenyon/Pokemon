using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocationDesigner.Graphics
{
    public static class GraphicsManager
    {
        public static GraphicsDevice GraphicsDevice { get; private set; }

        public static void Initialize(GraphicsDevice graphics) {
            GraphicsDevice = graphics;
        }
    }
}
