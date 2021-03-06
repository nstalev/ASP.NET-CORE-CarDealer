﻿
namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Data.Models;


    public class CustomersService : ICustomersService
    {
        private readonly CarDealerDbContext db;
        private const double AdditionalDiscount = 0.05;

        public CustomersService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public bool Exists(int id)
        {
            return db.Customers.Any(c => c.Id == id);
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order, string search)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(b => b.BirthDate).ThenBy(y => y.IsYoungDriver);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .Where(c => c.Name.ToLower().Contains(search.ToLower()))
                        .OrderByDescending(b => b.BirthDate).ThenBy(y => y.IsYoungDriver);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid Order directions {order}");
            }


            return customersQuery
                .Select(c => new CustomerModel()
                {
                    Id = c.Id,
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

        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            var newCustomer = new Customer()
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            db.Customers.Add(newCustomer);
            db.SaveChanges();

        }

        public CustomerModel ById(int id)
        {
            return db.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                     Id = c.Id,
                     Name = c.Name,
                     BirthDate = c.BirthDate,
                     IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();
        }

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return;
            }

            customer.Name = name;
            customer.BirthDate = birthDate;
            customer.IsYoungDriver = isYoungDriver;

            db.SaveChanges();

        }

        public IEnumerable<CustomerBasicModel> AllCustomers()
        {
            return db.Customers
                 .Select(c => new CustomerBasicModel
                 {
                     Id = c.Id,
                     Name = c.Name
                 })
                 .ToList();
        }
    }
}
