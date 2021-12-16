using Library.Domain;
using Library.Graphics;
using static Library.Graphics.GraphicsHelper;

namespace Library.GameState.BagState
{
    public static class BagStateGraphicsHelper
    {
        public static readonly int CursorXOffset = (int)(12 * Constants.Scaler);
        public static readonly int CursorYOffset = (int)(1 * Constants.Scaler);
        public static readonly int ItemTextXOffset = (int)(156 * Constants.Scaler);
        public static readonly int ItemYOffset = (int)(18 * Constants.Scaler);

        private static readonly Vector FirstPosition = new Vector(158, 14) * Constants.Scaler;
        private static readonly Vector TitlePositionMiddle = new Vector(99, 12) * Constants.Scaler;

        public static Vector GetItemTitlePosition(int index) => new Vector(FirstPosition.X, FirstPosition.Y + ItemYOffset * index);
        public static Vector GetItemCountPosition(int index) => new Vector(FirstPosition.X + ItemTextXOffset, FirstPosition.Y + ItemYOffset * index);
        public static Vector GetCursorPosition(int index) => new Vector(FirstPosition.X - CursorXOffset, FirstPosition.Y + ItemYOffset * index - CursorYOffset);

        public static IDrawingString GetTitleDrawingObject()
            => new CenteredStringObject { Position = TitlePositionMiddle, String = GetFormattedString(BagStateManager.BagState.ToString()) };
    }
}
