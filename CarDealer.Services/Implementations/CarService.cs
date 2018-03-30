

namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Models;

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
    }
}
