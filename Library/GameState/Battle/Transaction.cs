using System;

namespace Library.GameState.Battle
{
    public class Transaction
    {
        public int Counter { get; set; }
        public int CounterTimeout { get; set; }
        public bool Complete { get; set;}
        public Action UpdateAction { get; set; }
        public Action CompleteAction { get; set; }

        public void Update()
        {
            UpdateAction.Invoke();
            Counter++;

            if (Counter >= CounterTimeout) {
                CompleteAction.Invoke();
                Complete = true;
            }
        }
    }
}
