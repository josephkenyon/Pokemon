
using Library.Domain;

namespace Library.GameState
{
    public class DoodadFrameState : FrameState
    {
        public DoodadFrameState(IAnimatedAsset parentAsset) : base(parentAsset)
        {
        }

        public override int FrameSkipConstant => Constants.DoodadFrameSkip;
    }
}
