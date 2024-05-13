using Common.Application.Commands;
using Delivery.Domain.Model.Couriers;

namespace Delivery.Application.Commands;

public class UpdateCourierAvailability
{
    public record Command(Guid CourierId, bool Available) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly ICourierRepository _courierRepository;

        public Handler(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var courier = await _courierRepository.FindByIdAsync(command.CourierId)
                ?? throw new CourierNotFoundException(command.CourierId);
            var events = command.Available ? courier.MakeAvailable() : courier.MakeUnavailable();
            await _courierRepository.SaveAsync(courier);
            domainEventPublisher.publish(courier, events);
        }
    }
}
