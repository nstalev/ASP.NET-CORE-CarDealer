using CarDealer.Services.Models;
using System.Collections.Generic;

namespace CarDealer.Services
{
    public interface ISuppliersService
    {
        IEnumerable<SupplierModel> Suppliers(bool isImporter);
    }
}
