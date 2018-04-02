using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarDealer.Services;
using CarDealer.Web.Models.PartsViewModels;

namespace CarDealer.Web.Controllers
{
    public class PartsController : Controller
    {
        private readonly IPartsService service;
        private const int pageSize = 25;

        public PartsController(IPartsService service)
        {
            this.service = service;
        }

        public IActionResult All(int page = 1)
        {

            var allParts = new PartsListViewModel
            {
                Parts = service.AllParts(page, pageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.service.Total() / (double)pageSize)
            };

            return View(allParts);
        }
    }
}