using Library.Base;
using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Library.World
{
    public class WorldManager
    {
        public static List<LocationStitch> LocationStitches { get; set; }

        public static void InitializeStiches()
        {
            LocationStitches.ForEach(stitch =>
            {
                if (stitch.Orientation == Orientation.Vertical)
                {
                    List<Point> backgroundTilesA = LocationManager.LocationLayouts[stitch.LocationA].BackgroundTiles.Keys.OrderBy(tile => tile.Y).ToList();
                    List<Point> backgroundTilesB = LocationManager.LocationLayouts[stitch.LocationB].BackgroundTiles.Keys.OrderBy(tile => tile.Y).ToList();

                    stitch.LocationADistance = backgroundTilesA.Last().Y - backgroundTilesA.First().Y;
                    stitch.LocationBDistance = backgroundTilesB.Last().Y - backgroundTilesB.First().Y;
                }
                else
                {
                    List<Point> backgroundTilesA = LocationManager.LocationLayouts[stitch.LocationA].BackgroundTiles.Keys.OrderBy(tile => tile.X).ToList();
                    List<Point> backgroundTilesB = LocationManager.LocationLayouts[stitch.LocationB].BackgroundTiles.Keys.OrderBy(tile => tile.X).ToList();

                    stitch.LocationADistance = backgroundTilesA.Last().X - backgroundTilesA.First().X;
                    stitch.LocationBDistance = backgroundTilesB.Last().X - backgroundTilesB.First().X;
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

        public static Point GetStitchLocation(Point newLocation)
        {
            return newLocation - StitchDrawingManager.StitchHelperObject.OffsetPoint;
        }
    }
}