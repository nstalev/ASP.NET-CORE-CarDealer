using System;
using System.Collections.Generic;
using System.Text;
using CarDealer.Services.Models.Parts;
using CarDealer.Data;
using System.Linq;

namespace CarDealer.Services.Implementations
{
    public class PartsService : IPartsService
    {

        private readonly CarDealerDbContext db;

        public PartsService(CarDealerDbContext db)
        {
            this.db = db;
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

        public int Total()
        {
            return this.db.Parts.Count();
        }
    }
}
