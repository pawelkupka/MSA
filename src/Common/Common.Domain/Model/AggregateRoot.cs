namespace Common.Domain.Model
{
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
            DomainEvents = new List<IDomainEvent>();
        }

        public IEnumerable<IDomainEvent> DomainEvents { get; }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            DomainEvents.Append(domainEvent);
        }
    }
}
