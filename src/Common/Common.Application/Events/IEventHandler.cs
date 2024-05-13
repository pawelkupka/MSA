namespace Common.Application.Events;

public interface IEventHandler<in TDomainEvent> where TDomainEvent : IEvent
{
    Task HandleAsync(TDomainEvent @event);
}