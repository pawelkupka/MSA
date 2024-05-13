using Common.Application.Commands;
using Consumer.Domain.Model.Consumers;

namespace Consumer.Application.Commands;

public class CreateConsumer
{
    public record Command(PersonName Name) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IConsumerRepository _consumerRepository;

        public Handler(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            (var consumer, var events) = Domain.Model.Consumers.Consumer.Create(command.Name);
            await _consumerRepository.SaveAsync(consumer);
            domainEventPublisher.publish(consumer, events);
        }
    }
}
