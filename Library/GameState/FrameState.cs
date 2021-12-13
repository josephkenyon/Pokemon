using Library.Domain;

namespace Library.GameState
{
    public class FrameState
    {
        private static readonly int DefaultNumFrames = 3;

        public int CurrentFrame { get; set; }
        public int FrameSkip { get; set; }
        public virtual int FrameSkipConstant => Constants.DefaultFrameSkip;
        public IAnimatedAsset ParentAsset { get; private set; }

        public FrameState(IAnimatedAsset parentAsset)
        {
            ParentAsset = parentAsset;
        }

        public void IncrementFrame()
        {
            if (FrameSkip == FrameSkipConstant)
            {
                CurrentFrame = CurrentFrame == (ParentAsset != null ? ParentAsset.NumberOfFrames : DefaultNumFrames) - 1 ? 0 : CurrentFrame + 1;
                FrameSkip = 0;
            }
            else
            {
                FrameSkip += 1;
            }
        }
    }
}
