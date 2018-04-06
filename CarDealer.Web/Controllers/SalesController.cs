
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.SalesViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISalesService sales;
        private readonly ICustomersService customers;
        private readonly ICarsService cars;

        public SalesController(ISalesService sales,
                               ICustomersService customers,
                               ICarsService cars)
        {
            this.sales = sales;
            this.customers = customers;
            this.cars = cars;
        }


        [Route("")]
        public IActionResult AllSales()
        {
            var allSales = sales.AllSales();

            return View(allSales);
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var customerWithSales = sales.ById(id);

            if (customerWithSales == null)
            {
                return NotFound();
            }

            return View(customerWithSales);
        }

        [Route("discounted")]
        public IActionResult DiscountedSales()
        {
            var DiscountedSales = sales.DiscountedSales(null);

            return View(DiscountedSales);
        }

        [Route("discounted/{percent}")]
        public IActionResult DiscountedSales(int percent)
        {
            var DiscountedSales = sales.DiscountedSales(percent);

            return View(DiscountedSales);
        }

        [Route("AddSale")]
        public IActionResult AddSale()
        {

            return View(new SaleFormModel
            {
                Cars = this.GetCars(),
                Customers = this.GetCustomers(),
                Discount = 0
            });
        }

        [Route("AddSale")]
        [HttpPost]
        public IActionResult AddSale(SaleFormModel saleModel)
        {

            if (!ModelState.IsValid)
            {
                saleModel.Cars = this.GetCars();
                saleModel.Customers = this.GetCustomers();

                return View(saleModel);
            }

            return RedirectToAction(nameof(AllSales));
           
        }




        private IEnumerable<SelectListItem> GetCustomers()
            => this.customers.AllCustomers()
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            .ToList();



        private IEnumerable<SelectListItem> GetCars()
            => this.cars.AllBasicCars()
                .OrderBy(c => c.Make)
                .Select(c => new SelectListItem
                {
                    Text = $"{c.Make} {c.Model}",
                    Value = c.Id.ToString()
                })
            .ToList();
    }
}