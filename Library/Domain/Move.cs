using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Move
    {
        public string Name { get; set; }
        public int PP { get; set; }
        public Type Type { get; set; }
        public int? Power { get; set; }
        public int SecondaryPower { get; set; }
        public int Accuracy { get; set; }
        public int SecondaryAccuracy { get; set; }
        public List<MoveType> MoveTypes { get; set; }
        public Stat? StatInteraction { get; set; }
        public string FormattedName => Name.Replace("_", " ").ToUpper();
        public MoveName MoveName => (MoveName)Enum.Parse(typeof(MoveName), Name, true);

        public Move GetCopy() {
            return new Move
            {
                Name = Name,
                PP = PP,
                Type = Type,
                Power = Power,
                SecondaryPower = SecondaryPower,
                Accuracy = Accuracy,
                SecondaryAccuracy = SecondaryAccuracy,
                MoveTypes = MoveTypes,
                StatInteraction = StatInteraction,
            };
        }
    }
}
