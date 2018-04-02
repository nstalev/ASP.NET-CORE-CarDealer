namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models.Customers;
    using CarDealer.Web.Models.CustomerViewModel;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : Controller
    {
        private readonly ICustomersService customers;

        public CustomersController(ICustomersService customers)
        {
            this.customers = customers;
        }


        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var result = this.customers.OrderedCustomers(orderDirection);

            return View(result);
        }


        [Route("customers/{id:int}")]
        public IActionResult CustomersAndSales(int id)
        {
            var result = customers.CustomerAndSales(id);

            return View(result);
        }


        public IActionResult Create()
        {
            return View( new CustomerFormModel());
        }

        [HttpPost]
        public IActionResult Create(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            customers.Create(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver
                );

            return RedirectToAction(nameof(All), new { order = "ascending" });
        }
    }
}