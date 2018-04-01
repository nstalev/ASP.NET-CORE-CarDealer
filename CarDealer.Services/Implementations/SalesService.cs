
namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Data;
    using System.Linq;
    using System;

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
    }
}
