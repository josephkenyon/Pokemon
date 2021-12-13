using Library.Assets;
using Library.Domain;
using Library.World.Json;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Library.World
{
    public class CapturedPokemon : BaseObject
    {
        [JsonProperty]
        public Pokemon Pokemon { get; set; }

        public CapturedPokemon() {}

        public CapturedPokemon(CapturedPokemonJson json) : this(json.Species, json.Level, json.Position) { }

        public CapturedPokemon(Species species, int level, Point position)
        {
            Pokemon = new Pokemon(species, level);
            TextureName = TextureName.Effects;
            SpritePosition = new Vector(0, 3);
            Position = new Vector(position);
        }
    }
}
