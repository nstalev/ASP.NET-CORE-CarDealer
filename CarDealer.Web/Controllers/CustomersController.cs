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


        public IActionResult All(string order, string search)
        {

            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }

            var customers = this.customers.OrderedCustomers(orderDirection, search);

            var customersVM = new AllCustomersListViewModel()
            {
                Customers = customers,
                Search = search
            };


            return View(customersVM);
        }


        [Route("customers/{id:int}")]
        public IActionResult CustomersAndSales(int id)
        {
            var result = customers.CustomerAndSales(id);

            return View(result);
        }

        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View( new CustomerFormModel());
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(customerModel);
            }

            customers.Create(
               customerModel.Name,
               customerModel.BirthDate,
               customerModel.IsYoungDriver
                );

            return RedirectToAction(nameof(All), new { order = "ascending" });
        }


        [Route("customers/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var customerExists = customers.Exists(id);

            if (!customerExists)
            {
                return RedirectToAction(nameof(All), new { order = "ascending" });
            }

            var customer = this.customers.ById(id);

            var vm = new CustomerFormModel()
            {
                Id = customer.Id,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            };

            return View(vm);
        }

        [HttpPost]
        [Route("customers/edit/{id}")]
        public IActionResult Edit(int id, CustomerFormModel customerModel)
        {
            var customerExists = customers.Exists(id);

            if (!customerExists)
            {
                return RedirectToAction(nameof(All), new { order = "ascending" });
            }

            if (!ModelState.IsValid)
            {
                return View(customerModel);
            }

            customers.Edit(customerModel.Id, customerModel.Name, customerModel.BirthDate, customerModel.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = "ascending" });
        }

    }
}