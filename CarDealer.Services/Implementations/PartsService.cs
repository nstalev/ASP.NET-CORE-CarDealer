namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Data;
    using System.Linq;
    using CarDealer.Data.Models;

    public class PartsService : IPartsService
    {

        private readonly CarDealerDbContext db;

        public PartsService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public bool Exists(int id)
        {
            return db.Parts.Any(p => p.Id == id);
        }


        public IEnumerable<PartsListModel> AllParts(int page, int pageSize)
        {
            return db
                .Parts
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartsListModel
                {
                    Id= p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity  = p.Quantity,
                    Supplier = p.Supplier.Name
                })
                .ToList();
        }

        public void Create(string name, decimal price, int quantity, int supplierId)
        {
            var newPart = new Part()
            {
                Name = name,
                Price = price,
                Quantity = quantity > 0 ? quantity : 1,
                SupplierId = supplierId
            };

            db.Parts.Add(newPart);
            db.SaveChanges();

        }

     

        public int Total()
        {
            return this.db.Parts.Count();
        }

        public PartModel ById(int id)
        {
            return db.Parts
                .Where(p => p.Id == id)
                .Select(p => new PartModel
                {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       Quantity = p.Quantity
                })
                .FirstOrDefault();
        }

        public void Edit(int id, decimal price, int quantity)
        {
            var part = db.Parts.Find(id);

            part.Price = price;
            part.Quantity = quantity;

            db.SaveChanges();
        }
    }
}
