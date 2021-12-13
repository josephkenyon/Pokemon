using Library.Domain;
using Library.GameState.Base.TransitionState;
using System.Collections.Generic;

namespace Library.Cutscenes
{
    public class CutsceneTransactionJson
    {
        public CutsceneTransactionType CutsceneTransactionType { get; set; }
        public List<string> Messages { get; set; }
        public List<TeleportTransaction> TeleportTransactions { get; set; }
        public MovementTransaction MovementTransaction { get; set; }
        public Flag? Flag { get; set; }
        public SpecialActionKey? SpecialActionKey { get; set; }
    }
}
