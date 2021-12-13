using Library.Domain;
using Library.GameState.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Library.Cutscenes
{
    public class Cutscene
    {
        public bool Repeating { get; set; }
        public LocationName LocationName { get; set; }
        public List<Point> TriggerPoints { get; set; }
        public List<ICutsceneTransaction> CutsceneTransactions { get; set; }
        public Flag? Flag { get; set; }

        public Cutscene()
        {
            CutsceneTransactions = new List<ICutsceneTransaction>();
            TriggerPoints = new List<Point>();
        }
    }
}
