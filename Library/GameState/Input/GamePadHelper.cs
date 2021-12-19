using Library.Controls;
using Library.Domain;

namespace Library.GameState.Input
{
    public static class GamePadHelper
    {
        public static Direction? GetDPadDirection()
        {

            if (ControlsManager.UpPressed())
            {
                return Direction.Up;
            }
            else if (ControlsManager.DownPressed())
            {
                return Direction.Down;
            }
            else if (ControlsManager.LeftPressed())
            {
                return Direction.Left;
            }
            else if (ControlsManager.RightPressed())
            {
                return Direction.Right;
            }

            return null;
        }
    }
}
