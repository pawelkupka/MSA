namespace Common.Application.Events;

public interface IEventPublisher
{
    void Publish(string aggregateType, object aggregateId, IEnumerable<IEvent> domainEvents);
    void Publish<TAggregate>(object aggregateId, IEnumerable<IEvent> domainEvents);
    Task PublishAsync(string aggregateType, object aggregateId, IEnumerable<IEvent> domainEvents);
    Task PublishAsync<TAggregate>(object aggregateId, IEnumerable<IEvent> domainEvents);
}