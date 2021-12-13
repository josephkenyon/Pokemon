using System.Collections.Generic;
using Library.Domain;
using Library.GameState.Base;
using Library.GameState.Base.MessageState;
using Microsoft.Xna.Framework;

namespace Library.Assets
{
    public class Player : Character
    {
        public Pokedex Pokedex { get; set; }
        public List<Pokemon> PokemonStorage { get; set; }

        public Player()
        {
            PokemonStorage = new List<Pokemon>();
        }

        public Rectangle GetTargetRectangle() => new Rectangle(
            location: (CharacterState.Position * Constants.ScaledTileSize).ToPoint(),
            size: (SpriteSize * Constants.ScaledTileSize).ToPoint()
        );

        public override TextureName GetTextureName() => TextureName.Ash;

        public void GivePokemon(Pokemon pokemon)
        {
            CharacterState.Pokemon.Add(pokemon);

            if (BaseStateManager.Instance.FlagManager.GetFlagValue(Flag.Selected_Pokemon) == false)
            {
                if (pokemon.Species == Species.Bulbasaur)
                {
                    BaseStateManager.Instance.FlagManager.SetFlagNumericValue(Flag.Pokemon_Choice, 5);
                }
                else if (pokemon.Species == Species.Squirtle)
                {
                    BaseStateManager.Instance.FlagManager.SetFlagNumericValue(Flag.Pokemon_Choice, 3);
                }
                else if (pokemon.Species == Species.Charmander)
                {
                    BaseStateManager.Instance.FlagManager.SetFlagNumericValue(Flag.Pokemon_Choice, 4);
                }

                BaseStateManager.Instance.FlagManager.SetFlagValue(Flag.Select_Pokemon, false);
                BaseStateManager.Instance.FlagManager.SetFlagValue(Flag.Selected_Pokemon, true);
            }

            MessageStateManager.EnterMessageState(new List<Message>
            {
                new Message("You recieved a " + pokemon.Species.ToString().ToUpper() + "!")
            });
        }
    }
}