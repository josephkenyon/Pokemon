using LocationDesigner.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LocationDesigner.FileHandling
{
    public class TextureManager
    {
        public static Texture2D BackgroundTileSetTexture { get; set; } 
        public static Texture2D ForegroundTileSetTexture { get; set; }
        public static Texture2D GrassTileSetTexture { get; set; }

        public static Texture2D GetTexture2DFromBitmap(Bitmap bitmap)
        {
            if (GraphicsManager.GraphicsDevice != null && bitmap != null)
            {
                using (MemoryStream s = new MemoryStream())
                {
                    bitmap.Save(s, ImageFormat.Png);
                    s.Seek(0, SeekOrigin.Begin);
                    return Texture2D.FromStream(GraphicsManager.GraphicsDevice, s);
                }
            }

            return null;
        }
    }
}
