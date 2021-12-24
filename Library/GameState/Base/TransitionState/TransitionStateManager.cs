using System.Collections.Generic;
using System.Linq;
using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base.TransitionState
{
    public class TransitionStateManager
    {
        public static List<TeleportTransaction> TeleportTransactions { get; private set; }
        public static int Counter { get; set; }

        private static Texture2D BlankTexture { get; set; }
        private static Rectangle DestinationRectangle = new Rectangle(0, 0, Constants.ResolutionWidth, Constants.ResolutionHeight);
        private static Rectangle SourceRectangle = new Rectangle(0, 0, 1, 1);

        public static void StartTransition(TeleportTransaction teleportTransaction) {
            TeleportTransactions = new List<TeleportTransaction>
            {
                teleportTransaction
            };

            StartTransitionList(TeleportTransactions);
        }

        public static void StartTransitionList(List<TeleportTransaction> teleportTransactions)
        {
            TeleportTransactions = teleportTransactions;
            Counter = 0;

            BaseStateManager.Instance.StateStack.Push(BaseState.Transition);

            if (BlankTexture == null)
            {
                BlankTexture = CreateTexture(GraphicsManager.GraphicsDevice);
            }
        }

        private static void Transition()
        {
            if (TeleportTransactions == null)
            {
                return;
            }

            TeleportTransactions.ForEach(teleportTransaction =>
            {
                Character character = GameStateManager.Instance.GetCharacter(teleportTransaction.CharacterName);
                if (character == null)
                {
                    character = new NPC
                    {
                        Name = teleportTransaction.CharacterName
                    };

                    character.CharacterState = new NPCState(character);
                }
                else if (teleportTransaction.ToLocationName != null)
                {
                    BaseStateManager.Instance.LocationStates[character.CharacterState.CurrentLocation].Characters.Remove(character);
                }

                CharacterState characterState = character.CharacterState;
                if (teleportTransaction.ToLocationName != null)
                {
                    characterState.CurrentLocation = (LocationName)teleportTransaction.ToLocationName;
                    characterState.MovementPath = characterState.Position - characterState.MovementPath;
                    characterState.Position = new Vector(teleportTransaction.ToLocationPoint) * Constants.ScaledTileSize;
                    characterState.MovementPath = characterState.Position + characterState.MovementPath;
                    BaseStateManager.Instance.LocationStates[(LocationName)teleportTransaction.ToLocationName].Characters.Add(character);
                }

                if (teleportTransaction.FinalDirection != null)
                {
                    characterState.Direction = (Direction)teleportTransaction.FinalDirection;
                    characterState.CurrentFrame = character.NumberOfFrames - Constants.NPCDefaultFrameCount;
                    characterState.FrameSkip = 0;
                }
            });

            if (!TeleportTransactions.Exists(teleportTransaction => !teleportTransaction.Instant))
            {
                TeleportTransactions = null;
            }
        }

        public static void Update()
        {
            if (Counter == Constants.TransitionTime / 2 || (TeleportTransactions != null && !TeleportTransactions.Exists(teleportTransaction => !teleportTransaction.Instant)))
            {
                if (TeleportTransactions != null && !TeleportTransactions.Exists(teleportTransaction => !teleportTransaction.Instant))
                {
                    BaseStateManager.Instance.StateStack.Pop();
                }

                Transition();
            }
            else if (Counter >= Constants.TransitionTime)
            {
                BaseStateManager.Instance.StateStack.Pop();
                TeleportTransactions = null;
            }

            Counter++;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            float scaler = 0f;
            if (TeleportTransactions == null || TeleportTransactions.Exists(teleportTransaction => !teleportTransaction.Instant))
            {
                if (Counter < Constants.TransitionTime / 2)
                {
                    scaler = ((1f / Constants.TransitionTime) / 2) * Counter * 2.5f;
                }
                else if (Counter == Constants.TransitionTime / 2)
                {
                    scaler = 1f;
                }
                else
                {
                    scaler = ((1f / Constants.TransitionTime) * 2) * (Constants.TransitionTime - Counter);
                }
            }

            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.BasicTextures[TextureName.EmptyWhiteTexture], DestinationRectangle, SourceRectangle, Color.Black * scaler);
            spriteBatch.End();
        }

        private static Texture2D CreateTexture(GraphicsDevice device)
        {
            Texture2D texture = new Texture2D(device, 1, 1);

            Color[] data = new Color[1];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = Color.White;
            }

            texture.SetData(data);

            return texture;
        }
    }
}
