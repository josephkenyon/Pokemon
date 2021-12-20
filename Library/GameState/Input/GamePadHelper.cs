using Library.Controls;
using Library.Domain;

namespace Library.GameState.Input
{
    public static class GamePadHelper
    {
        public static Direction? GetPressedDPadButton()
        {

            if (ControlsManager.ControlPressed(Control.Up))
            {
                return Direction.Up;
            }
            else if (ControlsManager.ControlPressed(Control.Down))
            {
                return Direction.Down;
            }
            else if (ControlsManager.ControlPressed(Control.Left))
            {
                return Direction.Left;
            }
            else if (ControlsManager.ControlPressed(Control.Right))
            {
                return Direction.Right;
            }

            return null;
        }

        public static Direction? GetHeldDPadButton()
        {

            if (ControlsManager.ControlHeld(Control.Up))
            {
                return Direction.Up;
            }
            else if (ControlsManager.ControlHeld(Control.Down))
            {
                return Direction.Down;
            }
            else if (ControlsManager.ControlHeld(Control.Left))
            {
                return Direction.Left;
            }
            else if (ControlsManager.ControlHeld(Control.Right))
            {
                return Direction.Right;
            }

            return null;
        }
    }
}
