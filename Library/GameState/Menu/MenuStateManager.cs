using Library.Domain;
using Library.Graphics;
using Library.Menu;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Library.GameState.Menu
{
    public class MenuStateManager : IStateManager
    {
        public static MenuStateManager Instance { get; private set; }
        public int SelectedIndex { get; set; }
        public int SaveLoadSelectedIndex { get; set; }
        public bool Saving { get; set; }
        public bool Loading { get; set; }
        public static bool SavingOrLoading => Instance.Saving || Instance.Loading;

        public MenuStateManager()
        {
            Instance = this;
        }

        public void ChangeSelectedItem(Direction direction)
        {
            if (direction == Direction.Up)
            {
                if (Saving || Loading)
                {
                    SaveLoadSelectedIndex = SaveLoadSelectedIndex == 0 ? Enum.GetValues(typeof(MenuItem)).Length - 1 : SaveLoadSelectedIndex - 1;
                }
                else
                {
                    SelectedIndex = SelectedIndex == 0 ? Enum.GetValues(typeof(MenuItem)).Length - 1 : SelectedIndex - 1;
                }
            }
            else if (direction == Direction.Down)
            {
                if (Saving || Loading)
                {
                    SaveLoadSelectedIndex++;
                    SaveLoadSelectedIndex = SaveLoadSelectedIndex == Enum.GetValues(typeof(MenuItem)).Length ? 0 : SaveLoadSelectedIndex;
                }
                else
                {
                    SelectedIndex++;
                    SelectedIndex = SelectedIndex == Enum.GetValues(typeof(MenuItem)).Length ? 0 : SelectedIndex;
                }
            }
            GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
        }

        public bool Update()
        {
            bool result = MenuGamePadHandler.Update();
            return result;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            GameStateManager.Instance.StateManagers[UIState.Base].Draw(spriteBatch);

            MenuDrawingManager.DrawMenuItem(spriteBatch, MenuGraphicsHelper.MenuSourceRectangle, MenuGraphicsHelper.MenuTargetRectangle);
            MenuDrawingManager.DrawMenuItem(spriteBatch, MenuGraphicsHelper.PointerSourceRectangle, MenuGraphicsHelper.PointerTargetRectangle);

            if (Saving || Loading)
            {
                MenuDrawingManager.DrawMenuItem(spriteBatch, MenuGraphicsHelper.SaveLoadMenuSourceRectangle, MenuGraphicsHelper.SaveLoadMenuTargetRectangle);
                MenuDrawingManager.DrawMenuItem(spriteBatch, MenuGraphicsHelper.PointerSourceRectangle, MenuGraphicsHelper.SaveLoadPointerTargetRectangle);
            }
        }

        public void CloseSaveLoadMenu()
        {
            Instance.Saving = false;
            Instance.Loading = false;
            GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
        }

        public void CloseMenu()
        {
            Instance.Saving = false;
            Instance.Loading = false;
            Instance.SelectedIndex = 0;
            Instance.SaveLoadSelectedIndex = 0;
            GameStateManager.Instance.UIState = UIState.Base;
            GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
        }
    }
}