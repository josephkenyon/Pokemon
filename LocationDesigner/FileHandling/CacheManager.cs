using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationDesigner.FileHandling
{
    public class CacheManager
    {
        private static string CacheFileName => Environment.CurrentDirectory + "\\cache.txt";
        public static string TileSetBitmapsPath { get; private set; }

        public static string BackgroundBitmapPath => TileSetBitmapsPath + "\\backgroundTileSet.png";
        public static string ForegroundBitmapPath => TileSetBitmapsPath + "\\foregroundTileSet.png";
        public static string GrassBitmapPath => TileSetBitmapsPath + "\\grassTileSet.png";
        public static string AnimationBitmapPath => TileSetBitmapsPath + "\\animationTileSet.png";

        public static void SetTileSetBitmapsPath(string path)
        {
            TileSetBitmapsPath = path;
            WriteCacheFile();
        }

        public static void ReadCacheFile() {
            if (File.Exists(CacheFileName)) {
                string[] lines = File.ReadAllLines(CacheFileName);

                if (lines.Length > 0)
                {
                    TileSetBitmapsPath = lines[0];
                }
                else {
                    TileSetBitmapsPath = "";
                }
            }
        }

        private static void WriteCacheFile()
        {
            if (!File.Exists(CacheFileName))
            {
                File.Create(CacheFileName);
            }

            File.WriteAllLines(CacheFileName, new string[] { TileSetBitmapsPath });
        }
    }
}
