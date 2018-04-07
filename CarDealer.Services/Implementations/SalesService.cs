
namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Data;
    using System.Linq;
    using System;
    using CarDealer.Data.Models;

    public class SalesService : ISalesService
    {
        private readonly CarDealerDbContext db;

        public SalesService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<SaleModel> AllSales()
        {
            return db.Sales
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Customer = s.Customer.Name,
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    Discount = Math.Min(1,
                            s.Discount + (s.Customer.IsYoungDriver ? 0.05 : 0)),

                }).ToList();


        }

      

        public IEnumerable<SaleModel> DiscountedSales(int? discountPercentage)
        {
            if (discountPercentage == null)
            {
                return db.Sales
                  .Where(s => s.Discount > 0 || s.Customer.IsYoungDriver)
                  .Select(s => new SaleModel
                  {
                      Id = s.Id,
                      Make = s.Car.Make,
                      Model = s.Car.Model,
                      TravelledDistance = s.Car.TravelledDistance,
                      Customer = s.Customer.Name,
                      Price = s.Car.Parts.Sum(p => p.Part.Price),
                      Discount = Math.Min(1,
                              s.Discount + (s.Customer.IsYoungDriver ? 0.05 : 0)),
                  }).ToList();
            }
            else
            {
             return db.Sales
                  .Where(s => discountPercentage / (double)100 == Math.Min(1,
                              s.Discount + (s.Customer.IsYoungDriver ? 0.05 : 0)))
                  .Select(s => new SaleModel
                  {
                      Id = s.Id,
                      Make = s.Car.Make,
                      Model = s.Car.Model,
                      TravelledDistance = s.Car.TravelledDistance,
                      Customer = s.Customer.Name,
                      Price = s.Car.Parts.Sum(p => p.Part.Price),
                      Discount = Math.Min(1,
                              s.Discount + (s.Customer.IsYoungDriver ? 0.05 : 0)),
                  }).ToList();
            }

        }



        public SaleModel ById(int id)
        {
            return db.Sales
                .Where(s => s.Id == id)
                .Select(s => new SaleModel
                {
                    Id = s.Id,
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Customer = s.Customer.Name,
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    Discount = Math.Min(1,
                              s.Discount + (s.Customer.IsYoungDriver ? 0.05 : 0)),
                }).FirstOrDefault();
        }


        public SaleReviewModel SaleReview(int carId, int customerId, int discount)
        {
            var car = db.Cars
                .Where(c => c.Id == carId)
                .Select(c => new
                {
                    Name = $"{c.Make} {c.Model}",
                    Price = c.Parts.Sum(p => p.Part.Price)
                })
                .FirstOrDefault();


            var customer = db.Customers.Find(customerId);
            
            int discountPercentage = discount;
            string discoutnView = $"{discount.ToString()}%";
            

            if (customer.IsYoungDriver)
            {
                discountPercentage += 5;
                discoutnView += " (5%)";
            }

            decimal discountFloat = ((decimal)1 - (decimal)(discountPercentage / (decimal)100));
            decimal discountNum = ( (decimal)(discountPercentage / (decimal)100));

            return new SaleReviewModel
            {
                Car = car.Name,
                CarId = carId,
                Customer = customer.Name,
                CustomerId = customerId,
                DiscountVeiw = discoutnView,
                CarPrice = car.Price,
                FinalCarPrice = (decimal)car.Price * discountFloat,
                Discount = (double)discountNum
            };
        }

        public void CreateNewSale(int carId, int customerId, double discount)
        {

            var sale = new Sale()
            { 
                CarId = carId,
                CustomerId = customerId,
                Discount = discount
            };

            db.Sales.Add(sale);
            db.SaveChanges();

        }
    }
}
