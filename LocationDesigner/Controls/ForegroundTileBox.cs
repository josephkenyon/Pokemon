using LocationDesigner.Controls;
using LocationDesigner.FileHandling;
using System.Drawing;

namespace LevelDesigner.Controls
{
    public class ForegroundTileBox : TileBox
    {
        public void Setup(Point spriteLocation)
        {
            Image = CropImage(BitmapManager.ForegroundTileSetBitmap, spriteLocation);
            SetupParameters(spriteLocation);
        }

        protected override void DeSelectOthers()
        {
            FormHelper.BackgroundTiles.ForEach(tile => tile.OnDeSelect());
            FormHelper.DoodadTiles.ForEach(tile => tile.OnDeSelect());
        }
    }
}