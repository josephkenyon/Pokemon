using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.GameState.Base.MessageState;
using Library.GameState.Battle;
using Library.GameState.Input;
using Library.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.GameState.Base
{
    public static class BaseGamePadHandler
    {
        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //string a = JsonConvert.SerializeObject(GameStateManager.Instance);

                List<Pokemon> list = new List<Pokemon>() {
                    new Pokemon(Species.Ekans, 5),
                    new Pokemon(Species.Caterpie, 5),
                    new Pokemon(Species.Butterfree, 5),
                    new Pokemon(Species.Weedle, 5),
                    new Pokemon(Species.Fearow, 25)
                };

                List<Pokemon> list2 = new List<Pokemon>() {
                    new Pokemon(Species.Charmander, 10),
                    new Pokemon(Species.Bulbasaur, 9),
                    new Pokemon(Species.Squirtle, 5),
                    //new Pokemon(Species.Venusaur, 60),
                    //new Pokemon(Species.Pidgey, 5),
                    //new Pokemon(Species.Pikachu, 5)
                };

                list2[0].Moves = new List<Move> { MoveManager.Moves[MoveName.Scratch].GetCopy(), MoveManager.Moves[MoveName.Ember].GetCopy() };

                ((BattleStateManager)GameStateManager.Instance.StateManagers[UIState.Battle]).CreateNewBattle(new CharacterState { Pokemon = list2 }, new CharacterState { Pokemon = list });
                GameStateManager.Instance.UIState = UIState.Battle;
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                Player player = GameStateManager.Instance.GetPlayer();
                Vector newLocation = MovementHandler.GetNewPath(player.CharacterState.Direction, player.CharacterState.Position);
                LocationLayout locationLayout = LocationManager.LocationLayouts[BaseStateManager.Instance.GetPlayerLocation()];
                Point nextPosition = (newLocation / Constants.ScaledTileSize).ToPoint();
                SignJson sign = locationLayout.Signs.ContainsKey(nextPosition) ? locationLayout.Signs[nextPosition] : null;
                if (sign != null)
                {
                    BaseStateManager.Instance.BaseState = BaseState.Message;
                    MessageStateManager.Messages = new List<Message>();
                    sign.Messages.ForEach(message => MessageStateManager.Messages.Add(new Message(message)));
                }
                else {
                    LocationState locationState = BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()];
                    Character character = locationState.Characters.FirstOrDefault(character => character.CharacterState.Position == newLocation);
                    if (character != null && character.CharacterState is NPCState npcState) {
                        BaseStateManager.Instance.BaseState = BaseState.Message;
                        MessageStateManager.Messages = new List<Message>(npcState.Messages);
                    }
                }
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                return;
            }

            if (gamePadState.Buttons.Start == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                GameStateManager.Instance.UIState = UIState.Menu;
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                return;
            }

            Direction? nullableDir = !GameStateManager.Instance.GetPlayer().CharacterState.IsMoving ? GamePadHelper.GetDPadDirection(gamePadState) : null;

            if (nullableDir != null)
            {
                Direction direction = (Direction)nullableDir;
                Vector newLocation = MovementHandler.GetNewPath(direction, GameStateManager.Instance.GetPlayer().CharacterState.Position);
                if (CollisionHandler.IsValidMove(GameStateManager.Instance.GetPlayer().CharacterState, newLocation))
                {
                    GameStateManager.Instance.GetPlayer().CharacterState.StartMoving(direction);
                }
                GameStateManager.Instance.GetPlayer().CharacterState.Direction = direction;
            }
        }
    }
}
