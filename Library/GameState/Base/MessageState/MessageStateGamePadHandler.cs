using Library.Controls;
using Library.Domain;

namespace Library.GameState.Base.MessageState
{
    public static class MessageStateGamePadHandler
    {
        public static void Update()
        {
            if (ControlsManager.APressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                MessageStateManager.CompleteMessage();
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounceLong;
                return;
            }
        }
    }
}
