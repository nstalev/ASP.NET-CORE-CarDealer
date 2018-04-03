
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
        public IActionResult Create(PartsFormModel partModel)
        {
            if (!ModelState.IsValid)
            {
                partModel.Suppliers = GetSuppliers();
                return View(partModel);
            }

            this.parts.Create(
                partModel.Name,
                partModel.Price,
                partModel.Quantity,
                partModel.SupplierId
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


            return View(new PartsFormModel
            {
                 Id = part.Id,
                 Name = part.Name,
                 Price= part.Price,
                 Quantity = part.Quantity,
                 IsEdit = true
                 
            });
        }

        [Route("parts/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(int id, PartsFormModel partModel)
        {
            var partExists = this.parts.Exists(id);
            if (!partExists)
            {
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                return View(partModel);
            }

            this.parts.Edit(partModel.Id, partModel.Price, partModel.Quantity);

            return RedirectToAction(nameof(All));
        }


        [Route("parts/delete/{id}")]
        public IActionResult Delete (int id)
        {
            var part = this.parts.ById(id);
            return View(new DeletePartModel()
            {
                Id = id,
                Name = part.Name
            });
        }

        [Route("parts/delete/{id}")]
        [HttpPost]
        public IActionResult Delete(int id, DeletePartModel partModel)
        {
            if (!ModelState.IsValid)
            {
                return View(partModel);
            }

            this.parts.Delete(id);

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