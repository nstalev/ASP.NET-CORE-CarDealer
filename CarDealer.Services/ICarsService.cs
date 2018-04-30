namespace CarDealer.Services
{
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;

    public interface ICarsService
    {
        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarBasicModel> AllBasicCars();

        int Total();

        IEnumerable<CarWithPartsModel> CarsWithParts(int page, int pageSize);

        void Create(string make, string model, long travelledDistance, IEnumerable<int> PartsIds);
    }
}
