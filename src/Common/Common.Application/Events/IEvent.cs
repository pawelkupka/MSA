using MediatR;

namespace Common.Application.Events;

public interface IEvent : INotification
{
    Guid EventId { get; }
    Guid AggregateId { get; }
    string AggregateType { get; }
    int EventVersion { get; set; }
    DateTimeOffset OccurredOn { get; set; }
}