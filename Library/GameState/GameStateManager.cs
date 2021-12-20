using System.Linq;
using System.Collections.Generic;
using Library.Assets;
using Library.Domain;
using Library.GameState.Base;
using Library.GameState.Battle;
using Library.GameState.Menu;
using Microsoft.Xna.Framework.Graphics;
using Library.GameState.BagState;

namespace Library.GameState
{
    public class GameStateManager
    {
        public Dictionary<UIState, IStateManager> StateManagers { get; set; }
        public Stack<UIState> UIStateStack { get; set; }
        public Vector CameraLocation { get; set; }
        public static GameStateManager Instance { get; set; }

        public static void Initialize()
        {
            Instance = new GameStateManager
            {
                CameraLocation = new Vector(Constants.ResolutionWidth / 2, Constants.ResolutionHeight / 2),
                UIStateStack = new Stack<UIState>(),
                StateManagers = new Dictionary<UIState, IStateManager> {
                    { UIState.Base, new BaseStateManager() },
                    { UIState.Battle, new BattleStateManager() },
                    { UIState.Menu, new MenuStateManager() },
                    { UIState.Bag, new BagStateManager() },
                }
            };

            Instance.UIStateStack.Push(UIState.Base);
        }

        public bool Update()
        {
            bool result = StateManagers[UIStateStack.Peek()].Update();

            CameraLocation = GetPlayer().CharacterState.Position + (new Vector(GetPlayer().GetTargetRectangle().Size) / 2);

            return result;
        }

        public void Draw(SpriteBatch spriteBatch) => StateManagers[UIStateStack.Peek()].Draw(spriteBatch);

        public Player GetPlayer()
        {
            return (Player) GetCharacter(CharacterName.Ash);
        }

        public Character GetCharacter(CharacterName characterName)
        {
            Character returnCharacter = null;
            BaseStateManager baseStateManager = (BaseStateManager)StateManagers[UIState.Base];

            baseStateManager.LocationStates.Values.ToList().ForEach(locationState =>
            {
                Character character = locationState.Characters.FirstOrDefault(character => characterName == character.Name);
                if (character != null)
                {
                    returnCharacter = character;
                }
            });

            return returnCharacter;
        }
    }
}
