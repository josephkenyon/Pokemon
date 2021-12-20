using Library.Controls;
using Library.Domain;

namespace Library.GameState.Base.MessageState
{
    public static class MessageStateGamePadHandler
    {
        public static void Update()
        {
            if (ControlsManager.ControlPressed(Control.A))
            {
                MessageStateManager.CompleteMessage();
                return;
            }
        }
    }
}
