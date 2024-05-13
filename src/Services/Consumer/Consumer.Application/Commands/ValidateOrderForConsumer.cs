using Common.Application.Commands;
using Consumer.Domain.Model.Consumers;

namespace Consumer.Application.Commands;

public class ValidateOrderForConsumer
{
    public record Command(Guid ConsumerId, decimal OrderTotal) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IConsumerRepository _consumerRepository;

        public Handler(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var consumer = await _consumerRepository.FindByIdAsync(command.ConsumerId)
                ?? throw new ConsumerNotFoundException(command.ConsumerId);
            consumer.ValidateOrderByConsumer(command.OrderTotal);
            await _consumerRepository.SaveAsync(consumer);
        }
    }
}
