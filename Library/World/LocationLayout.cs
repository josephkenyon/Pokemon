using System;
using System.Collections.Generic;
using System.Text;
using Library.Assets;
using Library.Base;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.World
{
    public class LocationLayout
    {
        public Dictionary<Point, Tile> BackgroundTiles { get; set; }
        public Dictionary<Point, Tile> ForegroundTiles { get; set; }
        public List<Character> InitialCharacters { get; set; }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            BaseDrawingManager.DrawBatch(spriteBatch, BackgroundTiles.Values);
            BaseDrawingManager.DrawBatch(spriteBatch, ForegroundTiles.Values);
        }

        public void DrawForeground(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }
    }

    public class LocationLayoutJson
    {
        public List<Tile> BackgroundTiles { get; set; }
        public List<Tile> ForegroundTiles { get; set; }
        public List<Character> InitialCharacters { get; set; }
    }
}
