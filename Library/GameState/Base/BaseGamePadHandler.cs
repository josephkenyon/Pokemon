using Library.Assets;
using Library.Domain;
using Library.GameState.Battle;
using Library.GameState.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Library.GameState.Base
{
    public static class BaseGamePadHandler
    {
        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //string a = JsonConvert.SerializeObject(GameStateManager.Instance);

                List<Pokemon> list = new List<Pokemon>() {
                    new Pokemon(Species.Ekans, 5),
                    new Pokemon(Species.Caterpie, 5),
                    new Pokemon(Species.Butterfree, 5),
                    new Pokemon(Species.Weedle, 5),
                    new Pokemon(Species.Fearow, 25)
                };

                List<Pokemon> list2 = new List<Pokemon>() {
                    new Pokemon(Species.Charmander, 10),
                    new Pokemon(Species.Bulbasaur, 9),
                    new Pokemon(Species.Squirtle, 5),
                    //new Pokemon(Species.Venusaur, 60),
                    //new Pokemon(Species.Pidgey, 5),
                    //new Pokemon(Species.Pikachu, 5)
                };

                list2[0].Moves = new List<Move> { MoveManager.Moves[MoveName.Scratch].GetCopy(), MoveManager.Moves[MoveName.Ember].GetCopy() };

                ((BattleStateManager)GameStateManager.Instance.StateManagers[UIState.Battle]).CreateNewBattle(new CharacterState { Pokemon = list2 }, new CharacterState { Pokemon = list });
                GameStateManager.Instance.UIState = UIState.Battle;
            }

            if (gamePadState.Buttons.Start == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                GameStateManager.Instance.UIState = UIState.Menu;
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                return;
            }

            Direction? nullableDir = !GameStateManager.Instance.GetPlayer().CharacterState.IsMoving ? GamePadHelper.GetDPadDirection(gamePadState) : null;

            if (nullableDir != null)
            {
                Direction direction = (Direction)nullableDir;
                if (CollisionHandler.IsValidMove(GameStateManager.Instance.GetPlayer(), MovementHandler.GetNewPath(direction, GameStateManager.Instance.GetPlayer().CharacterState.Position)))
                {
                    GameStateManager.Instance.GetPlayer().CharacterState.StartMoving(direction);
                }
                GameStateManager.Instance.GetPlayer().CharacterState.Direction = direction;
            }
        }
    }
}
