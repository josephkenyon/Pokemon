using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.Graphics
{
    public static class GraphicsHelper
    {
        public static Rectangle GetSourceRectangle(Vector position, Vector size) { 
            return new Rectangle((position * Constants.TileSize).ToPoint(), (size * Constants.TileSize).ToPoint());
        }
    }
}
