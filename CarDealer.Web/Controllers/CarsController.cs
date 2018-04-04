

namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.CarsViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarsService cars;
        private const int pageSize = 25;

        public CarsController(ICarsService cars)
        {
            this.cars = cars;
        }


        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {

            var result = cars.ByMake(make);

            return View(result);
        }

       

        [Route("parts", Order = 1)]
        public IActionResult CarsWithParts(int page = 1)
        {

            return View(new CarsListModel()
            {
                Cars = cars.CarsWithParts(page, pageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.cars.Total() / (double)pageSize)

            });
        }

        [Route("create", Order = 1)]
        public IActionResult Create()
        {
            return View( new CarFormModel());
        }

        [HttpPost]
        [Route("create", Order = 1)]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance);

            return RedirectToAction(nameof(CarsWithParts));
        }

    }
}