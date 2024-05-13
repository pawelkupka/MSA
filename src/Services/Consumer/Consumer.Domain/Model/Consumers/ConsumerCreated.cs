using Common.Domain.Model;

namespace Consumer.Domain.Model.Consumers;

public record ConsumerCreated(Guid ConsumerId, PersonName Name) : IDomainEvent;