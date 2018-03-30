
namespace CarDealer.Services.Implementations
{

    using System;
    using System.Linq;
    using CarDealer.Data;
    using System.Collections.Generic;
    using CarDealer.Services.Models;

    public class SuppliersService : ISuppliersService
    {
        private readonly CarDealerDbContext db;


        public SuppliersService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<SupplierModel> Suppliers(bool isImporter)
        {
            return db.Suppliers
             .Where(s => s.IsImporter == isImporter)
             .Select(s => new SupplierModel()
             {
                 Id = s.Id,
                 Name = s.Name,
                 NunOfParts = s.Parts.Count()

             }).ToList();

        }
    }
}
