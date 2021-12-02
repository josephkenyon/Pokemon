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

            InitializeTileSets();
        }

        private void SetTileSetDirectory_Click(object sender, EventArgs e)
        {
            CacheManager.SetTileSetBitmapsPath(GetTileSetDirectory());
            InitializeTileSets();
        }

        private void InitializeTileSets()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(CacheManager.BackgroundBitmapPath);
            }
            catch (Exception) { }

            if (bitmap != null)
            {
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

            bitmap = null;
            try
            {
                bitmap = new Bitmap(CacheManager.ForegroundBitmapPath);
            }
            catch (Exception) { }

            if (bitmap != null)
            {
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

            bitmap = null;
            try
            {
                bitmap = new Bitmap(CacheManager.GrassBitmapPath);
            }
            catch (Exception) { }

            if (bitmap != null)
            {
                BitmapManager.SetGrassTileSetBitmap(bitmap);

                DoodadTileBox grassTile = new DoodadTileBox();
                doodadPanel.Controls.Add(grassTile);
                grassTile.Setup(LocationDoodad.Grass);

                DoodadTileBox flowerTile = new DoodadTileBox();
                doodadPanel.Controls.Add(flowerTile);
                flowerTile.Setup(LocationDoodad.Red_Flower);
            }
        }

        private string GetTileSetDirectory() {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return theDialog.SelectedPath;
                }
                catch (Exception) {
                    MessageBox.Show("File could not be opened.");
                }
            }

            return null;
        }

        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void MouseHover(object sender, EventArgs e)
        {
            RenderPanel.TopLeftPoint = new Microsoft.Xna.Framework.Point(RenderPanel.TopLeftPoint.X - 1, RenderPanel.TopLeftPoint.Y);
        }
    }
}
