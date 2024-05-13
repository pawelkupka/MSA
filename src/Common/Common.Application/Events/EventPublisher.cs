using System.Text.Json;

namespace Common.Application.Events;

using Messaging;

public class EventPublisher : IEventPublisher
{
    private readonly IMessageBroker _messageBroker;

    public EventPublisher(IMessageBroker messageProducer)
    {
        _messageBroker = messageProducer;
    }

    public void Publish(string aggregateType, object aggregateId, IEnumerable<IEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            _messageBroker.Send(aggregateType, CreateMessage(aggregateType, aggregateId, domainEvent));
        }
    }

    public void Publish<TAggregate>(object aggregateId, IEnumerable<IEvent> domainEvents)
    {
        Publish(typeof(TAggregate).FullName, aggregateId, domainEvents);
    }

    public async Task PublishAsync(string aggregateType, object aggregateId, IEnumerable<IEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _messageBroker.SendAsync(aggregateType, CreateMessage(aggregateType, aggregateId, domainEvent));
        }
    }

    public async Task PublishAsync<TAggregate>(object aggregateId, IEnumerable<IEvent> domainEvents)
    {
        await PublishAsync(typeof(TAggregate).FullName, aggregateId, domainEvents);
    }

    private IMessage CreateMessage(string aggregateType, object aggregateId, IEvent @event)
    {
        var eventType = @event.GetType().FullName;
        var aggregateIdAsString = aggregateId.ToString();
        return new MessageBuilder()
            .WithBody(JsonSerializer.Serialize(@event))
            .WithHeader("event-type", eventType)
            .WithHeader("event-aggregate-type", aggregateType)
            .WithHeader("event-aggregate-id", aggregateIdAsString)
            .Build();
    }
}
