using Library.Domain;
using Library.GameState.Base.TransitionState;
using System.Collections.Generic;

namespace Library.Cutscenes
{
    public class TeleportCutsceneTransaction : ICutsceneTransaction
    {
        public List<TeleportTransaction> TeleportTransactions { get; private set; }
        public CutsceneTransactionType GetCutsceneTransactionType() => CutsceneTransactionType.Teleport;

        public TeleportCutsceneTransaction(List<TeleportTransaction> teleportTransactions)
        {
            TeleportTransactions = teleportTransactions;
        }
    }
}
