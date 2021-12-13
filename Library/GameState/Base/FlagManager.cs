using System;
using System.Collections.Generic;
using System.Linq;
using Library.Cutscenes;
using Library.Domain;

namespace Library.GameState.Base
{
    public class FlagManager
    {
        public Dictionary<Flag, bool> Flags { get; private set; }
        public Dictionary<Flag, int> NumericValues { get; private set; }
        public bool GetFlagValue(Flag flag) => Flags[flag];
        public int GetFlagNumericValue(Flag flag) => NumericValues[flag];

        public FlagManager()
        {
            NumericValues = new Dictionary<Flag, int>();

            Flags = new Dictionary<Flag, bool>();
            foreach (Flag flag in Enum.GetValues(typeof(Flag)))
            {
                Flags.Add(flag, false);
            }
        }

        public void SetFlagValue(Flag flag, bool value)
        {
            Flags[flag] = value;

            LocationState locationState = BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()];

            CutsceneJson cutscene = locationState.Cutscenes.Where(cutscene => cutscene.TriggerType == CutsceneTriggerType.Flag && cutscene.IsValid()).FirstOrDefault();
            if (cutscene != null)
            {
                CutsceneManager.BeginCutscene(cutscene);
                return;
            }
        }

        public void SetFlagNumericValue(Flag flag, int value)
        {
            if (NumericValues.ContainsKey(flag))
            {
                NumericValues[flag] = value;
            }
            else
            {
                NumericValues.Add(flag, value);
            }
        }
    }
}
