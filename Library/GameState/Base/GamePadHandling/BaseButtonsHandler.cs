using Library.Assets;
using Library.Content;
using Library.Controls;
using Library.Cutscenes;
using Library.Domain;
using Library.GameState.Base.MessageState;
using Library.Graphics;
using Library.World;
using Library.World.Json;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Library.GameState.Base.GamePadHandling
{
    public static class BaseButtonsHandler
    {
        public static void Update()
        {
            if (ControlsManager.ControlPressed(Control.A))
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
                    return;
                }

                SignJson sign = locationLayout.Signs.ContainsKey(nextPosition) ? locationLayout.Signs[nextPosition] : null;
                if (sign != null)
                {
                    List<Message> messages = new List<Message>();
                    sign.Messages.ForEach(message => messages.Add(new Message(message)));

                    MessageStateManager.EnterMessageState(messages);

                    return;
                }

                CapturedPokemon capturedPokemon = locationState.CapturedPokemon.FirstOrDefault(pokemon => pokemon.Position.ToPoint() == nextPosition);
                if (capturedPokemon != null)
                {
                    locationState.CapturedPokemon.Remove(capturedPokemon);
                    player.GivePokemon(capturedPokemon.Pokemon);

                    return;
                }

                Item item = locationState.Items.FirstOrDefault(item => item.Position.ToPoint() == nextPosition);
                if (item != null)
                {
                    locationState.Items.Remove(item);
                    player.CharacterState.Bag.AddItems(item.ItemName, item.Count);

                    List<Message> messages = new List<Message>();
                    AddItemMessage(messages, item.ItemName, item.Count);

                    MessageStateManager.EnterMessageState(messages);

                    return;
                }

                Character character = locationState.Characters.FirstOrDefault(character => character.CharacterState.Position == newLocation);
                if (character != null && character.CharacterState is NPCState npcState)
                {
                    List<Message> messages = new List<Message>(npcState.Messages);
                    if (character.Name == null || character.CharacterState.Pokemon.Count == 0) {
                        Bag bag = character.CharacterState.Bag;
                        List<ItemName> itemNames = bag.ItemsDictionary.Keys.Where(itemName => bag.ItemsDictionary[itemName] > 0).ToList();
                        bool hasItems = false;
                        foreach (ItemName itemName in itemNames)
                        {
                            int count = bag.ItemsDictionary[itemName];
                            if (count > 0)
                            {
                                GameStateManager.Instance.GetPlayer().CharacterState.Bag.AddItems(itemName, count);
                                bag.RemoveItems(itemName, count);

                                AddItemMessage(messages, itemName, count);
                                hasItems = true;
                            }
                        }

                        if (hasItems)
                        {
                            messages.RemoveAll(message => message.MessageDependency != null && message.MessageDependency == MessageDependency.HasNoItems);
                        } else
                        {
                            messages.RemoveAll(message => message.MessageDependency != null && message.MessageDependency == MessageDependency.HasAnItem);
                        }
                    }

                    if (messages.Count > 0)
                    {
                        MessageStateManager.EnterMessageState(messages);
                    }

                    return;
                }
            }

            if (ControlsManager.ControlPressed(Control.Start))
            {
                GameStateManager.Instance.UIStateStack.Push(UIState.Menu);
                return;
            }
        }

        private static void AddItemMessage(List<Message> messages, ItemName itemName, int count)
        {
            Message message;
            if (count == 1)
            {
                message = new Message("You recieved a " + GraphicsHelper.GetFormattedString(itemName.ToString()) + "!");
            }
            else
            {
                message = new Message("You recieved " + count.ToString() + " " + GraphicsHelper.GetFormattedString(itemName.ToString()) + "s!");
            }

            message.Unique = true;
            messages.Add(message);
        }
    }
}
