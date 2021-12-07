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
        public override int NumberOfFrames => 2;

        public NPCState() {
            Messages = new List<Message>();
        }

        public void Update() {
            TimeSinceWander += 5;
            Random rnd = new Random();
            int number = TimeSinceWander >= Constants.NPCRandomConstant ? 1 : rnd.Next(1, Constants.NPCRandomConstant - TimeSinceWander);
            if (number == 1) {
                int directionNum = rnd.Next(1, 4) - 1;
                Direction direction = (Direction) directionNum;

                Vector movementPath = MovementHandler.GetNewPath(direction, Position);
                if (CollisionHandler.IsValidMove(this, movementPath)) {
                    StartMoving(direction);
                    TimeSinceWander = 0;
                }
            }
        }
    }
}
