namespace Kitchen.Domain.Model.Restaurants;

public class RestaurantIdNotFoundException(Guid RestaurantId) : Exception;