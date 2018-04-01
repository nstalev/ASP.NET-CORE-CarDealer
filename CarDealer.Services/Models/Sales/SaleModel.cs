using CarDealer.Data.Models;
using CarDealer.Services.Models.Cars;

namespace CarDealer.Services.Models.Sales
{
    public class SaleModel : CarModel
    {

        public int Id { get; set; }

        public string Customer { get; set; }


        public decimal Price { get; set; }

        public double Discount { get; set; }

        public decimal PriceWithDiscount
              => this.Price * (decimal)(1 - this.Discount);

    }
}
