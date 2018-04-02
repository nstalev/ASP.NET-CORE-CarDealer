
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISuppliersService
    {
        IEnumerable<SupplierModel> Suppliers(bool isImporter);

        IEnumerable<SuppliersListModel> AllSuppliers();
    }
}
