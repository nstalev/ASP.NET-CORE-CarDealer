
namespace CarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using CarDealer.Services;
    using CarDealer.Services.Models;
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


        private IEnumerable<SupplierModel> GetSupplierModel(bool isImporter)
        {
            return service.Suppliers(isImporter);
        }
    }
}