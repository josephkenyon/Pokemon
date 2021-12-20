using Library.Domain;
using Library.GameState.Battle;
using Library.GameState.Battle.BattleDrawingObjects;
using Library.GameState.Battle.GamePadHelpers;
using Library.GameState.Menu;
using Library.Graphics;
using Library.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Library.Battle
{
    public static class CatchManager
    {
        public static bool IsCaught(int itemPower, BattlePokemon battlePokemon)
        {
            if (itemPower == 4)
            {
                return true;
            }

            int n = GetPokeBallNumber(itemPower);
            if (n < 25 && PokemonIsFrozenOrAsleep(battlePokemon))
            {
                return true;
            }
            else if (n < 12 && PokemonIsBurnedParalyzedOrPoisoned(battlePokemon))
            {
                return true;
            }

            int a = (int)(battlePokemon.GetStat(Stat.HP) * 255 * 4 / (battlePokemon.CurrentHealth * (itemPower == 2 ? 12 : 8)));

            if (a >= n)
            {
                return true;
            }

            return false;
        }

        private static int GetPokeBallNumber(int itemPower) {
            var upperBound = itemPower switch
            {
                1 => 255,
                2 => 200,
                _ => 150,
            };

            Random rnd = new Random();
            return rnd.Next(0, upperBound);
        }
        private static bool PokemonIsFrozenOrAsleep(BattlePokemon battlePokemon)
        {
            return battlePokemon.StatusEffects.Contains(StatusEffect.Frozen)
                || battlePokemon.StatusEffects.Contains(StatusEffect.Asleep);
        }

        private static bool PokemonIsBurnedParalyzedOrPoisoned(BattlePokemon battlePokemon)
        {
            return battlePokemon.StatusEffects.Contains(StatusEffect.Burned)
                || battlePokemon.StatusEffects.Contains(StatusEffect.Poisoned)
                || battlePokemon.StatusEffects.Contains(StatusEffect.Paralyzed);
        }
    }
}