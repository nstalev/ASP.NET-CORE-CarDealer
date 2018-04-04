
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.CarsViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarsService cars;
        private readonly IPartsService parts;
        private const int pageSize = 25;

        public CarsController(ICarsService cars,
                              IPartsService parts)
        {
            this.cars = cars;
            this.parts = parts;
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
            return View(new CarFormModel
            {
                Parts = GetParts()
            });
        }

        [HttpPost]
        [Route("create", Order = 1)]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.Parts = GetParts();
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance,
                carModel.PartsIds);

            return RedirectToAction(nameof(CarsWithParts));
        }


        private IEnumerable<SelectListItem> GetParts()
         =>    this.parts.BasicParts()
                 .OrderBy(p => p.Name)
                 .Select(p => new SelectListItem
                 {
                     Text = p.Name,
                     Value = p.Id.ToString()
                 });

              
              

    }
}