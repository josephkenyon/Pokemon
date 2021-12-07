using Library.Content;
using Library.Domain;
using Library.GameState.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Library.GameState.Menu
{
    public static class MenuGamePadHandler
    {
        public static bool Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (gamePadState.Buttons.Start == ButtonState.Pressed || (gamePadState.Buttons.B == ButtonState.Pressed && !MenuStateManager.SavingOrLoading))
                {
                    MenuStateManager.Instance.CloseMenu();
                    return true;
                }

                Direction? direction = GamePadHelper.GetDPadDirection(gamePadState);

                if (direction != null)
                {
                    if (direction == Direction.Left || direction == Direction.Right)
                        return true;

                    MenuStateManager.Instance.ChangeSelectedItem((Direction)direction);
                    return true;
                }

                if (gamePadState.Buttons.B == ButtonState.Pressed)
                {
                    if (MenuStateManager.Instance.Saving || MenuStateManager.Instance.Loading)
                    {
                        MenuStateManager.Instance.CloseSaveLoadMenu();
                    }
                    else {
                        MenuStateManager.Instance.CloseMenu();
                        return true;
                    }
                }

                if (gamePadState.Buttons.A == ButtonState.Pressed)
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
                                GameStateManager.Instance.UIState = UIState.Base;
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
                    GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                    return true;
                }
            }
            return true;
        }
    }
}