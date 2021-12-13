using Library.GameState;
namespace Library.World
{
    public class Tile : BaseAnimatedObject
    {
        public Tile() {
            FrameState = new DoodadFrameState(this);
        }
    }
}
