namespace Delivery.Domain.Model.Deliveries;

public record DeliveryAddress(
    string Line1,
    string Line2,
    string City,
    string PostalCode);
