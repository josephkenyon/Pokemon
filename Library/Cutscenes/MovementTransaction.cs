using Library.Domain;
using System.Collections.Generic;

namespace Library.Cutscenes
{
    public class MovementTransaction
    {
        public List<CharacterName> CharacterNames { get; set; }
        public List<Direction> Directions { get; set; }
        public List<int> Distances { get; set; }
        public List<Flag> DistancesFromFlags { get; set; }

        public MovementTransaction(MovementTransaction movementTransaction)
        {
            if (movementTransaction != null)
            {
                CharacterNames = new List<CharacterName>(movementTransaction.CharacterNames);
                Directions = new List<Direction>(movementTransaction.Directions);

                if (movementTransaction.Distances != null)
                {
                    Distances = new List<int>(movementTransaction.Distances);
                }
                else
                {
                    Distances = new List<int>();
                }

                if (movementTransaction.DistancesFromFlags != null)
                {
                    DistancesFromFlags = new List<Flag>(movementTransaction.DistancesFromFlags);
                }
                else
                {
                    DistancesFromFlags = new List<Flag>();
                }
            }
            else
            {
                DistancesFromFlags = new List<Flag>();
            }
        }
    }
}