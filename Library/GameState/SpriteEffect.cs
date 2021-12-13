using Library.Base;
using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.GameState
{
    public class SpriteEffect : FrameState, IAnimatedAsset, IBaseDrawableObject
    {
        public TextureName TextureName { get; set; }
        public Vector SpritePosition { get; set; }
        public Vector Position { get; set; }
        public Vector Size { get; set; }
        public int NumFrames { get; set; }
        public bool Repeating { get; set; }
        public SpriteEffect() : base(null)
        {
        }

        public bool Update()
        {
            IncrementFrame();

            if (FrameSkip == 0 && CurrentFrame == 0 && !Repeating) {
                return false;
            }

            return true;
        }

        public Vector GetPosition() => Position * Constants.ScaledTileSize;

        public Rectangle GetSourceRectangle() => new Rectangle(new Point((SpritePosition.X + CurrentFrame) * Constants.TileSize, SpritePosition.Y), (Size * Constants.TileSize).ToPoint());

        public TextureName GetTextureName() => TextureName;
    }
}
