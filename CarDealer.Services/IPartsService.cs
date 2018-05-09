
namespace CarDealer.Services
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public interface IPartsService
    {
        IEnumerable<PartsListModel> AllParts(string search, int page, int pageSize);

        PartModel ById(int id);

        void Create(string name, decimal price, int quantity, int supplierId);

        void Edit(int id, decimal price, int quantity);

        IEnumerable<PartBasicModel> BasicParts();

        int Total(string search);

        bool Exists(int id);
        void Delete(int id);
    }
}
