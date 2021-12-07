using Library.Assets;
using System.Collections.Generic;

namespace Library.World
{
    public class LocationLayoutJson
    {
        public List<int> BackgroundTilesPositionX { get; set; }
        public List<int> BackgroundTilesPositionY { get; set; }
        public List<int> BackgroundTilesSpriteX { get; set; }
        public List<int> BackgroundTilesSpriteY { get; set; }

        public List<int> ForegroundTilesPositionX { get; set; }
        public List<int> ForegroundTilesPositionY { get; set; }
        public List<int> ForegroundTilesSpriteX { get; set; }
        public List<int> ForegroundTilesSpriteY { get; set; }

        public List<TileJson> LocationDoodads { get; set; }
        public List<SignJson> Signs { get; set; }
        public List<PortalJson> Portals { get; set; }
        public List<NPCJson> InitialNPCS { get; set; }

        public LocationLayoutJson()
        {
            InitialNPCS = new List<NPCJson>();
        }
    }
}
