
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISuppliersService
    {
        IEnumerable<SupplierModel> Suppliers(bool isImporter);

        IEnumerable<SuppliersListModel> AllSuppliers();

        void Create(string name, bool isImporter);

        SupplierEditModel ById(int id);

        bool Exists(int id);

        void Edit(int id, string name, bool isImporter);
    }
}
