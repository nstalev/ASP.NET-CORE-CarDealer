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


        public IEnumerable<PartsListModel> AllParts(string search, int page, int pageSize)
        {
            return db
                .Parts
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
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

        public IEnumerable<PartBasicModel> BasicParts()
        {
            return db.Parts
                .Select(p => new PartBasicModel
                {
                    Id = p.Id,
                    Name = p.Name
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
     

        public int Total(string search)
        {
            return this.db.Parts
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .Count();
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

            if (part == null)
            {
                return;
            }

            part.Price = price;
            part.Quantity = quantity;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var part = db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            db.Parts.Remove(part);
            db.SaveChanges();
        }
        
        public IEnumerable<string> GetNames(string term)
        {
            return this.db.Parts
                .Where(p => p.Name.ToLower().StartsWith(term.ToLower()))
                .Select(p => p.Name).ToList();
        }
    }
}
