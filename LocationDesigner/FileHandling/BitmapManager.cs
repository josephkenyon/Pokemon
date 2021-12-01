using System.Drawing;

namespace LocationDesigner.FileHandling
{
    public class BitmapManager
    {
        public static Bitmap BackgroundTileSetBitmap { get; private set; } 
        public static Bitmap ForegroundTileSetBitmap { get; private set; }

        public static void SetBackgroundTileSetBitmap(Bitmap bitmap) {
            BackgroundTileSetBitmap = bitmap;
            TextureManager.BackgroundTileSetTexture = TextureManager.GetTexture2DFromBitmap(bitmap);
        }

        public static void SetForegroundTileSetBitmap(Bitmap bitmap)
        {
            ForegroundTileSetBitmap = bitmap;
            TextureManager.ForegroundTileSetTexture = TextureManager.GetTexture2DFromBitmap(bitmap);
        }
    }
}
