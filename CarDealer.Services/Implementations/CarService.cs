
namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Parts;

    public class CarService : ICarsService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<CarModel> ByMake(string make)
        {
            return this.db.Cars
               .Where(c => c.Make == make)
               .OrderBy(c => c.Model)
               .ThenByDescending(c => c.TravelledDistance)
               .Select(car => new CarModel()
               {
                   Make = car.Make,
                   Model = car.Model,
                   TravelledDistance = car.TravelledDistance
               }).ToList();
        }



        public IEnumerable<CarWithPartsModel> CarsWithParts(int page, int pageSize)
        {
            var result =  this.db.Cars
                .OrderBy(c => c.Make)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    }).ToList()

                }).ToList();


            return result;
        }

        public int Total()
        {
            return this.db.Cars.Count();
        }
    }
}
