using Common.Domain.Model;

namespace Consumer.Domain.Model.Consumers;

public class Consumer(PersonName name)
{
    public Guid Id { get; } = Guid.NewGuid();
    public PersonName Name { get; } = name;

    public static (Consumer, List<IDomainEvent>) Create(PersonName name)
    {
        var consumer = new Consumer(name);
        return (consumer, [new ConsumerCreated(consumer.Id, consumer.Name)]);
    }

    public void ValidateOrderByConsumer(decimal orderTotal)
    {
        throw new NotImplementedException();
    }
}
