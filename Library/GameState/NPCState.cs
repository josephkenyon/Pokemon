using Library.Domain;
using System;
using System.Collections.Generic;

namespace Library.GameState
{
    public class NPCState : CharacterState
    {

        public bool Wanders { get; set; }
        public int TimeSinceWander { get; set; }
        public List<Message> Messages { get; set; }

        public NPCState(IAnimatedAsset parentAsset) : base(parentAsset) {
            Messages = new List<Message>();
            CurrentFrame = parentAsset.NumberOfFrames - Constants.NPCDefaultFrameCount;
        }

        public void Update() {
            if (Wanders)
            {
                TimeSinceWander += 5;
                Random rnd = new Random();
                int number = TimeSinceWander >= Constants.NPCRandomConstant ? 1 : rnd.Next(1, Constants.NPCRandomConstant - TimeSinceWander);
                if (number == 1 || TimeSinceWander > Constants.NPCRandomConstant)
                {
                    int directionNum = rnd.Next(1, 4) - 1;
                    Direction direction = (Direction)directionNum;

                    Vector movementPath = MovementHandler.GetNewPath(direction, Position);
                    if (CollisionHandler.IsValidMove(this, movementPath))
                    {
                        StartMoving(direction);
                        TimeSinceWander = 0;
                    }
                }
            }
        }
    }
}
