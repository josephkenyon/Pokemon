using Library.Domain;

namespace Library.Cutscenes
{
    public class SpecialActionCutsceneTransaction : ICutsceneTransaction
    {
        public SpecialActionKey SpecialActionKey { get; set; }
        public CutsceneTransactionType GetCutsceneTransactionType() => CutsceneTransactionType.Special_Action;

        public SpecialActionCutsceneTransaction(SpecialActionKey specialActionKey)
        {
            SpecialActionKey = specialActionKey;
        }
    }
}
