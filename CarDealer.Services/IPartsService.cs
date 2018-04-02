

namespace CarDealer.Services
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public interface IPartsService
    {
        IEnumerable<PartsListModel> AllParts(int page, int pageSize);

        void Create(string name, decimal price, int quantity, int supplierId);

        int Total();
    }
}
