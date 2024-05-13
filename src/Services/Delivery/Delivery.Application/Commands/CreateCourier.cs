using Common.Application.Commands;
using Delivery.Domain.Model.Couriers;

namespace Delivery.Application.Commands;

public class CreateCourier
{
    public record Command(string Name, bool Available) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly ICourierRepository _courierRepository;

        public Handler(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            (var courier, var events) = Courier.Create(command.Name, command.Available);
            await _courierRepository.SaveAsync(courier);
            domainEventPublisher.publish(courier, events);
        }
    }
}
