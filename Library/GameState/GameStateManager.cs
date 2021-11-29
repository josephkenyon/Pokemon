using System.Linq;
using System.Collections.Generic;
using Library.Assets;
using Library.Domain;
using Library.GameState.Base;
using Library.GameState.Battle;
using Library.GameState.Menu;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Library.GameState
{
    public class GameStateManager
    {
        public Dictionary<UIState, IStateManager> StateManagers { get; set; }
        public UIState UIState { get; set; }
        public Vector CameraLocation { get; set; }
        public int InputDebounceTimer { get; set; }
        public static GameStateManager Instance { get; set; }

        public static void Initialize()
        {
            Instance = new GameStateManager
            {
                CameraLocation = new Vector(Constants.ResolutionWidth / 2, Constants.ResolutionHeight / 2),
                UIState = UIState.Base,
                StateManagers = new Dictionary<UIState, IStateManager> {
                    { UIState.Base, new BaseStateManager() },
                    { UIState.Battle, new BattleStateManager() },
                    { UIState.Menu, new MenuStateManager() },
                }
            };
        }

        public bool Update()
        {
            UpdateTimers();

            bool result = StateManagers[UIState].Update();

            CameraLocation = GetPlayer().CharacterState.Position + (new Vector(GetPlayer().GetTargetRectangle().Size) / 2);

            return result;
        }

        public void Draw(SpriteBatch spriteBatch) => StateManagers[UIState].Draw(spriteBatch);

        public Player GetPlayer()
        {
            BaseStateManager baseStateManager = (BaseStateManager)StateManagers[UIState.Base];

            return (Player)baseStateManager.LocationStates[baseStateManager.GetPlayerLocation()].Characters.Find(character => character.Name == Constants.PlayerName);
        }

        public void UpdateTimers() {
            if (InputDebounceTimer != 0)
            {
                InputDebounceTimer--;
            }
        }
    }
}
