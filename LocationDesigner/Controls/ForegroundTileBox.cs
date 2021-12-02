﻿using LocationDesigner.FileHandling;
using System.Drawing;
using System.Windows.Forms;

namespace LevelDesigner.Controls
{
    public class ForegroundTileBox : TileBox
    {
        public static TileBox LastClicked { get; private set; }

        public void Setup(Point spriteLocation)
        {
            Image = CropImage(BitmapManager.ForegroundTileSetBitmap, spriteLocation);
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

            BackgroundTileBox.ClearLastClicked();
            DoodadTileBox.ClearLastClicked();
        }

        public static void ClearLastClicked()
        {
            LastClicked?.ResetLastClicked();
            LastClicked = null;
        }
    }
}