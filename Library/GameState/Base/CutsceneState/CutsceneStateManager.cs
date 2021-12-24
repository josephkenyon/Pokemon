using System.Collections.Generic;
using System.Linq;
using Library.Cutscenes;
using Library.Domain;
using Library.GameState.Base.MessageState;
using Library.GameState.Base.TransitionState;

namespace Library.GameState.Base.CutsceneState
{
    public class CutsceneStateManager
    {
        public static void Update()
        {
            if (CutsceneManager.ActiveCutscene.CutsceneTransactions.Count == 0)
            {
                CutsceneManager.EndCutscene();
                return;
            }

            ICutsceneTransaction cutsceneTransaction = CutsceneManager.ActiveCutscene.CutsceneTransactions.FirstOrDefault();
            if (cutsceneTransaction is MessageCutsceneTransaction messageCutSceneTransaction)
            {
                List<Message> messages = new List<Message>();

                messageCutSceneTransaction.Messages.ForEach(message => messages.Add(new Message(message)));

                MessageStateManager.EnterMessageState(messages);

                CutsceneManager.ActiveCutscene.CutsceneTransactions.Remove(cutsceneTransaction);
            }
            else if (cutsceneTransaction is TeleportCutsceneTransaction teleportCutsceneTransaction)
            {
                TransitionStateManager.StartTransitionList(teleportCutsceneTransaction.TeleportTransactions);

                CutsceneManager.ActiveCutscene.CutsceneTransactions.Remove(cutsceneTransaction);
            }
            else if (cutsceneTransaction is MovementCutsceneTransaction movementCutsceneTransaction)
            {
                if (!movementCutsceneTransaction.InProgress)
                {
                    movementCutsceneTransaction.Begin();
                }
                else
                {
                    bool complete = true;
                    movementCutsceneTransaction.MovementTransaction.Distances.ForEach(distance =>
                    {
                        if (distance != 0)
                        {
                            complete = false;
                        }
                    });

                    if (complete)
                    {
                        CutsceneManager.ActiveCutscene.CutsceneTransactions.Remove(movementCutsceneTransaction);
                    }
                }
            }
            else if (cutsceneTransaction is FlagCutsceneTransaction flagCutsceneTransaction)
            {
                BaseStateManager.Instance.FlagManager.SetFlagValue(flagCutsceneTransaction.Flag, true);
                CutsceneManager.ActiveCutscene.CutsceneTransactions.Remove(cutsceneTransaction);
            }
            else if (cutsceneTransaction is SpecialActionCutsceneTransaction specialActionCutsceneTransaction)
            {
                SpecialActionManager.SpecialActions[specialActionCutsceneTransaction.SpecialActionKey].Invoke();
                CutsceneManager.ActiveCutscene.CutsceneTransactions.Remove(cutsceneTransaction);
            }
        }

        public static MovementCutsceneTransaction GetActiveMovementCutsceneTransaction()
        {
            if (CutsceneManager.ActiveCutscene == null) {
                return null;
            }

            ICutsceneTransaction cutsceneTransaction = CutsceneManager.ActiveCutscene.CutsceneTransactions.FirstOrDefault();
            if (cutsceneTransaction is MovementCutsceneTransaction movementCutsceneTransaction)
            {
                return movementCutsceneTransaction;
            }

            return null;
        }
    }
}