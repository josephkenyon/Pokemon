using Library.Base;
using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public abstract class BaseObject : IBaseDrawableObject, ICollidable
    {
        public TextureName TextureName { get; set; }
        public Vector Position { get; set; }
        public Vector SpritePosition { get; set; }

        public Vector GetPosition() => Position * Constants.ScaledTileSize;

        public virtual Rectangle GetSourceRectangle() => new Rectangle(
            location: new Point(SpritePosition.X * Constants.TileSize, SpritePosition.Y * Constants.TileSize),
            size: new Vector(Constants.TileSize).ToPoint()
        );

        public TextureName GetTextureName() => TextureName;
        public Rectangle GetCollisionRectangle() => new Rectangle(GetPosition().ToPoint(), (Vector.One * Constants.ScaledTileSize).ToPoint());
    }
}
