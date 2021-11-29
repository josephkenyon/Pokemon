using Library.Assets;
using Library.Base;
using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class Tile : IBaseDrawableObject
    {
        public TextureName TextureName { get; set; }
        public Vector Position { get; set; }
        public Vector SpritePosition { get; set; }
        public int? NumFrames { get; set; }

        public Vector GetPosition() => Position * Constants.ScaledTileSize;

        public Rectangle GetSourceRectangle() => new Rectangle(
            location: (SpritePosition * Constants.TileSize).ToPoint(),
            size: new Vector(Constants.TileSize).ToPoint()
        );

        public TextureName GetTextureName() => TextureName;
    }
}
