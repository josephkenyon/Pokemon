using Library.Domain;
using System.Collections.Generic;

namespace Library.Cutscenes
{
    public class MessageCutsceneTransaction : ICutsceneTransaction
    {
        public List<string> Messages { get; set; }
        public CutsceneTransactionType GetCutsceneTransactionType() => CutsceneTransactionType.Message;

        public MessageCutsceneTransaction(List<string> messages)
        {
            Messages = new List<string>();
            messages.ForEach(message => Messages.Add(string.Copy(message)));
        }
    }
}
