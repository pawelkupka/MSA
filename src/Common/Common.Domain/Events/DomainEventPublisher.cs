using System.Text.Json;

namespace Common.Domain.Events;

using Messaging;

public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly IMessageBroker _messageBroker;

    public DomainEventPublisher(IMessageBroker messageProducer)
    {
        _messageBroker = messageProducer;
    }

    public void Publish(string aggregateType, object aggregateId, IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            _messageBroker.Send(aggregateType, CreateMessage(aggregateType, aggregateId, domainEvent));
        }
    }

    public void Publish<TAggregate>(object aggregateId, IEnumerable<IDomainEvent> domainEvents)
    {
        Publish(typeof(TAggregate).FullName, aggregateId, domainEvents);
    }

    public async Task PublishAsync(string aggregateType, object aggregateId, IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _messageBroker.SendAsync(aggregateType, CreateMessage(aggregateType, aggregateId, domainEvent));
        }
    }

    public async Task PublishAsync<TAggregate>(object aggregateId, IEnumerable<IDomainEvent> domainEvents)
    {
        await PublishAsync(typeof(TAggregate).FullName, aggregateId, domainEvents);
    }

    private IMessage CreateMessage(string aggregateType, object aggregateId, IDomainEvent @event)
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
