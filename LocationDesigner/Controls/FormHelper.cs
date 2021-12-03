using LevelDesigner.Controls;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LocationDesigner.Controls
{
    public static class FormHelper
    {
        public static CheckBox SignCheckBox { get; set; }
        public static CheckBox SuperForegroundCheckbox { get; set; }
        public static CheckBox PortalCheckbox { get; set; }
        public static Label CoordinatesLabel { get; set; }
        public static List<BackgroundTileBox> BackgroundTiles { get; set; }
        public static List<ForegroundTileBox> ForegroundTiles { get; set; }
        public static List<DoodadTileBox> DoodadTiles { get; set; }

        public static void Initialize() {
            BackgroundTiles = new List<BackgroundTileBox>();
            ForegroundTiles = new List<ForegroundTileBox>();
            DoodadTiles = new List<DoodadTileBox>();
        }

    }
}
