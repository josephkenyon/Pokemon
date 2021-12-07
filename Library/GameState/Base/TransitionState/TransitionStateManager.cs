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
        public static LocationName TransitionLocationName { get; private set; }
        public static Point TransitionLocationPoint { get; private set; }
        public static int Counter { get; set; }
        public static bool TransitionInstantlyNextTime { get; private set; }
        private static Texture2D BlankTexture { get; set; }

        private static Rectangle DestinationRectangle = new Rectangle(0, 0, Constants.ResolutionWidth, Constants.ResolutionHeight);

        private static Rectangle SourceRectangle = new Rectangle(0, 0, 1, 1);

        public static void StartTransition(LocationName locationName, Point locationCoordinates) {
            TransitionLocationName = locationName;
            TransitionLocationPoint = locationCoordinates;
            Counter = 0;

            BaseStateManager.Instance.BaseState = BaseState.Transition;

            if (BlankTexture == null) {
                BlankTexture = CreateTexture(GraphicsManager.GraphicsDevice);
            }
        }

        public static void TransitionInstantly(LocationName locationName, Point locationCoordinates)
        {
            TransitionInstantlyNextTime = true;
            StartTransition(locationName, locationCoordinates);
        }

        private static void Transition(LocationName locationName, Point locationCoordinates)
        {
            Player player = GameStateManager.Instance.GetPlayer();
            CharacterState playerState = player.CharacterState;
            BaseStateManager.Instance.LocationStates[playerState.CurrentLocation].Characters.Remove(player);

            playerState.CurrentLocation = locationName;
            playerState.Position = new Vector(locationCoordinates) * Constants.ScaledTileSize;
            playerState.IsMoving = false;

            BaseStateManager.Instance.LocationStates[locationName].Characters.Add(player);

            TransitionInstantlyNextTime = false;
        }

        public static void Update()
        {
            if (Counter == Constants.TransitionTime / 2 || TransitionInstantlyNextTime)
            {
                if (TransitionInstantlyNextTime)
                {
                    BaseStateManager.Instance.BaseState = BaseState.Base;
                }

                Transition(TransitionLocationName, TransitionLocationPoint);
            }
            else if (Counter >= Constants.TransitionTime)
            {
                BaseStateManager.Instance.BaseState = BaseState.Base;
            }

            Counter++;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            float scaler = 0f;
            if (!TransitionInstantlyNextTime)
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
