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
        public static string BackgroundBitmapPath { get; private set; }
        public static string ForegroundBitmapPath { get; private set; }

        public static void SetBackgroundBitmapPath(string path)
        {
            BackgroundBitmapPath = path;
            WriteCacheFile();
        }

        public static void SetForegroundBitmapPath(string path)
        {
            ForegroundBitmapPath = path;
            WriteCacheFile();
        }

        public static void ReadCacheFile() {
            if (File.Exists(CacheFileName)) {
                string[] lines = File.ReadAllLines(CacheFileName);

                if (lines.Length > 0)
                {
                    BackgroundBitmapPath = lines[0];

                    if (lines.Length > 1)
                    {
                        ForegroundBitmapPath = lines[1];
                    }
                    else {
                        ForegroundBitmapPath = "";
                    }
                }
                else {
                    BackgroundBitmapPath = "";
                    ForegroundBitmapPath = "";
                }
            }
        }

        private static void WriteCacheFile()
        {
            if (!File.Exists(CacheFileName))
            {
                File.Create(CacheFileName);
            }

            File.WriteAllLines(CacheFileName, new string[] { BackgroundBitmapPath, ForegroundBitmapPath });
        }
    }
}
