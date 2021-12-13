namespace Library.GameState
{
    public interface IAnimatedAsset
    {
        bool Update();
        virtual int NumberOfFrames => 3;
    }
}