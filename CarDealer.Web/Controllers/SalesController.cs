
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
            var currentSale = service.ById(id);

            return View(currentSale);
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