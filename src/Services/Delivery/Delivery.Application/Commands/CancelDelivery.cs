using Common.Application.Commands;
using Common.Application.Validation;
using Delivery.Domain.Model.Couriers;
using Delivery.Domain.Model.Deliveries;

namespace Delivery.Application.Commands;

public static class CancelDelivery
{
    public record Command(Guid OrderId) : ICommand;

    public class Validator : IValidable
    {
        public async Task<ValidationResult> Validate(Command command)
        {
            //if (repository.Todos.Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
            //    return ValidationResult.Fail("Todo already exists.");

            return ValidationResult.Success();
        }
    }

    public class Handler : ICommandHandler<Command>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ICourierRepository _courierRepository;

        public Handler(IDeliveryRepository deliveryRepository, ICourierRepository courierRepository)
        {
            _deliveryRepository = deliveryRepository;
            _courierRepository = courierRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.FindByOrderIdAsync(command.OrderId);
            delivery.Cancel();
            await _deliveryRepository.UpdateAsync(delivery);
            if (delivery.HasAssignedCourier)
            {
                var courier = await _courierRepository.FindByIdAsync(delivery.CourierId.Value);
                courier.RemoveDelivery(delivery.Id);
                await _courierRepository.UpdateAsync(courier);
            }
        }
    }
}
