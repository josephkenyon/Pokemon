using LocationDesigner.FileHandling;
using System.Drawing;
using System.Windows.Forms;

namespace LevelDesigner.Controls
{
    public class BackgroundTileBox : TileBox
    {
        public static TileBox LastClicked { get; private set; }

        public override void Setup(Point spriteLocation)
        {
            Image = CropImage(BitmapManager.BackgroundTileSetBitmap, spriteLocation);
            SetupParameters(spriteLocation);
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

            ForegroundTileBox.ClearLastClicked();
        }

        public static void ClearLastClicked()
        {
            LastClicked?.ResetLastClicked();
            LastClicked = null;
        }
    }
}