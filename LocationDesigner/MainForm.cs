using LevelDesigner.Controls;
using LocationDesigner.Controls;
using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using LocationDesigner.World;
using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace LocationDesigner
{
    public partial class MainForm : Form
    {
        public int SelectedTag { get; set; }

        public MainForm()
        {
            InitializeComponent();

            CacheManager.ReadCacheFile();
            FormHelper.Initialize();

            InitializeTileSets();

            FormHelper.SignCheckBox = SignCheckbox;
            FormHelper.SuperForegroundCheckbox = SuperForegroundCheckbox;
            FormHelper.PortalCheckbox = PortalCheckBox;
            FormHelper.CoordinatesLabel = CoordinatesLabel;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimeTick);
            timer.Interval = 200;
            timer.Enabled = true;

            SelectedTag = -1;
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
                        FormHelper.BackgroundTiles.Add(newTile);
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
                        FormHelper.ForegroundTiles.Add(newTile);
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
                FormHelper.DoodadTiles.Add(grassTile);
                doodadPanel.Controls.Add(grassTile);
                grassTile.Setup(LocationDoodad.Grass);

                DoodadTileBox flowerTile = new DoodadTileBox();
                FormHelper.DoodadTiles.Add(flowerTile);
                doodadPanel.Controls.Add(flowerTile);
                flowerTile.Setup(LocationDoodad.Red_Flower);
            }
        }

        private string GetTileSetDirectory()
        {
            FolderBrowserDialog theDialog = new FolderBrowserDialog();

            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return theDialog.SelectedPath;
                }
                catch (Exception)
                {
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
                RenderPanel.TopLeftPoint = RenderPanel.DefaultSpot;
            }
        }

        public void MainForm_MouseHover(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (int.TryParse((string)panel.Tag, out int j))
            {
                SelectedTag = j;
            }
        }

        public void MainForm_MouseLeave(object sender, EventArgs e)
        {
            SelectedTag = -1;
        }

        private void OnTimeTick(object source, ElapsedEventArgs e)
        {
            if (SelectedTag == 0)
            {
                RenderPanel.TopLeftPoint = new Microsoft.Xna.Framework.Point(RenderPanel.TopLeftPoint.X + 1, RenderPanel.TopLeftPoint.Y);
            }
            else if (SelectedTag == 1)
            {
                RenderPanel.TopLeftPoint = new Microsoft.Xna.Framework.Point(RenderPanel.TopLeftPoint.X - 1, RenderPanel.TopLeftPoint.Y);
            }
            else if (SelectedTag == 2)
            {
                RenderPanel.TopLeftPoint = new Microsoft.Xna.Framework.Point(RenderPanel.TopLeftPoint.X, RenderPanel.TopLeftPoint.Y + 1);
            }
            else if (SelectedTag == 3)
            {
                RenderPanel.TopLeftPoint = new Microsoft.Xna.Framework.Point(RenderPanel.TopLeftPoint.X, RenderPanel.TopLeftPoint.Y - 1);
            }
        }
    }
}
