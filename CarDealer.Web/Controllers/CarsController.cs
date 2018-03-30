

namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarsService cars;


        public CarsController(ICarsService cars)
        {
            this.cars = cars;
        }


        [Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {

            var result = cars.ByMake(make);

            return View(result);
        }
    }
}