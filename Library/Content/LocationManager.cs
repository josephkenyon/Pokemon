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
using Library.World.Json;

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
                    using StreamReader r = new StreamReader(filePath);

                    LocationLayoutJson locationLayoutJson = JsonConvert.DeserializeObject<LocationLayoutJson>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);

                    LocationLayout locationLayout = new LocationLayout();

                    for (int i = 0; i < locationLayoutJson.BackgroundTilesPositionX.Count; i++)
                    {
                        locationLayout.BackgroundTiles.Add(
                            new Point(locationLayoutJson.BackgroundTilesPositionX[i], locationLayoutJson.BackgroundTilesPositionY[i]), new Tile
                            {
                                TextureName = TextureName.Background,
                                Position = new Vector(locationLayoutJson.BackgroundTilesPositionX[i], locationLayoutJson.BackgroundTilesPositionY[i]),
                                SpritePosition = new Vector(locationLayoutJson.BackgroundTilesSpriteX[i], locationLayoutJson.BackgroundTilesSpriteY[i]),
                            });
                    }

                    for (int i = 0; i < locationLayoutJson.ForegroundTilesPositionX.Count; i++)
                    {
                        locationLayout.ForegroundTiles.Add(
                            new Point(locationLayoutJson.ForegroundTilesPositionX[i], locationLayoutJson.ForegroundTilesPositionY[i]), new Tile
                            {
                                TextureName = TextureName.Foreground,
                                Position = new Vector(locationLayoutJson.ForegroundTilesPositionX[i], locationLayoutJson.ForegroundTilesPositionY[i]),
                                SpritePosition = new Vector(locationLayoutJson.ForegroundTilesSpriteX[i], locationLayoutJson.ForegroundTilesSpriteY[i]),
                            });
                    }

                    locationLayoutJson.LocationDoodads.ForEach(tile =>
                    {
                        if (tile.LocationDoodad == LocationDoodad.Grass)
                        {
                            locationLayout.ForegroundGrassTiles.Add(tile.Position, new Tile
                            {
                                TextureName = TextureName.Grass,
                                Position = new Vector(tile.Position),
                                SpritePosition = new Vector(1, 0),
                            });

                            locationLayout.BackgroundGrassTiles.Add(tile.Position, new Tile
                            {
                                TextureName = TextureName.Grass,
                                Position = new Vector(tile.Position),
                                SpritePosition = new Vector(2, 0),
                            });
                        }
                        else if (tile.LocationDoodad == LocationDoodad.Red_Flower)
                        {
                            locationLayout.BackgroundGrassTiles.Add(tile.Position, new Tile
                            {
                                TextureName = TextureName.Grass,
                                Position = new Vector(tile.Position),
                                SpritePosition = new Vector(0, 1),
                                NumFrames = 5
                            });
                        }
                    });

                    locationLayoutJson.LocationPokemonJson.ForEach(locationPokemonJson =>
                    {
                        locationLayout.LocationPokemonJson.Add(locationPokemonJson);
                    });

                    locationLayoutJson.Signs.ForEach(signJson =>
                    {
                        locationLayout.Signs.Add(signJson.Position, signJson);
                    });

                    locationLayoutJson.Portals.ForEach(portalJson =>
                    {  
                        locationLayout.Portals.Add(portalJson.Position, portalJson);
                    });

                    locationLayoutJson.InitialCapturedPokemon.ForEach(capturedPokemonJson =>
                    {
                        locationLayout.InitialCapturedPokemon.Add(new CapturedPokemon(capturedPokemonJson));
                    });

                    locationLayoutJson.InitialItems.ForEach(itemJson =>
                    {
                        locationLayout.InitialItems.Add(new Item(itemJson.ItemName, itemJson.Position, count: (int)(itemJson.Count == null ? 1 : itemJson.Count)));
                    });

                    locationLayoutJson.InitialNPCS.ForEach(npcJson =>
                    {
                        List<Message> messages = new List<Message>();
                        npcJson.Messages.ForEach(message => messages.Add(new Message(message)));

                        NPC npc = new NPC
                        {
                            Name = npcJson.CharacterName,
                            SpriteLocation = npcJson.SpriteLocation,
                        };

                        npc.CharacterState = new NPCState(npc)
                        {
                            Position = new Vector(npcJson.Position),
                            Messages = messages,
                            Wanders = npcJson.CharacterName == null,
                            CurrentLocation = locationName
                        };

                        if (npcJson.ItemName != null && npcJson.ItemCount != null)
                        {
                            npc.CharacterState.Bag = new Bag();
                            npc.CharacterState.Bag.AddItems((ItemName)npcJson.ItemName, (int)npcJson.ItemCount);
                        }

                        locationLayout.InitialCharacters.Add(npc);
                    });

                    //locationLayoutJson.InitialCharacters.ForEach(character => locationLayout.InitialCharacters.Add(character));
                    LocationName startingLocation = LocationName.PalletTown;
                    if (locationName == startingLocation)
                    {
                        if (!locationLayout.InitialCharacters.Exists(character => character.Name == CharacterName.Ash))
                        {
                            Player player = new Player
                            {
                                Name = CharacterName.Ash
                            };

                            player.CharacterState = new CharacterState(player)
                            {
                                //Position = new Vector(-5, -1) * Constants.ScaledTileSize,
                                Position = new Vector(2, -3) * Constants.ScaledTileSize,
                                CurrentFrame = 1,
                                Direction = Direction.Up,
                                FrameSkip = 0,
                                IsMoving = false,
                                CurrentLocation = locationName
                            };

                            locationLayout.InitialCharacters.Add(player);
                        }
                    }

                    LocationLayouts.Add(locationName, locationLayout);
                }
                else
                {
                    Trace.TraceError("LocationLayout file not found at " + filePath + ".");
                }
            }

            WorldManager.InitializeStiches();
        }
    }
}