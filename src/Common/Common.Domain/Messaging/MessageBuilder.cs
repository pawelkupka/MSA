namespace Common.Domain.Messaging;

public class MessageBuilder
{
    private string _body;
    private IDictionary<string, string> _headers;

    public MessageBuilder()
    {
        _headers = new Dictionary<string, string>();
    }

    public MessageBuilder WithHeader(string name, string value)
    {
        _headers[name] = value;
        return this;
    }

    public MessageBuilder WithBody(string body)
    {
        _body = body;
        return this;
    }

    public Message Build()
    {
        return new Message(_headers, _body);
    }
}
