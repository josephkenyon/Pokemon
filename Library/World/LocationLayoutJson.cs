using Library.Domain;
using System.Collections.Generic;

namespace Library.World
{
    public class LocationLayoutJson
    {
        public List<TileJson> BackgroundTiles { get; set; }
        public List<TileJson> ForegroundTiles { get; set; }
        public List<TileJson> LocationDoodads { get; set; }
        public List<PortalJson> Portals { get; set; }
        public List<SignJson> Signs { get; set; }
    }
}
