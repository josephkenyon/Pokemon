using LocationDesigner.Domain;
using System.Collections.Generic;

namespace LocationDesigner.World
{
    public class LocationLayoutJson
    {
        public List<TileJson> BackgroundTiles { get; set; }
        public List<TileJson> ForegroundTiles { get; set; }
        public List<TileJson> SuperForegroundTiles { get; set; }
        public List<TileJson> LocationDoodads { get; set; }
        public List<SignJson> Signs { get; set; }
        public List<PortalJson> Portals { get; set; }

        public LocationLayoutJson()
        {
            BackgroundTiles = new List<TileJson>();
            ForegroundTiles = new List<TileJson>();
            SuperForegroundTiles = new List<TileJson>();
            LocationDoodads = new List<TileJson>();
            Signs = new List<SignJson>();
            Portals = new List<PortalJson>();
        }
    }
}
