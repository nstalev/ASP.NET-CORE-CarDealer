

namespace CarDealer.Services
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public interface IPartsService
    {
        IEnumerable<PartsListModel> AllParts(int page, int pageSize);

        PartModel ById(int id);

        void Create(string name, decimal price, int quantity, int supplierId);

        void Edit(int id, decimal price, int quantity);

        int Total();

        bool Exists(int id);

    }
}
