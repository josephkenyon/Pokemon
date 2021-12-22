using Library.Base;
using Library.Content;
using Library.Domain;
using Library.GameState;
using Library.GameState.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.World
{
    public class WorldManager
    {
        public static List<LocationStitch> LocationStitches { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            string stichesFilePath = contentManager.RootDirectory + FileHelper.ConfigurationDirectory + FileHelper.LocationStichesFileName + FileHelper.JsonExtension;
            if (File.Exists(stichesFilePath))
            {
                using StreamReader r = new StreamReader(stichesFilePath);
                LocationStitches = JsonConvert.DeserializeObject<List<LocationStitch>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings);
            }
        }

        public static void InitializeStiches()
        {
            LocationStitches.ForEach(stitch =>
            {
                if (stitch.Orientation == Orientation.Vertical)
                {
                    List<Point> backgroundTilesA = LocationManager.LocationLayouts[stitch.LocationA].BackgroundTiles.Keys.OrderBy(tile => tile.Y).ToList();
                    List<Point> backgroundTilesB = LocationManager.LocationLayouts[stitch.LocationB].BackgroundTiles.Keys.OrderBy(tile => tile.Y).ToList();

                    stitch.LocationAGreatest = backgroundTilesA.Last().Y;
                    stitch.LocationALeast = backgroundTilesA.First().Y;
                    stitch.LocationBGreatest = backgroundTilesB.Last().Y;
                    stitch.LocationBLeast = backgroundTilesB.First().Y;
                }
                else
                {
                    List<Point> backgroundTilesA = LocationManager.LocationLayouts[stitch.LocationA].BackgroundTiles.Keys.OrderBy(tile => tile.X).ToList();
                    List<Point> backgroundTilesB = LocationManager.LocationLayouts[stitch.LocationB].BackgroundTiles.Keys.OrderBy(tile => tile.X).ToList();

                    stitch.LocationAGreatest = backgroundTilesA.Last().X;
                    stitch.LocationALeast = backgroundTilesA.First().X;
                    stitch.LocationBGreatest = backgroundTilesB.Last().X;
                    stitch.LocationBLeast = backgroundTilesB.First().X;
                }
            });
        }

        public static void DrawStitch(SpriteBatch spriteBatch, LocationStitch locationStitch)
        {
            StitchHelperObject stitchHelperObject = new StitchHelperObject(locationStitch);
            StitchDrawingManager.StitchHelperObject = stitchHelperObject;

            LocationLayout locationLayout = LocationManager.LocationLayouts[stitchHelperObject.Location];

            locationLayout.DrawBackground(spriteBatch, stitchHelperObject);
            locationLayout.DrawForeground(spriteBatch, stitchHelperObject);
        }

        public static StitchHelperObject GetClosestStitchHelperObject()
        {
            LocationName currentLocationName = GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation;
            List<LocationStitch> stitches = LocationStitches.Where(stitch => stitch.LocationA == currentLocationName || stitch.LocationB == currentLocationName).ToList();
            LocationStitch stitch = stitches.Where(s => s.GetDistanceFromPlayer() < 12).OrderBy(s => s.GetDistanceFromPlayer()).FirstOrDefault();
            if (stitch == null)
            {
                return null;
            }

            return new StitchHelperObject(stitch);
        }

        public static Point GetStitchLocation(StitchHelperObject stitchHelperObject, Point newLocation)
        {
            return newLocation - stitchHelperObject.OffsetPoint;
        }
    }
}