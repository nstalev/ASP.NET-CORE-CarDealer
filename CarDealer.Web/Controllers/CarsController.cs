

namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;



    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarsService service;


        public CarsController(ICarsService service)
        {
            this.service = service;
        }


        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {

            var result = service.ByMake(make);

            return View(result);
        }

       

        [Route("parts", Order = 1)]
        public IActionResult CarsWithParts()
        {
            var result = service.CarsWithParts();

            return View(result);
        }


    }
}