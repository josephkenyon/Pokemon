using LocationDesigner.Controls;
using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using System.Drawing;

namespace LevelDesigner.Controls
{
    public class DoodadTileBox : TileBox
    {
        public void Setup(LocationDoodad location)
        {
            Image = CropImage(BitmapManager.GrassTileSetBitmap, new Point(0, (int) location));
            SetupParameters(new Point(0, (int)location));
        }
        protected override void DeSelectOthers()
        {
            FormHelper.BackgroundTiles.ForEach(tile => tile.OnDeSelect());
            FormHelper.ForegroundTiles.ForEach(tile => tile.OnDeSelect());
            FormHelper.DoodadTiles.ForEach(tile => tile.OnDeSelect());
        }
    }
}