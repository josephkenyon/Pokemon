using LocationDesigner.Domain;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LevelDesigner.Controls
{
    public abstract class TileBox : PictureBox
    {
        public Point SpriteLocation { get; private set; }

        public bool IsSelected() => BorderStyle == BorderStyle.Fixed3D;

        protected abstract void DeSelectOthers();

        protected void SetupParameters(Point spriteLocation)
        {
            SpriteLocation = spriteLocation;

            Width = Constants.ScaledTileSize;
            Height = Constants.ScaledTileSize;
            Left = Constants.ScaledTileSize * spriteLocation.X;
            Top = Constants.ScaledTileSize * spriteLocation.Y;

            SizeMode = PictureBoxSizeMode.StretchImage;

            Click += (this.MyButtonHandler);
        }

        protected static Image CropImage(Bitmap img, Point spriteLocation)
        {
            Bitmap bmpCrop = img.Clone(
                new Rectangle(
                    Constants.TileSize * spriteLocation.X,
                    Constants.TileSize * spriteLocation.Y,
                    Constants.TileSize, Constants.TileSize
                    ),
                img.PixelFormat
            );
            return (bmpCrop);
        }

        void MyButtonHandler(object sender, EventArgs e)
        {
            OnSelect();
        }

        public void OnSelect()
        {
            bool selected = IsSelected();
            DeSelectOthers();

            if (!selected)
            {
                BorderStyle = BorderStyle.Fixed3D;
                BackColor = Color.LightBlue;
            }
            else
            {
                OnDeSelect();
            }
        }

        public void OnDeSelect()
        {
            BorderStyle = BorderStyle.None;
            BackColor = Color.White;
        }
    }
}