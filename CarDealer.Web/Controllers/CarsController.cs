

namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarsService service;


        public CarsController(ICarsService service)
        {
            this.service = service;
        }


        [Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {

            var result = service.ByMake(make);

            return View(result);
        }
    }
}