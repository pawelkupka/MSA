using MediatR;

namespace Common.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    Guid AggregateId { get; }
    string AggregateType { get; }
    int EventVersion { get; set; }
    DateTimeOffset OccurredOn { get; set; }
}