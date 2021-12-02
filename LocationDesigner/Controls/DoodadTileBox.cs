using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using System.Drawing;
using System.Windows.Forms;

namespace LevelDesigner.Controls
{
    public class DoodadTileBox : TileBox
    {
        public static TileBox LastClicked { get; private set; }

        public void Setup(LocationDoodad location)
        {
            Image = CropImage(BitmapManager.GrassTileSetBitmap, new Point(0, (int) location));
            SetupParameters(new Point(0, (int)location));
        }

        public override void ResetLastClicked()
        {
            if (LastClicked != null)
            {
                LastClicked.BorderStyle = BorderStyle.None;
                LastClicked.BackColor = Color.Transparent;
            }
        }

        public override void SetLastClicked(TileBox tileBox)
        {
            LastClicked = tileBox;

            BackgroundTileBox.ClearLastClicked();
            ForegroundTileBox.ClearLastClicked();
        }

        public static void ClearLastClicked()
        {
            LastClicked?.ResetLastClicked();
            LastClicked = null;
        }
    }
}