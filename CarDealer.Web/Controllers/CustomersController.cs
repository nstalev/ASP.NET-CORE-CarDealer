namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models.Customers;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : Controller
    {
        private readonly ICustomersService service;

        public CustomersController(ICustomersService service)
        {
            this.service = service;
        }


        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var result = this.service.OrderedCustomers(orderDirection);

            return View(result);
        }


        [Route("customers/{id}")]
        public IActionResult CustomersAndSales(int id)
        {
            var result = service.CustomerAndSales(id);

            return View(result);
        }
    }
}