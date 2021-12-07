using Microsoft.Xna.Framework;
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
        public List<NPCJson> InitialNPCS { get; set; }

        public LocationLayoutJson()
        {

            BackgroundTiles = new List<TileJson>();
            ForegroundTiles = new List<TileJson>();
            SuperForegroundTiles = new List<TileJson>();
            LocationDoodads = new List<TileJson>();
            Signs = new List<SignJson>();
            Portals = new List<PortalJson>();
            InitialNPCS = new List<NPCJson>();
        }

        public LocationLayoutJson(LocationLayoutJsonCompact compact)
        {
            BackgroundTiles = new List<TileJson>();
            for (int i = 0; i < compact.BackgroundTilesPositionX.Count; i++)
            {
                BackgroundTiles.Add(new TileJson
                {
                    Position = new Point(compact.BackgroundTilesPositionX[i], compact.BackgroundTilesPositionY[i]),
                    SpritePosition = new Point(compact.BackgroundTilesSpriteX[i], compact.BackgroundTilesSpriteY[i])
                });
            }

            ForegroundTiles = new List<TileJson>();
            for (int i = 0; i < compact.ForegroundTilesPositionX.Count; i++)
            {
                ForegroundTiles.Add(new TileJson
                {
                    Position = new Point(compact.ForegroundTilesPositionX[i], compact.ForegroundTilesPositionY[i]),
                    SpritePosition = new Point(compact.ForegroundTilesSpriteX[i], compact.ForegroundTilesSpriteY[i])
                });
            }

            SuperForegroundTiles = new List<TileJson>();
            LocationDoodads = compact.LocationDoodads;
            Signs = compact.Signs;
            Portals = compact.Portals;
            InitialNPCS = compact.InitialNPCS;
        }
    }
}
