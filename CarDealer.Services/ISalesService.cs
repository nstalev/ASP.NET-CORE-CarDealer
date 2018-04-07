

namespace CarDealer.Services
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;

    public interface ISalesService
    {
        IEnumerable<SaleModel> AllSales();

        IEnumerable<SaleModel> DiscountedSales(int? discountPercentage);

        SaleModel ById(int id);

        SaleReviewModel SaleReview(int carId, int customerId, int discount);

        void CreateNewSale(int carId, int customerId, double discount);
    }
}
