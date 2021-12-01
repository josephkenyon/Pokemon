using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Library.Domain;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using Library.World;
using Microsoft.Xna.Framework;
using Library.Assets;
using Library.GameState;

namespace Library.Content
{
    public static class LocationManager
    {
        public static Dictionary<LocationName, LocationLayout> LocationLayouts { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            LocationLayouts = new Dictionary<LocationName, LocationLayout>();

            foreach (LocationName locationName in Enum.GetValues(typeof(LocationName)))
            {
                string filePath = contentManager.RootDirectory + FileHelper.LocationsDirectory + FileHelper.FormatEnumNameToJson(locationName.ToString());

                if (File.Exists(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        LocationLayoutJson locationLayoutJson = JsonConvert.DeserializeObject<LocationLayoutJson>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);

                        LocationLayout locationLayout = new LocationLayout
                        {
                            BackgroundTiles = new Dictionary<Point, Tile>(),
                            ForegroundTiles = new Dictionary<Point, Tile>(),
                            InitialCharacters = new List<Assets.Character>()
                        };

                        locationLayoutJson.BackgroundTiles.ForEach(tile =>
                        {
                            locationLayout.BackgroundTiles.Add(tile.Position, new Tile {
                                TextureName = TextureName.Background,
                                Position = new Vector(tile.Position),
                                SpritePosition = new Vector(tile.SpritePosition),
                            });
                        });

                        locationLayoutJson.ForegroundTiles.ForEach(tile =>
                        {
                            locationLayout.ForegroundTiles.Add(tile.Position, new Tile
                            {
                                TextureName = TextureName.Foreground,
                                Position = new Vector(tile.Position),
                                SpritePosition = new Vector(tile.SpritePosition),
                            });
                        });

                        //locationLayoutJson.InitialCharacters.ForEach(character => locationLayout.InitialCharacters.Add(character));

                        if (locationName == LocationName.PalletTown)
                        {
                            if (!locationLayout.InitialCharacters.Exists(character => character.Name == Constants.PlayerName)) {
                                locationLayout.InitialCharacters.Add(new Player {
                                    Name = Constants.PlayerName,
                                    TextureName = TextureName.Ash,
                                    SpriteSize = new Vector(1, 2),
                                    CharacterState = new CharacterState { 
                                        Position = new Vector(2) * Constants.ScaledTileSize,
                                        CurrentFrame = 1,
                                        FrameSkip = 0,
                                        IsMoving = false,
                                    }
                                });
                            }
                        }

                        LocationLayouts.Add(locationName, locationLayout);
                    }
                }
                else
                {
                    Trace.TraceError("LocationLayout file not found at " + filePath + ".");
                }
            }
        }
    }
}
