
namespace CarDealer.Services.Implementations
{

    using System;
    using System.Linq;
    using CarDealer.Data;
    using System.Collections.Generic;
    using CarDealer.Services.Models.Suppliers;

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

        public IEnumerable<SuppliersListModel> AllSuppliers()
        {
            return db.Suppliers
                .Select(s => new SuppliersListModel
                {
                    Id = s.Id,
                    Name= s.Name
                });
        }
    }
}
