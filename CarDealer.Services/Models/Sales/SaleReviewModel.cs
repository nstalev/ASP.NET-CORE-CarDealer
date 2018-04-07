
namespace CarDealer.Services.Models.Sales
{
    public class SaleReviewModel
    {
        public int CustomerId { get; set; }

        public string Customer { get; set; }

        public int CarId { get; set; }

        public string Car { get; set; }

        public double Discount { get; set; }

        public string DiscountVeiw { get; set; }

        public decimal CarPrice { get; set; }

        public decimal FinalCarPrice { get; set; }

    }
}
