
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Customers;
    using System;
    using System.Collections.Generic;

    public interface ICustomersService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerSalesModel CustomerAndSales(int id);

        void Create(string name, DateTime birthDate, bool isYoungDriver);
    }
}
