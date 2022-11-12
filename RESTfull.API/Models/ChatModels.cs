namespace RESTfull.API.Models;

public class ChatModels
{
    public class CreateChatRequest
    {
        public string Name { get; set; }
    }
    
    public class UpdateChatRequest
    {
        public string Name { get; set; }
    }

    public class CreateMessageRequest
    {
        public string Text { get; set; }
    }
}