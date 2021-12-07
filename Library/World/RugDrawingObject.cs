using Library.Base;
using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.World
{
    public class RugDrawingObject : IBaseDrawableObject
    {
        public Point Position { get; set; }

        public Vector GetPosition() => new Vector(Position) * Constants.ScaledTileSize - Vector.One * Constants.ScaledTileSize / 2;

        public Rectangle GetSourceRectangle() => new Rectangle(0, 1 * Constants.TileSize, 2 * Constants.TileSize, 2 * Constants.TileSize);

        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.Effects];

        public TextureName GetTextureName() => TextureName.Effects;
    }
}