namespace Common.Domain.Messaging;

public interface IMessageBroker
{
    void Send(string destination, IMessage message);
    Task SendAsync(string destination, IMessage message);
}
