﻿using Library.Domain;
using Library.GameState.Battle.GamePadHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Library.GameState.Battle
{
    public static class BattleGamePadHandler
    {
        public static void Update()
        {
            if (GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    if (BattleStateManager.Battle.SwitchToPreviousState())
                    {
                        GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                    }
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
}