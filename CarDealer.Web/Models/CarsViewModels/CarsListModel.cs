
namespace CarDealer.Web.Models.CarsViewModels
{
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;

    public class CarsListModel
    {
        public IEnumerable<CarWithPartsModel> Cars { get; set;}

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

    }
}
