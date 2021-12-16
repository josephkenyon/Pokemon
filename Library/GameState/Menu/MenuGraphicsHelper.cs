using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework;

namespace Library.GameState.Menu
{
    public static class MenuGraphicsHelper
    {
        private static Vector MenuTextureSize => new Vector(5, 9);
        public static Rectangle MenuSourceRectangle => GraphicsHelper.GetSourceRectangle(new Vector(2, 0), MenuTextureSize);
        public static Rectangle MenuTargetRectangle => new Rectangle(Constants.ResolutionWidth - MenuTextureSize.X * Constants.ScaledTileSize, 0, MenuTextureSize.X * Constants.ScaledTileSize, MenuTextureSize.Y * Constants.ScaledTileSize);

        private static Vector SaveLoadMenuTextureSize => new Vector(4, 10);
        public static Rectangle SaveLoadMenuSourceRectangle => GraphicsHelper.GetSourceRectangle(new Vector(7, 0), SaveLoadMenuTextureSize);
        public static Rectangle SaveLoadMenuTargetRectangle => new Rectangle(Constants.ResolutionWidth - (SaveLoadMenuTextureSize.X + MenuTextureSize.X) * Constants.ScaledTileSize, 0, SaveLoadMenuTextureSize.X * Constants.ScaledTileSize, SaveLoadMenuTextureSize.Y * Constants.ScaledTileSize);

        public static Vector PointerTextureSize => new Vector(1, 1);
        private static Vector MenuPointerStartingPosition => new Vector(2 * Constants.Scaler, 11 * Constants.Scaler);
        private static int PointerIncrement => (int)(15 * Constants.Scaler);
        public static Rectangle PointerSourceRectangle => GraphicsHelper.GetSourceRectangle(new Vector(1, 0), Vector.One);
        public static Rectangle PointerTargetRectangle => new Rectangle(new Point(MenuTargetRectangle.Location.X + MenuPointerStartingPosition.X, MenuTargetRectangle.Location.Y + MenuPointerStartingPosition.Y + PointerIncrement * MenuStateManager.Instance.SelectedIndex), (PointerTextureSize * Constants.ScaledTileSize).ToPoint());

        private static Vector SaveLoadMenuPointerStartingPosition => new Vector(2 * Constants.Scaler, 11 * Constants.Scaler);
        public static Rectangle SaveLoadPointerTargetRectangle => new Rectangle(new Point(SaveLoadMenuTargetRectangle.Location.X + SaveLoadMenuPointerStartingPosition.X, SaveLoadMenuTargetRectangle.Location.Y + SaveLoadMenuPointerStartingPosition.Y + PointerIncrement * MenuStateManager.Instance.SaveLoadSelectedIndex), (PointerTextureSize * Constants.ScaledTileSize).ToPoint());
    }
}
