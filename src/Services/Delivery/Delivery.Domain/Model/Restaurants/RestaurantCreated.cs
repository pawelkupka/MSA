using Common.Domain.Model;

namespace Delivery.Domain.Model.Restaurants;

public record RestaurantCreated(Guid RestaurantId, string Name, RestaurantAddress Address) : IDomainEvent;
