using Library.Base;
using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class Tile : IBaseDrawableObject, IAnimatedAsset, ICollidable
    {
        public TextureName TextureName { get; set; }
        public Vector Position { get; set; }
        public Vector SpritePosition { get; set; }
        public int? NumFrames { get; set; }
        public FrameState FrameState { get; set; }

        public Tile() {
            FrameState = new DoodadFrameState();
        }

        public bool Update()
        {
            if (FrameState != null) {
                FrameState.IncrementFrame();
            }

            return true;
        }

        public Vector GetPosition() => Position * Constants.ScaledTileSize;

        public Rectangle GetSourceRectangle() => new Rectangle(
            location: new Point((SpritePosition.X + GetFrameOffset()) * Constants.TileSize, SpritePosition.Y * Constants.TileSize),
            size: new Vector(Constants.TileSize).ToPoint()
        );

        private int GetFrameOffset() {
            if (FrameState != null) {
                return FrameState.CurrentFrame;
            }

            return 0;
        }

        public TextureName GetTextureName() => TextureName;

        public Rectangle GetCollisionRectangle() => new Rectangle(GetPosition().ToPoint(), (Vector.One * Constants.ScaledTileSize).ToPoint());
    }
}
