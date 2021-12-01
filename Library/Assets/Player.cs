using System.Collections.Generic;
using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.Assets
{
    public class Player : Character
    {
        public Pokedex Pokedex { get; set; }
        public List<Pokemon> PokemonStorage { get; set; }

        public Player() {
            Pokedex = new Pokedex();
        }

        public Rectangle GetTargetRectangle() => new Rectangle(
            location: (CharacterState.Position * Constants.ScaledTileSize).ToPoint(),
            size: (SpriteSize * Constants.ScaledTileSize).ToPoint()
        );
    }
}
