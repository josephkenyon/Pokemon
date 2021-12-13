using Library.Domain;

namespace Library.Cutscenes
{
    public class FlagCutsceneTransaction : ICutsceneTransaction
    {
        public Flag Flag { get; set; }
        public CutsceneTransactionType GetCutsceneTransactionType() => CutsceneTransactionType.Flag;

        public FlagCutsceneTransaction(Flag flag)
        {
            Flag = flag;
        }
    }
}
