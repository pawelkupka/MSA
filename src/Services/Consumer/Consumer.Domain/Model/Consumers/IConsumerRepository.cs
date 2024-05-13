namespace Consumer.Domain.Model.Consumers;

public interface IConsumerRepository
{
    Task<Consumer> FindByIdAsync(Guid consumerId);
    Task SaveAsync(Consumer consumer);
}
