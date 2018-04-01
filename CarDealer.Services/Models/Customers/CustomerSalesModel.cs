
namespace CarDealer.Services.Models.Customers
{
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;

    public class CustomerSalesModel
    {
        public string Name { get; set; }

        public int BoughtCars { get; set; }

        public decimal TotalSpentMoney { get; set; }

    }
}
