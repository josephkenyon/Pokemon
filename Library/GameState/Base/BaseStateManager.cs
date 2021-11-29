using System;
using System.Collections.Generic;
using System.Linq;
using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base
{
    public class BaseStateManager : IStateManager
    {
        public static BaseStateManager Instance { get; private set; }
        public Dictionary<LocationName, LocationState> LocationStates { get; private set; }

        public BaseStateManager()
        {
            Instance = this;
            LocationStates = new Dictionary<LocationName, LocationState>();

            foreach (LocationName locationName in Enum.GetValues(typeof(LocationName)))
            {
                LocationStates.Add(locationName, new LocationState
                {
                    Characters = LocationManager.LocationLayouts[locationName].InitialCharacters,
                    Name = locationName
                });
            }
        }

        public bool Update()
        {
            BaseGamePadHandler.Update();

            LocationStates[GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation].Update();

            GameStateManager.Instance.CameraLocation = GameStateManager.Instance.GetPlayer().CharacterState.Position + (new Vector(GameStateManager.Instance.GetPlayer().GetTargetRectangle().Size) / 2);

            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LocationManager.LocationLayouts[GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation].DrawBackground(spriteBatch);

            LocationStates[GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation].Draw(spriteBatch);

            LocationManager.LocationLayouts[GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation].DrawForeground(spriteBatch);
        }

        public LocationName GetPlayerLocation() { 
             return LocationStates.Values.Single(state => state.Characters.Exists(character => character.Name == Constants.PlayerName)).Name;
        }
    }
}
