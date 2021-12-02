using Library.Domain;

namespace Library.GameState
{
    public class FrameState
    {
        public int CurrentFrame { get; set; }
        public int FrameSkip { get; set; }
        public virtual int FrameSkipConstant => Constants.DefaultFrameSkip;

        public void IncrementFrame()
        {
            if (FrameSkip == FrameSkipConstant)
            {
                CurrentFrame = CurrentFrame == 2 ? 0 : CurrentFrame + 1;
                FrameSkip = 0;
            }
            else
            {
                FrameSkip += 1;
            }
        }
    }
}
