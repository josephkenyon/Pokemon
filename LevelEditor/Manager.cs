using Library.Graphics;
using Library.World;
using Microsoft.Xna.Framework.Graphics;

namespace LevelEditor
{
    public static class Manager
    {
        public static LocationLayout LocationLayout { get; set; }

        public static bool Active() => LocationLayout != null;

        public static void Initialize() {
            LocationLayout = new LocationLayout();
        }

        public static void Draw(SpriteBatch spriteBatch) {
        }
        public static void Update()
        {

        }
    }
}
