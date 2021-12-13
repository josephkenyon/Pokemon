using Library.Domain;

namespace Library.World.Json
{
    public class LocationPokemonJson
    {
        public Species Species { get; set; }
        public float EncounterRate { get; set; }
        public int LowerLevelBound { get; set; }
        public int UpperLevelBound { get; set; }
    }
}