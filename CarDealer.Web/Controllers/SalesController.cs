
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    public class SalesController : Controller
    {
        private readonly ISalesService service;

        public SalesController(ISalesService service)
        {
            this.service = service;
        }

        [Route("sales")]
        public IActionResult AllSales()
        {
            var allSales = service.AllSales();

            return View(allSales);
        }

        [Route("sales/discounted")]
        public IActionResult DiscountedSales()
        {
            var DiscountedSales = service.DiscountedSales(null);

            return View(DiscountedSales);
        }

        [Route("sales/discounted/{percent}")]
        public IActionResult DiscountedSales(int percent)
        {
            var DiscountedSales = service.DiscountedSales(percent);

            return View(DiscountedSales);
        }
    }
}