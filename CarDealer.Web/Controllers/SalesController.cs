
namespace CarDealer.Web.Controllers
{
    using CarDealer.Data.Models;
    using CarDealer.Services;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Web.Models.SalesViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ISalesService sales;
        private readonly ICustomersService customers;
        private readonly ICarsService cars;
        private readonly ILogService logs;

        public SalesController(UserManager<User> userManager, 
                               ISalesService sales,
                               ICustomersService customers,
                               ICarsService cars,
                               ILogService logs)
        {
            _userManager = userManager;
            this.sales = sales;
            this.customers = customers;
            this.cars = cars;
            this.logs = logs;
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

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {

            return View(new SaleFormModel
            {
                Cars = this.GetCars(),
                Customers = this.GetCustomers(),
                Discount = 0
            });
        }

        [Authorize]
        [Route(nameof(ReviewCreate))]
        public IActionResult ReviewCreate(SaleFormModel saleModel)
        {
            if (!ModelState.IsValid)
            {
                saleModel.Cars = this.GetCars();
                saleModel.Customers = this.GetCustomers();

                return View(nameof(Create), saleModel);
            }

            SaleReviewModel saleReviewModel = this.sales.SaleReview(
                saleModel.CarId,
                saleModel.CustomerId,
                saleModel.Discount
                );

            return View(saleReviewModel);
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(FinalizeCreate))]
        public IActionResult FinalizeCreate(SaleReviewModel saleModel)
        {

            if (!ModelState.IsValid)
            {
                var createModel = new SaleFormModel
                {
                    Cars = this.GetCars(),
                    Customers = this.GetCustomers(),
                };
                return View(nameof(Create), createModel);
            }

            this.sales.CreateNewSale(
                saleModel.CarId,
                saleModel.CustomerId,
                saleModel.Discount
                );

            var userId = _userManager.GetUserId(HttpContext.User);
            this.logs.Add(userId, "Add", "Sale", DateTime.Now);

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