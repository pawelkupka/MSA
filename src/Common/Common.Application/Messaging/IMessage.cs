namespace Common.Application.Messaging;

public interface IMessage
{
    IDictionary<string, string> Headers { get; }
    string Body { get; }
}
