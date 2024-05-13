using Common.Domain.Model;

namespace Delivery.Domain.Model.Couriers;

public record CourierCreated(Guid CourierId, string Name, bool Available, CourierDeliveryPlan DeliveryPlan) : IDomainEvent;
