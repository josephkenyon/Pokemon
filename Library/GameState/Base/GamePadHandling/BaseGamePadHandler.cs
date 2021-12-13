using Library.Assets;
using Library.Domain;
using Library.GameState.Battle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Library.GameState.Base.GamePadHandling
{
    public static class BaseGamePadHandler
    {
        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //string a = JsonConvert.SerializeObject(GameStateManager.Instance);

                //List<Pokemon> list = new List<Pokemon>() {
                //    new Pokemon(Species.Ekans, 5),
                //    new Pokemon(Species.Caterpie, 5),
                //    new Pokemon(Species.Butterfree, 5),
                //    new Pokemon(Species.Weedle, 5),
                //    new Pokemon(Species.Fearow, 25)
                //};

                //List<Pokemon> list2 = new List<Pokemon>() {
                //    new Pokemon(Species.Charmander, 10),
                //    new Pokemon(Species.Bulbasaur, 9),
                //    new Pokemon(Species.Squirtle, 5),
                //    //new Pokemon(Species.Venusaur, 60),
                //    //new Pokemon(Species.Pidgey, 5),
                //    //new Pokemon(Species.Pikachu, 5)
                //};

                //list2[0].Moves = new List<Move> { MoveManager.Moves[MoveName.Scratch].GetCopy(), MoveManager.Moves[MoveName.Ember].GetCopy() };

                //((BattleStateManager)GameStateManager.Instance.StateManagers[UIState.Battle]).CreateNewBattle(new CharacterState(null) { Pokemon = list2 }, new CharacterState(null) { Pokemon = list });
                //GameStateManager.Instance.UIStateStack.Push(UIState.Battle);
            }

            BaseButtonsHandler.Update(gamePadState);

            BaseMovementHandler.Update(gamePadState);
        }
    }
}
