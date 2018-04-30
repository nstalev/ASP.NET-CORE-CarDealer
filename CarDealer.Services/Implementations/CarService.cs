
namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Parts;

    public class CarService : ICarsService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public int Total()
        {
            return this.db.Cars.Count();
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

        public void Create(string make, string model, long travelledDistance, IEnumerable<int> PartsIds)
        {
            var car = new Car()
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };


            foreach (var partId in PartsIds)
            {
                var part = db.Parts.Find(partId);
                car.Parts.Add(new PartCar { PartId = partId });
            }

            this.db.Cars.Add(car);
            this.db.SaveChanges();

        }

        public IEnumerable<CarBasicModel> AllBasicCars()
        {
            return db.Cars
                .Select(c => new CarBasicModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model
                })
                .ToList();
        }
    }
}
