using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Model.Restaurants;

public class Restaurant
{
    public Restaurant(string name, RestaurantAddress address)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        //AddDomainEvent(new RestaurantCreated(RestaurantId, Name, Address));
    }

    public Guid Id { get; }
    public string Name { get; }
    public RestaurantAddress Address { get; }
}
