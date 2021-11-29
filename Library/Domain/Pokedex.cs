using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Pokedex
    {
        public Dictionary<Species, PokedexStatus> PokedexStatus { get; private set; }

        public Pokedex() {
            PokedexStatus = new Dictionary<Species, PokedexStatus>();
            foreach (Species species in Enum.GetValues(typeof(Species)))
            {
                PokedexStatus.Add(species, Domain.PokedexStatus.Unknown);
            }
        }

        public void UpdateEntry(Species species, PokedexStatus pokedexStatus) {
            PokedexStatus[species] = pokedexStatus;
        }
    }
}
