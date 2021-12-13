using Library.Domain;
using Library.GameState.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Library.Cutscenes
{
    public class CutsceneJson
    {
        public CutsceneTriggerType TriggerType { get; set; }
        public bool Repeating { get; set; }
        public LocationName LocationName { get; set; }
        public List<Point> TriggerPoints { get; set; }
        public List<CutsceneTransactionJson> CutsceneTransactions { get; set; }
        public List<Flag> Flags { get; set; }
        public List<bool> FlagValues { get; set; }

        public bool IsValid()
        {
            for (int i = 0; i < Flags.Count; i++)
            {
                if (BaseStateManager.Instance.FlagManager.GetFlagValue(Flags[i]) != FlagValues[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
