
namespace CarDealer.Services.Implementations
{
    using System;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using System.Collections.Generic;
    using CarDealer.Services.Models.Suppliers;
    using Microsoft.EntityFrameworkCore;

    public class SuppliersService : ISuppliersService
    {
        private readonly CarDealerDbContext db;


        public SuppliersService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public bool Exists(int id)
        {
            return db.Suppliers.Any(s => s.Id == id);
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

        public void Create(string name, bool isImporter)
        {
            var supplier = new Supplier()
            {
                Name = name,
                IsImporter = isImporter
            };

            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }

        public SupplierEditModel ById(int id)
        {
            return this.db
                .Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierEditModel
                {
                    Name= s.Name,
                    IsImporter= s.IsImporter
                })
                .FirstOrDefault();
        }

        public void Edit(int id, string name, bool isImporter)
        {
           var supplier =  db.Suppliers.Find(id);

            supplier.Name = name;
            supplier.IsImporter = isImporter;

            db.SaveChanges();
        }




        public void Delete(int id)
        {
            var supplier = this.db.Suppliers.Find(id);

            var supplierWithParts = this.db.
                       Suppliers.Include(s => s.Parts).ToList()
                         .Where(s => s.Id == id);

            if (supplier == null)
            {
                return;
            }

            var parts = supplier.Parts;

            db.Parts.RemoveRange(parts);
            db.Suppliers.Remove(supplier);

            db.SaveChanges();

        }
    }
}
