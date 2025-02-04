﻿using System.Collections.Generic;
using System.Linq;
using Library.Cutscenes;
using Library.Domain;

namespace Library.GameState.Base.MessageState
{
    public class MessageStateManager
    {
        public static List<Message> Messages { get; private set; }
        private static int RevealedLetters { get; set; }

        public static void EnterMessageState(List<Message> messages)
        {
            Messages = messages;
            BaseStateManager.Instance.StateStack.Push(BaseState.Message);
        }

        public static void Update()
        {
            if (Messages.Count > 0 && RevealedLetters < Messages.First().Content.Length) {
                RevealedLetters++;
            }
        }

        public static void CompleteMessage() {
            Message message = null;

            if (Messages.Count > 0)
            {
                message = Messages[0];
                Messages.RemoveAt(0);
                RevealedLetters = 0;
            }

            if (message != null && message.SpecialActionKey != null)
            {
                SpecialActionManager.SpecialActions[(SpecialActionKey)message.SpecialActionKey].Invoke();
            }

            if (Messages.Count == 0) {
                RevealedLetters = 0;
                BaseStateManager.Instance.StateStack.Pop();
            }
        }

        public static string GetMessage() {
            if (Messages.Count > 0) {
                return Messages.First().Content.Substring(0, RevealedLetters);
            }

            return "";
        }
    }
}
