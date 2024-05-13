namespace Consumer.Domain.Model.Consumers;

public class ConsumerNotFoundException(Guid consumerId) : Exception($"Consumer with id {consumerId} not found");