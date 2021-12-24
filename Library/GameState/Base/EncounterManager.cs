using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.GameState.Battle;
using Library.World;
using Library.World.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.GameState.Base
{
    public static class EncounterManager
    {
        private static int LastEncounterCounter { get; set; }

        public static bool EncounterPokemon()
        {
            if (GameStateManager.Instance.GetPlayer().CharacterState.Pokemon.Count == 0)
            {
                return false;
            }

            LocationLayout locationLayout = LocationManager.LocationLayouts[BaseStateManager.Instance.GetPlayerLocation()];

            if (locationLayout.ForegroundGrassTiles.Values.Any(tile => CollisionHandler.AreColliding(GameStateManager.Instance.GetPlayer(), tile)))
            {
                Random rnd = new Random();
                int number = rnd.Next(1, (int)(Constants.DefaultWildBattleChance * 100));
                if (LastEncounterCounter > Constants.DefaultWildBattleChance * 50 && number == 1)
                {
                    Player player = (Player)GameStateManager.Instance.GetCharacter(CharacterName.Ash);
                    List<Pokemon> list = new List<Pokemon>();

                    for (int i = 0; i < player.CharacterState.Pokemon.Count - 1; i++)
                    {
                        LastEncounterCounter = 0;
                        Species? species = null;
                        int level = 0;
                        while (species == null)
                        {
                            foreach (LocationPokemonJson pokemonJson in locationLayout.LocationPokemonJson)
                            {
                                number = rnd.Next(1, (int)(pokemonJson.EncounterRate * 100));
                                if (number == 1)
                                {
                                    species = pokemonJson.Species;

                                    level = rnd.Next(pokemonJson.LowerLevelBound, pokemonJson.UpperLevelBound);
                                    break;
                                }
                            }
                        }

                        list.Add(new Pokemon((Species)species, level));
                    }

                    BattleStateManager.CreateNewBattle(player.CharacterState, new CharacterState(null) { Pokemon = list });
                    return true;
                }
                else
                {
                    LastEncounterCounter += 1;
                }
            }

            return false;
        }
    }
}
