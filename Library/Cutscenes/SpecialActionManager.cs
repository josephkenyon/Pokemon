using Library.Assets;
using Library.Domain;
using Library.GameState;
using Library.GameState.Base;
using Library.GameState.Battle;
using Library.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Cutscenes
{
    public class SpecialActionManager
    {
        public static Dictionary<SpecialActionKey, Action> SpecialActions { get; private set; }

        public static void Initialize()
        {
            SpecialActions = new Dictionary<SpecialActionKey, Action>
            {
                { SpecialActionKey.Give_Rival_Pokemon, GivePokemonToRival },
                { SpecialActionKey.Battle_Rival, BattleRival },
                { SpecialActionKey.Put_Missing_Pokemon_Back, PutMissingPokemonBack }
            };
        }

        private static void GivePokemonToRival()
        {
            Species species = BaseStateManager.Instance.FlagManager.GetFlagNumericValue(Flag.Pokemon_Choice) switch
            {
                5 => Species.Charmander,
                3 => Species.Bulbasaur,
                _ => Species.Squirtle,
            };

            LocationState locationState = BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()];
            CapturedPokemon capturedPokemon = locationState.CapturedPokemon.FirstOrDefault(capturedPokemon => capturedPokemon.Pokemon.Species == species);
            if (capturedPokemon != null)
            {
                locationState.CapturedPokemon.Remove(capturedPokemon);
                CharacterState characterState = locationState.Characters.Find(character => character.Name == CharacterName.Green).CharacterState;
                characterState.Pokemon.Add(capturedPokemon.Pokemon);
            }
        }

        private static void PutMissingPokemonBack()
        {
            Species species = BaseStateManager.Instance.FlagManager.GetFlagNumericValue(Flag.Pokemon_Choice) switch
            {
                5 => Species.Charmander,
                3 => Species.Bulbasaur,
                _ => Species.Squirtle,
            };

            LocationState locationState = BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()];
            CapturedPokemon capturedPokemon = new CapturedPokemon(species, 5, new Point((BaseStateManager.Instance.FlagManager.GetFlagNumericValue(Flag.Pokemon_Choice) - 3), 0));
            locationState.CapturedPokemon.Add(capturedPokemon);
        }

        private static void BattleRival()
        {
            Player player = (Player) GameStateManager.Instance.GetCharacter(CharacterName.Ash);
            Character rival = GameStateManager.Instance.GetCharacter(CharacterName.Green);

            BattleStateManager.CreateNewBattle(player.CharacterState, rival.CharacterState);
        }
    }
}