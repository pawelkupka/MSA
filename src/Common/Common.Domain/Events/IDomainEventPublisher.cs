namespace Common.Domain.Events;

public interface IDomainEventPublisher
{
    void Publish(string aggregateType, object aggregateId, IEnumerable<IDomainEvent> domainEvents);
    void Publish<TAggregate>(object aggregateId, IEnumerable<IDomainEvent> domainEvents);
    Task PublishAsync(string aggregateType, object aggregateId, IEnumerable<IDomainEvent> domainEvents);
    Task PublishAsync<TAggregate>(object aggregateId, IEnumerable<IDomainEvent> domainEvents);
}