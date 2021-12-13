using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World.Json
{
    public class CapturedPokemonJson
    {
        public Species Species { get; set; }
        public int Level { get; set; }
        public Point Position { get; set; }
    }
}
