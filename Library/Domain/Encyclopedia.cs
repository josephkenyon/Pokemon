using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Content;
using Library.Content;

namespace Library.Domain
{
    public static class Encyclopedia
    {
        private static Dictionary<Species, SpeciesProperties> speciesProperties;
        public static void Load(ContentManager contentManager)
        {
            speciesProperties = new Dictionary<Species, SpeciesProperties>();

            using (StreamReader r = new StreamReader(contentManager.RootDirectory + "\\encyclopedia\\pokemonStats.json"))
            {
                foreach (SpeciesProperties prop in JsonConvert.DeserializeObject<List<SpeciesProperties>>(r.ReadToEnd(), FileHelper.JsonSerializerSettings)) {
                    Species species = (Species)Enum.Parse(typeof(Species), prop.Name, true);

                    speciesProperties.Add(species, prop);
                }
            }
        }

        public static SpeciesProperties GetSpeciesProperties(Species species) => speciesProperties[species];
    }

    public class SpeciesProperties
    {
        public string Name { get; set; }
        public StatSet StatSet { get; set; }
        public int EV { get; set; }
        public int CatchRate { get; set; }
        public List<Type> Types { get; set; }
        public List<MoveLearn> Moves { get; set; }
    }

    public class MoveLearn { 
        public MoveName MoveName { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
    }
}
