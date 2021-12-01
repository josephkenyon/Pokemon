namespace LocationDesigner.Domain
{
    public class Constants
    {
        public static readonly int TileSize = 16;
        public static readonly int ScaledTileSize = 32;

        public static readonly int RenderWidth = 40;
        public static readonly int RenderHeight = 30;

        public static float Scaler => ScaledTileSize / TileSize;
    }
}
