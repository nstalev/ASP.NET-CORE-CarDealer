
namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using CarDealer.Services;
    using CarDealer.Web.Models.PartsViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PartsController : Controller
    {
        private readonly IPartsService parts;
        private readonly ISuppliersService suppliers;
        private const int pageSize = 25;

        public PartsController(IPartsService parts,
                               ISuppliersService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult All(int page = 1)
        {

            var allParts = new PartsListViewModel
            {
                Parts = parts.AllParts(page, pageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)pageSize)
            };

            return View(allParts);
        }


        public IActionResult Create()
        {

            return View(new PartsFormModel
            {
                Suppliers = GetSuppliers()
            });
        }

        [HttpPost]
        public IActionResult Create(PartsFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Suppliers = GetSuppliers();
                return View(model);
            }

            this.parts.Create(
                model.Name,
                model.Price,
                model.Quantity,
                model.SupplierId
                );



          return RedirectToAction(nameof(All));
        }

        [Route("parts/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var partExists = this.parts.Exists(id);
            if (!partExists)
            {
                return RedirectToAction(nameof(All));
            }

            var part = this.parts.ById(id);


            return View(new PartsEditFormModel()
            {
                 Id = part.Id,
                 Name = part.Name,
                 Price= part.Price,
                 Quantity = part.Quantity
            });
        }

        [Route("parts/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(int id, PartsEditFormModel model)
        {
            var partExists = this.parts.Exists(id);
            if (!partExists)
            {
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.parts.Edit(model.Id, model.Price, model.Quantity);

            return RedirectToAction(nameof(All));
        }





        private IEnumerable<SelectListItem> GetSuppliers()
        => this.suppliers
                .AllSuppliers()
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
        
    }
}