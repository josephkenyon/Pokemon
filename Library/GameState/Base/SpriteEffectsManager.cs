using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.GameState.Battle;
using Library.World.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.GameState.Base
{
    public static class SpriteEffectsManager
    {
        public static void CharacterMovementCompleted(Character character)
        {
            LocationName currentLocation = BaseStateManager.Instance.GetPlayerLocation();
            if (LocationManager.LocationLayouts[currentLocation].BackgroundGrassTiles.Values.Any(tile => CollisionHandler.AreColliding(character, tile))) {
                Random rnd = new Random();
                int number = rnd.Next(1, (int) (Constants.DefaultWildBattleChance * 100));
                if (number == 1)
                {
                    Species? species = null;
                    int level = 0;
                    while (species == null)
                    {
                        foreach (LocationPokemonJson pokemonJson in LocationManager.LocationLayouts[currentLocation].LocationPokemonJson)
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

                    List<Pokemon> list = new List<Pokemon>() {
                        new Pokemon((Species) species, level),
                    };

                    Player player = (Player)GameStateManager.Instance.GetCharacter(CharacterName.Ash);
                    BattleStateManager.CreateNewBattle(player.CharacterState, new CharacterState(null) { Pokemon = list });
                }
                else
                {
                    BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()].SpriteEffects.Add(new SpriteEffect
                    {
                        TextureName = TextureName.Animation,
                        SpritePosition = new Vector(0),
                        Position = character.GetPosition() / Constants.ScaledTileSize + new Vector(0, 1),
                        Size = new Vector(1),
                        NumFrames = 3
                    });
                }
            }
        }
    }
}
