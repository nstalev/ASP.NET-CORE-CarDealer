
namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Models;


    public class CustomersService : ICustomersService
    {
        private readonly CarDealerDbContext db;


        public CustomersService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery.OrderBy(b => b.BirthDate).ThenBy(y => y.IsYoungDriver);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery.OrderByDescending(b => b.BirthDate).ThenBy(y => y.IsYoungDriver);

                    break;
                default:
                    throw new InvalidOperationException($"Invalid Order directions {order}");
            }


            return customersQuery
                .Select(c => new CustomerModel()
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }
    }
}
