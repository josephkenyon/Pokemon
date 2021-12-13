using Library.Domain;
using Library.GameState;
using Library.GameState.Base;

namespace Library.Cutscenes
{
    public class MovementCutsceneTransaction : ICutsceneTransaction
    {
        public MovementTransaction MovementTransaction { get; private set; }
        public bool InProgress { get; private set; }
        public CutsceneTransactionType GetCutsceneTransactionType() => CutsceneTransactionType.Movement;

        public MovementCutsceneTransaction(MovementTransaction movementTransaction)
        {
            MovementTransaction = new MovementTransaction(movementTransaction);
            
            for (int i = 0; i < MovementTransaction.DistancesFromFlags.Count; i++)
            {
                MovementTransaction.Distances.Add(BaseStateManager.Instance.FlagManager.GetFlagNumericValue(MovementTransaction.DistancesFromFlags[i]));
            }
        }

        public void Begin()
        {
            for (int i = 0; i < MovementTransaction.CharacterNames.Count; i++)
            {
                if (MovementTransaction.Distances[i] > 0)
                {
                    GameStateManager.Instance.GetCharacter(MovementTransaction.CharacterNames[i]).CharacterState.StartMoving(MovementTransaction.Directions[i]);
                }
            }

            InProgress = true;
        }
    }
}
