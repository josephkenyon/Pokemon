namespace Library.Domain
{
    public class Constants
    {
        public static readonly int TileSize = 16;
        public static readonly int ScaledTileSize = 80;

        public static readonly int WidthTiles = 24;
        public static readonly int HeightTiles = 13;

        public static readonly int ResolutionWidth = 1920;
        public static readonly int ResolutionHeight = 1080;

        public static readonly int DoodadFrameSkip = 15;
        public static readonly int DefaultFrameSkip = 5;
        public static readonly int NPCDefaultFrameCount = 2;
        public static readonly int CharacterSpeed = 10;

        public static readonly int TransitionTime = 60;
        public static readonly int ItemDebounce = 10;
        public static readonly int StandardDebounce = 8;

        public static readonly float DefaultWildBattleChance = 0.1f;

        public static readonly int NPCRandomConstant = 1600;
        public static float Scaler => ScaledTileSize / TileSize;

        public static string PlayerName = "Ash";
    }
}
