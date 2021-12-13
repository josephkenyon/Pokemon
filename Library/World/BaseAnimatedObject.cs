using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public abstract class BaseAnimatedObject : BaseObject, IAnimatedAsset
    {
        public int? NumFrames { get; set; }
        public FrameState FrameState { get; set; }

        public bool Update()
        {
            if (FrameState != null)
            {
                FrameState.IncrementFrame();
            }

            return true;
        }
        public override Rectangle GetSourceRectangle() => new Rectangle(
            location: new Point((SpritePosition.X + GetFrameOffset()) * Constants.TileSize, SpritePosition.Y * Constants.TileSize),
            size: new Vector(Constants.TileSize).ToPoint()
        );

        private int GetFrameOffset()
        {
            if (FrameState != null)
            {
                return FrameState.CurrentFrame;
            }

            return 0;
        }
    }
}
