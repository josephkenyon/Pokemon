using System.Collections.Generic;

namespace LocationDesigner.World
{
    public class LocationLayoutJsonCompact
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
        public List<CapturedPokemonJson> InitialCapturedPokemon { get; set; }
        public List<ItemJson> InitialItems { get; set; }
        public List<LocationPokemonJson> LocationPokemonJson { get; set; }

        public LocationLayoutJsonCompact(LocationLayoutJson layoutJson)
        {
            if (layoutJson != null)
            {
                BackgroundTilesPositionX = new List<int>();
                BackgroundTilesPositionY = new List<int>();
                BackgroundTilesSpriteX = new List<int>();
                BackgroundTilesSpriteY = new List<int>();

                layoutJson.BackgroundTiles.ForEach(tileJson =>
                {
                    BackgroundTilesPositionX.Add(tileJson.Position.X);
                    BackgroundTilesPositionY.Add(tileJson.Position.Y);
                    BackgroundTilesSpriteX.Add(tileJson.SpritePosition.X);
                    BackgroundTilesSpriteY.Add(tileJson.SpritePosition.Y);
                });

                ForegroundTilesPositionX = new List<int>();
                ForegroundTilesPositionY = new List<int>();
                ForegroundTilesSpriteX = new List<int>();
                ForegroundTilesSpriteY = new List<int>();

                layoutJson.ForegroundTiles.ForEach(tileJson =>
                {
                    ForegroundTilesPositionX.Add(tileJson.Position.X);
                    ForegroundTilesPositionY.Add(tileJson.Position.Y);
                    ForegroundTilesSpriteX.Add(tileJson.SpritePosition.X);
                    ForegroundTilesSpriteY.Add(tileJson.SpritePosition.Y);
                });

                LocationDoodads = layoutJson.LocationDoodads;
                Signs = layoutJson.Signs;
                Portals = layoutJson.Portals;
                InitialNPCS = layoutJson.InitialNPCS;
                InitialCapturedPokemon = layoutJson.InitialCapturedPokemon;
                InitialItems = layoutJson.InitialItems;
                LocationPokemonJson = layoutJson.LocationPokemonJson;
            }
        }
    }
}
