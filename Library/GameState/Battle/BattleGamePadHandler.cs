using Library.Controls;
using Library.Domain;
using Library.GameState.Battle.GamePadHelpers;

namespace Library.GameState.Battle
{
    public static class BattleGamePadHandler
    {
        public static void Update()
        {
            if (ControlsManager.ControlPressed(Control.B))
            {
                BattleStateManager.Battle.SwitchToPreviousState();
            }

            switch (BattleStateManager.Battle.State)
            {
                case BattleState.PokemonSelect:
                    PokemonSelectHelper.Update();
                    break;
                case BattleState.AshSelect:
                    AshSelectHelper.Update();
                    break;
                case BattleState.MoveSelect:
                    MoveSelectHelper.Update();
                    break;
                case BattleState.EnemySelect:
                    EnemySelectHelper.Update();
                    break;
            }
        }
    }
}