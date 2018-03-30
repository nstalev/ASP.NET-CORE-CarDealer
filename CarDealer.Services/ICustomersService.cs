
namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;

    public interface ICustomersService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);
    }
}
