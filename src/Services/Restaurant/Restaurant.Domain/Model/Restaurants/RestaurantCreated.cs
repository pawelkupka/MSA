using Common.Domain.Model;

namespace Restaurant.Domain.Model.Restaurants;

public record RestaurantCreated(Guid Id, string Name, RestaurantMenu Menu) : IDomainEvent;