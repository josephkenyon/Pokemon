using Library.Assets;
using Library.Content;
using Library.Cutscenes;
using Library.Domain;
using Library.GameState.Base;
using Library.GameState.Base.CutsceneState;
using Library.GameState.Base.TransitionState;
using Library.World;
using Library.World.Json;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.GameState
{
    public static class MovementHandler
    {
        public static Vector GetNewPath(Direction direction, Vector position)
        {
            return direction switch
            {
                Direction.Up => new Vector(position.X, position.Y - Constants.ScaledTileSize),
                Direction.Down => new Vector(position.X, position.Y + Constants.ScaledTileSize),
                Direction.Left => new Vector(position.X - Constants.ScaledTileSize, position.Y),
                Direction.Right => new Vector(position.X + Constants.ScaledTileSize, position.Y),
                _ => Vector.Zero,
            };
        }

        public static Vector GetNewJumpPath(Direction direction, Vector position)
        {
            return direction switch
            {
                Direction.Up => new Vector(position.X, position.Y - Constants.ScaledTileSize * 2),
                Direction.Down => new Vector(position.X, position.Y + Constants.ScaledTileSize * 2),
                Direction.Left => new Vector(position.X - Constants.ScaledTileSize * 2, position.Y),
                Direction.Right => new Vector(position.X + Constants.ScaledTileSize * 2, position.Y),
                _ => Vector.Zero,
            };
        }

        public static void MoveCharacter(Character character)
        {
            Vector position = character.CharacterState.Position;
            Vector movementPath = character.CharacterState.MovementPath;

            int xDiff = position.X - movementPath.X;
            int yDiff = position.Y - movementPath.Y;

            float speed = Constants.CharacterSpeed * (character.CharacterState.IsJumping || character.CharacterState.IsFalling ? 0.5f : 1f);
            if (xDiff < 0)
            {
                character.Move(new Vector(position.X + (speed > Math.Abs(xDiff) ? Math.Abs(xDiff) : speed), position.Y));
            }
            else if (xDiff > 0)
            {
                character.Move(new Vector(position.X - (speed > xDiff ? xDiff : speed), position.Y));
            }
            else if (yDiff < 0)
            {
                character.Move(new Vector(position.X, position.Y + (speed > Math.Abs(yDiff) ? Math.Abs(yDiff) : speed)));
            }
            else if (yDiff > 0)
            {
                character.Move(new Vector(position.X, position.Y - (speed > yDiff ? yDiff : speed)));
            }
            else
            {
                MovementCutsceneTransaction movementCutsceneTransaction = CutsceneStateManager.GetActiveMovementCutsceneTransaction();
                if (movementCutsceneTransaction != null && character.Name != null)
                {
                    List<CharacterName> characterNames = movementCutsceneTransaction.MovementTransaction.CharacterNames;
                    if (characterNames.Contains((CharacterName)character.Name))
                    {
                        int index = characterNames.IndexOf((CharacterName)character.Name);
                        movementCutsceneTransaction.MovementTransaction.Distances[index]--;

                        if (movementCutsceneTransaction.MovementTransaction.Distances[index] > 0)
                        {
                            character.CharacterState.StartMoving(movementCutsceneTransaction.MovementTransaction.Directions[index]);
                            return;
                        }
                    }
                }

                Point newLocationPoint = (movementPath / Constants.ScaledTileSize).ToPoint();
                LocationName currentLocation = character.CharacterState.CurrentLocation;
                LocationState currentLocationState = BaseStateManager.Instance.LocationStates[currentLocation];
                CutsceneJson cutscene = currentLocationState.Cutscenes.Where(cutscene => cutscene.TriggerType == CutsceneTriggerType.Movement && cutscene.TriggerPoints.Contains(newLocationPoint) && cutscene.IsValid()).FirstOrDefault();

                if (character.Name == CharacterName.Ash && cutscene != null)
                {
                    CutsceneManager.BeginCutscene(cutscene);
                }
                else if (CollisionHandler.NewStitchLocationName != null && character.Name != null)
                {
                    TeleportTransaction teleportTransaction = new TeleportTransaction
                    {
                        ToLocationName = (LocationName)CollisionHandler.NewStitchLocationName,
                        ToLocationPoint = CollisionHandler.NewStitchLocation,
                        Instant = true,
                        CharacterName = (CharacterName)character.Name
                    };

                    TransitionStateManager.StartTransition(teleportTransaction);
                    CollisionHandler.NewStitchLocationName = null;
                }
                else
                {
                    LocationLayout location = LocationManager.LocationLayouts[currentLocation];

                    if (character.Name != null && location.Portals.ContainsKey(newLocationPoint))
                    {
                        PortalJson portal = location.Portals[newLocationPoint];

                        TeleportTransaction teleportTransaction = new TeleportTransaction
                        {
                            ToLocationName = portal.ToLocationName,
                            ToLocationPoint = portal.Coordinate,
                            Instant = character.Name != CharacterName.Ash,
                            CharacterName = (CharacterName)character.Name
                        };

                        TransitionStateManager.StartTransition(teleportTransaction);
                    }
                    else
                    {
                        if (!EncounterManager.EncounterPokemon())
                        {
                            SpriteEffectsManager.CharacterMovementCompleted(character);
                        }
                    }
                }

                character.CharacterState.IsMoving = false;
            }
        }
    }
}