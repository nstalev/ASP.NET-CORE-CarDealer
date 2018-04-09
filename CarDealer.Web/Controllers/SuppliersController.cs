
namespace CarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using CarDealer.Services;
    using CarDealer.Services.Models.Suppliers;
    using CarDealer.Web.Models.SuppliersViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {

        private readonly ISuppliersService service;

        public SuppliersController(ISuppliersService service)
        {
            this.service = service;
        }


        public IActionResult Local()
        {
            return View("SuppliersView", GetSupplierModel(false));
        }

        public IActionResult Importers()
        {
            return View("SuppliersView", GetSupplierModel(true));
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(SupplierFormModel supplierModel)
        {

            if (!ModelState.IsValid)
            {
                return View(supplierModel);
            }

            //Create new Suppliers

            return RedirectToAction(nameof(Local));
        }



        private IEnumerable<SupplierModel> GetSupplierModel(bool isImporter)
        {
            return service.Suppliers(isImporter);
        }





    }
}