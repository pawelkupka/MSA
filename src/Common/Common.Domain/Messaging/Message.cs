namespace Common.Domain.Messaging;

public class Message : IMessage
{
    public Message(IDictionary<string, string> headers, string body)
    {
        Headers = headers;
        Body = body;
    }

    public IDictionary<string, string> Headers { get; }
    public string Body { get; }
}
