using Library.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Library.GameState.BagState;

namespace Library.GameState.Battle
{
    public class Battle
    {
        public Dictionary<Direction, BattleCharacterState> BattleCharacterStates { get; set; }
        public List<BattleStatusEffect> BattleStatusEffects { get; set; }
        public CharacterState LeftCharacterState { get; set; }
        public CharacterState RightCharacterState { get; set; }
        public bool TimerDescending { get; set; }
        public float Timer { get; set; }
        private Stack<BattleState> StateStack { get; set; }
        public BattleState State => StateStack.Peek();
        public string ConsoleText { get; set; }
        public Queue<Transaction> Transactions { get; set; }

        public BattleState? GetPreviousState()
        {
            if (StateStack.Count < 2)
            {
                return null;
            }

            BattleState temp = StateStack.Pop();
            BattleState returnState = StateStack.Peek();
            StateStack.Push(temp);

            return returnState;
        }

        public Battle(CharacterState leftCharacterState, CharacterState rightCharacterState)
        {
            LeftCharacterState = leftCharacterState;
            RightCharacterState = rightCharacterState;

            StateStack = new Stack<BattleState>();
            BattleStatusEffects = new List<BattleStatusEffect>();
            Transactions = new Queue<Transaction>();

            BattleCharacterStates = new Dictionary<Direction, BattleCharacterState> {
                { Direction.Left, new BattleCharacterState(leftCharacterState, Direction.Left, leftCharacterState.Pokemon.Count) },
                { Direction.Right, new BattleCharacterState(rightCharacterState, Direction.Right, rightCharacterState.Pokemon.Count) },
            };

            SwitchToState(BattleState.AshSelect);
            BagStateManager.ItemIndex = -1;
        }

        public void ClearStateStack()
        {
            StateStack.Clear();
            BattleCharacterStates[Direction.Left].SelectedPokemonIndex = 0;
        }

        public void SwitchToState(BattleState state)
        {
            BattleStateManager.EndBattleIfAppropriate();

            ResetFadeTimer();
            StateStack.Push(state);
            Debug.WriteLine(State.ToString());
        }

        public bool SwitchToPreviousState()
        {
            if (StateStack.Count > 1)
            {
                BattleStateManager.Battle.ResetFadeTimer();
                StateStack.Pop();

                if (StateStack.Peek() == BattleState.ItemSelect)
                {
                    GameStateManager.Instance.UIStateStack.Push(UIState.Bag);
                }

                Debug.WriteLine(State.ToString());
                return true;
            }
            return false;
        }

        public bool HasActiveTransation => Transactions.Count() >= 1 && Transactions.Where(t => !t.Complete).Count() > 0;

        public void UpdateTransaction()
        {
            if (Transactions.Peek().Complete)
            {
                Transactions.Dequeue();
            }

            Transaction transaction = Transactions.Peek();

            transaction.Update();
        }

        public void QueueNewTransaction(Action update, Action complete, int counterTemout)
        {
            Transactions.Enqueue(new Transaction
            {
                Counter = 0,
                UpdateAction = update,
                CompleteAction = complete,
                CounterTimeout = counterTemout,
                Complete = false
            });
        }

        public void UpdateTimer()
        {
            if (Timer < 0.1f)
            {
                ResetFadeTimer();
            }

            if (TimerDescending)
            {
                if (Timer > 0.5f)
                {
                    Timer -= 0.02f;
                }
                else
                {
                    TimerDescending = false;
                }
            }
            else
            {
                if (Timer < 1f)
                {
                    Timer += 0.02f;
                }
                else
                {
                    TimerDescending = true;
                }
            }
        }

        public void ResetFadeTimer()
        {
            TimerDescending = false;
            Timer = 0.5f;
        }

        public void HealSelectedPokemon(Direction direction, int amount)
        {
            BattlePokemon target = BattleCharacterStates[direction].GetSelectedPokemon();
            float healAmount = amount / 30f;
            QueueNewTransaction(() => target.Heal(healAmount), () =>
            {
                target.UsedMove = true;
                BattleStateManager.AdvanceStateAfterMoveUsage();
            }, 30);
        }
    }
}
