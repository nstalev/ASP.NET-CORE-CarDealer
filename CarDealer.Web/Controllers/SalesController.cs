
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISalesService service;

        public SalesController(ISalesService service)
        {
            this.service = service;
        }

        [Route("")]
        public IActionResult AllSales()
        {
            var allSales = service.AllSales();

            return View(allSales);
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var customerWithSales = service.ById(id);

            if (customerWithSales == null)
            {
                return NotFound();
            }

            return View(customerWithSales);
        }

        [Route("discounted")]
        public IActionResult DiscountedSales()
        {
            var DiscountedSales = service.DiscountedSales(null);

            return View(DiscountedSales);
        }

        [Route("discounted/{percent}")]
        public IActionResult DiscountedSales(int percent)
        {
            var DiscountedSales = service.DiscountedSales(percent);

            return View(DiscountedSales);
        }
    }
}