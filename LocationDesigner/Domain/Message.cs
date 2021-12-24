namespace LocationDesigner.Domain
{
    public class Message
    {
        public string Content { get; set; }
        public bool Unique { get; set; }
        public string MessageDependency { get; set; }
        public string SpecialActionKey { get; set; }

        public Message()
        {
        }
    }
}
