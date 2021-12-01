using System.Collections.Generic;

namespace LocationDesigner.World
{
    public class LocationLayoutJson
    {
        public List<TileJson> BackgroundTiles { get; set; }
        public List<TileJson> ForegroundTiles { get; set; }

        public LocationLayoutJson()
        {
            BackgroundTiles = new List<TileJson>();
            ForegroundTiles = new List<TileJson>();
        }
    }
}
