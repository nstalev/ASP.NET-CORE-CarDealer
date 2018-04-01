namespace CarDealer.Services.Models.Cars
{
    using System.Collections.Generic;
    using CarDealer.Services.Models;

    public class CarWithPartsModel: CarModel
    {

        public IEnumerable<PartModel> Parts { get; set; }

    }
}
