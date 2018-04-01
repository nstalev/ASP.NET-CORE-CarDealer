
namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Customers;


    public class CustomersService : ICustomersService
    {
        private readonly CarDealerDbContext db;
        private const double AdditionalDiscount = 0.05;

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



        public CustomerSalesModel CustomerAndSales(int id)
        {

            var customer = db.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerSalesModel
                {
                    Name = c.Name,
                    BoughtCars = c.Sales.Count,
                    TotalSpentMoney = c.Sales.Select(s => s.Car.Parts.Sum(p => p.Part.Price)).Sum(),
                })
                .FirstOrDefault();

            return customer;
        }
    }
}
