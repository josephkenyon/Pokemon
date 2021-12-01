using LevelDesigner.Controls;
using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using LocationDesigner.World;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LocationDesigner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            CacheManager.ReadCacheFile();

            InitializeBackgroundTileSet(CacheManager.BackgroundBitmapPath);
            InitializeForegroundTileSet(CacheManager.ForegroundBitmapPath);
        }

        private void BackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeBackgroundTileSet(GetFileName());
        }

        private void ForegroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeForegroundTileSet(GetFileName());
        }
        private void InitializeBackgroundTileSet(string fileName)
        {
            Bitmap bitmap = null;
            try
            {
                if (fileName != null)
                {
                    bitmap = new Bitmap(fileName);
                }
            }
            catch (Exception) { }

            if (bitmap != null)
            {
                CacheManager.SetBackgroundBitmapPath(fileName);
                BitmapManager.SetBackgroundTileSetBitmap(bitmap);
                int widthSize = bitmap.Width / Constants.TileSize;
                int heightSize = bitmap.Height / Constants.TileSize;
                for (int i = 0; i < widthSize; i++)
                {
                    for (int l = 0; l < heightSize; l++)
                    {
                        BackgroundTileBox newTile = new BackgroundTileBox();
                        backgroundPanel.Controls.Add(newTile);
                        newTile.Setup(new Point(i, l));
                    }
                }
            }
        }

        private void InitializeForegroundTileSet(string fileName)
        {
            Bitmap bitmap = null;
            try
            {
                if (fileName != null)
                {
                    bitmap = new Bitmap(fileName);
                }
            }
            catch (Exception) { }

            if (bitmap != null)
            {
                CacheManager.SetForegroundBitmapPath(fileName);
                BitmapManager.SetForegroundTileSetBitmap(bitmap);
                int widthSize = bitmap.Width / Constants.TileSize;
                int heightSize = bitmap.Height / Constants.TileSize;
                for (int i = 0; i < widthSize; i++)
                {
                    for (int l = 0; l < heightSize; l++)
                    {
                        ForegroundTileBox newTile = new ForegroundTileBox();
                        foregroundPanel.Controls.Add(newTile);
                        newTile.Setup(new Point(i, l));
                    }
                }
            }
        }

        private string GetFileName() {
            OpenFileDialog theDialog = new OpenFileDialog
            {
                Title = "Open ",
                Filter = "Image files|*.png;*.jpg;*.bmp;",
                InitialDirectory = @"C:\"
            };

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return theDialog.FileName;
                }
                catch (Exception) {
                    MessageBox.Show("File could not be opened.");
                }
            }

            return null;
        }

        private void LaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog theDialog = new SaveFileDialog
            {
                Title = "Open ",
                Filter = "Json file|*.json",
                InitialDirectory = @"C:\"
            };

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                LocationLayoutManager.SaveFile(theDialog.FileName);
            }
        }

        private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog
            {
                Title = "Open ",
                Filter = "Json file|*.json",
                InitialDirectory = @"C:\"
            };

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                LocationLayoutManager.LoadFile(theDialog.FileName);
            }
        }
    }
}
