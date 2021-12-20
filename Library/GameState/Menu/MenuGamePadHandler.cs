using Library.Content;
using Library.Controls;
using Library.Domain;
using Library.GameState.Input;

namespace Library.GameState.Menu
{
    public static class MenuGamePadHandler
    {
        public static bool Update()
        {
            if (ControlsManager.ControlPressed(Control.Start) || (!MenuStateManager.SavingOrLoading && ControlsManager.ControlPressed(Control.B)))
            {
                MenuStateManager.Instance.CloseMenu();
                return true;
            }

            Direction? direction = GamePadHelper.GetPressedDPadButton();

            if (direction != null)
            {
                if (direction == Direction.Left || direction == Direction.Right)
                    return true;

                MenuStateManager.Instance.ChangeSelectedItem((Direction)direction);
                return true;
            }

            if (ControlsManager.ControlPressed(Control.B))
            {
                if (MenuStateManager.Instance.Saving || MenuStateManager.Instance.Loading)
                {
                    MenuStateManager.Instance.CloseSaveLoadMenu();
                }
                else
                {
                    MenuStateManager.Instance.CloseMenu();
                    return true;
                }
            }

            if (ControlsManager.ControlPressed(Control.A))
            {
                if (MenuStateManager.Instance.Saving)
                {
                    FileHelper.SaveState(MenuStateManager.Instance.SaveLoadSelectedIndex);
                    MenuStateManager.Instance.CloseMenu();
                }
                else if (MenuStateManager.Instance.Loading)
                {
                    GameStateManager gameStateManager = FileHelper.LoadState(MenuStateManager.Instance.SaveLoadSelectedIndex);
                    if (gameStateManager != null)
                    {
                        GameStateManager.Instance = gameStateManager;
                        MenuStateManager.Instance.CloseMenu();
                    }
                }
                else
                {
                    switch ((MenuItem)MenuStateManager.Instance.SelectedIndex)
                    {
                        case MenuItem.Return:
                            MenuStateManager.Instance.SelectedIndex = 0;
                            GameStateManager.Instance.UIStateStack.Pop();
                            break;
                        case MenuItem.Bag:
                            GameStateManager.Instance.UIStateStack.Push(UIState.Bag);
                            break;
                        case MenuItem.Save:
                            MenuStateManager.Instance.Saving = true;
                            break;
                        case MenuItem.Load:
                            MenuStateManager.Instance.Loading = true;
                            break;
                        case MenuItem.ExitGame:
                            return false;
                    }
                }

                return true;
            }

            return true;
        }
    }
}