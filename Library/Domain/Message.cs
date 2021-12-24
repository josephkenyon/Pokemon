namespace Library.Domain
{
    public class Message
    {
        public string Content { get; set; }
        public bool Unique { get; set; }
        public MessageDependency? MessageDependency { get; set; }

        public Message(string content)
        {
            Content = content;
        }
    }
}
