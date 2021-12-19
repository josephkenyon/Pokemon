using Library.Assets;
using Library.Content;
using Library.Controls;
using Library.Cutscenes;
using Library.Domain;
using Library.GameState.Base.MessageState;
using Library.World;
using Library.World.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Library.GameState.Base.GamePadHandling
{
    public static class BaseButtonsHandler
    {
        public static void Update()
        {
            if (ControlsManager.APressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                Player player = GameStateManager.Instance.GetPlayer();
                Vector newLocation = MovementHandler.GetNewPath(player.CharacterState.Direction, player.CharacterState.Position);
                LocationLayout locationLayout = LocationManager.LocationLayouts[BaseStateManager.Instance.GetPlayerLocation()];
                Point nextPosition = (newLocation / Constants.ScaledTileSize).ToPoint();
                LocationState locationState = BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()];

                CutsceneJson cutscene = locationState.Cutscenes.Where(cutscene => cutscene.TriggerType == CutsceneTriggerType.Selection && cutscene.TriggerPoints.Contains(nextPosition) && cutscene.IsValid()).FirstOrDefault();
                if (cutscene != null)
                {
                    CutsceneManager.BeginCutscene(cutscene);
                    GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                    return;
                }

                SignJson sign = locationLayout.Signs.ContainsKey(nextPosition) ? locationLayout.Signs[nextPosition] : null;
                if (sign != null)
                {
                    List<Message> messages = new List<Message>();
                    sign.Messages.ForEach(message => messages.Add(new Message(message)));

                    MessageStateManager.EnterMessageState(messages);
                    GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;

                    return;
                }

                CapturedPokemon capturedPokemon = locationState.CapturedPokemon.FirstOrDefault(pokemon => pokemon.Position.ToPoint() == nextPosition);
                if (capturedPokemon != null)
                {
                    locationState.CapturedPokemon.Remove(capturedPokemon);
                    player.GivePokemon(capturedPokemon.Pokemon);
                    GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;

                    return;
                }

                Character character = locationState.Characters.FirstOrDefault(character => character.CharacterState.Position == newLocation);
                if (character != null && character.CharacterState is NPCState npcState)
                {
                    if (character.Name == null || character.CharacterState.Pokemon.Count == 0) {
                        Bag bag = character.CharacterState.Bag;
                        List<ItemName> itemNames = bag.ItemsDictionary.Keys.Where(itemName => bag.ItemsDictionary[itemName] > 0).ToList();
                        foreach (ItemName itemName in itemNames)
                        {
                            GameStateManager.Instance.GetPlayer().CharacterState.Bag.AddItems(itemName, bag.ItemsDictionary[itemName]);
                            bag.RemoveItems(itemName, bag.ItemsDictionary[itemName]);
                            int count = bag.ItemsDictionary[itemName];
                            if (count == 1)
                            {
                                npcState.Messages.Add(new Message("You recieved a " + itemName.ToString() + "!"));
                            }
                            else
                            {
                                npcState.Messages.Add(new Message("You recieved " + count.ToString() + " " + itemName.ToString() + "s!"));
                            }
                        }
                    }

                    MessageStateManager.EnterMessageState(new List<Message>(npcState.Messages));
                    GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;

                    return;
                }
            }

            if (ControlsManager.StartPressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                GameStateManager.Instance.UIStateStack.Push(UIState.Menu);
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
                return;
            }
        }
    }
}
