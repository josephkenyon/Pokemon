using Library.Assets;
using Library.Domain;
using System.Collections.Generic;

namespace Library.GameState.Battle
{
    public class BattleCharacterState
    {
        public Bag Bag { get; set; }
        public List<BattlePokemon> Pokemon { get; set; }
        public int SelectedPokemonIndex { get; set; }
        public Direction Direction { get; set; }
        public BattlePokemon GetSelectedPokemon()
        {
            if (SelectedPokemonIndex >= Pokemon.Count)
            {
                SelectedPokemonIndex = 0;
            }

            return Pokemon[SelectedPokemonIndex];
        }

        public BattleCharacterState(CharacterState characterState, Direction direction, int numPokemon) {
            Bag = characterState.Bag;
            Direction = direction;

            Pokemon = new List<BattlePokemon>();
            for (int i = 0; i < numPokemon && i < characterState.Pokemon.Count; i++) {
                Pokemon.Add(new BattlePokemon(Direction, characterState.Pokemon[i], i));
            }

            SelectedPokemonIndex = 0;
        }
    }
}
