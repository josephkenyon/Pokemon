using Library.Domain;
using Library.GameState.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using Library.GameState.BagState;
using Library.Content;
using Library.Controls;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class PokemonSelectHelper
    {
        public static void Update()
        {
            Direction? direction = GamePadHelper.GetDPadDirection();

            int unselectedIndex = 0;
            BattlePokemon currentlySelectedPokemon = BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemon;
            if (currentlySelectedPokemon.UsedMove || currentlySelectedPokemon.IsFainted) {
                foreach (BattlePokemon pokemon in BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon) {
                    if (!pokemon.UsedMove && !pokemon.IsFainted) {
                        BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemonIndex = unselectedIndex;
                        return;
                    }
                    unselectedIndex++;
                }
            }

            if (direction != null)
            {
                int? index = GetIndexOf((Direction)direction, BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon, BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemonIndex, true);

                if (index != null)
                {
                    BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemonIndex = (int)index;
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                    BattleStateManager.Battle.ResetFadeTimer();
                }
            }

            if (ControlsManager.APressed())
            {
                if (BattleStateManager.Battle.GetPreviousState() == BattleState.ItemSelect)
                {
                    ItemName? itemType = BagStateManager.UseSelectedItem();
                    if (itemType != null)
                    {
                        int? healAmount = ItemManager.GetHealAmount((ItemName)itemType);
                        if (healAmount != null)
                        {
                            BattleStateManager.Battle.HealSelectedPokemon(Direction.Left, (int)healAmount);
                        }
                    }
                }
                else
                {
                    MoveSelectHelper.SelectedIndex = 0;
                    BattleStateManager.Battle.SwitchToState(BattleState.MoveSelect);
                }
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
            }
        }

        public static int? GetIndexOf(Direction direction, List<BattlePokemon> pokemon, int index, bool selectingPokemon)
        {
            Vector selectedLocation = pokemon[index].Position;

            Func<BattlePokemon, bool> predicate = null;
            Vector vector = null;

            switch (direction)
            {
                case Direction.Left:
                    predicate = new Func<BattlePokemon, bool>(v => v.Position.X < selectedLocation.X && v.Position.Y == selectedLocation.Y && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderByDescending(v => v.Position.X).First().Position;
                        break;
                    }

                    predicate = new Func<BattlePokemon, bool>(v => v.Position.X < selectedLocation.X && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderByDescending(v => v.Position.X).First().Position;
                        break;
                    }
                    return null;
                case Direction.Right:
                    predicate = new Func<BattlePokemon, bool>(v => v.Position.X > selectedLocation.X && v.Position.Y == selectedLocation.Y && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderBy(v => v.Position.X).First().Position;
                        break;
                    }

                    predicate = new Func<BattlePokemon, bool>(v => v.Position.X > selectedLocation.X && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderBy(v => v.Position.X).First().Position;
                        break;
                    }

                    return null;
                case Direction.Up:
                    predicate = new Func<BattlePokemon, bool>(v => v.Position.Y < selectedLocation.Y && v.Position.X == selectedLocation.X && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderByDescending(v => v.Position.Y).First().Position;
                        break;
                    }

                    predicate = new Func<BattlePokemon, bool>(v => v.Position.Y < selectedLocation.Y && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderByDescending(v => v.Position.Y).First().Position;
                        break;
                    }

                    return null;
                case Direction.Down:
                    predicate = new Func<BattlePokemon, bool>(v => v.Position.Y > selectedLocation.Y && v.Position.X == selectedLocation.X && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderBy(v => v.Position.Y).First().Position;
                        break;
                    }

                    predicate = new Func<BattlePokemon, bool>(v => v.Position.Y > selectedLocation.Y && (selectingPokemon ? !v.UsedMove : !v.IsFainted));
                    if (pokemon.Where(predicate).Count() > 0)
                    {
                        vector = pokemon.Where(predicate).OrderBy(v => v.Position.Y).First().Position;
                        break;
                    }

                    return null;
            }

            return pokemon.FindIndex(0, pokemon.Count, v => v.Position == vector);
        }
    }
}