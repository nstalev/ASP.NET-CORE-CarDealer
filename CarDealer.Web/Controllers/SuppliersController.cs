
namespace CarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using CarDealer.Data.Models;
    using CarDealer.Services;
    using CarDealer.Services.Models.Suppliers;
    using CarDealer.Web.Models.SuppliersViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    public class SuppliersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ISuppliersService suppliers;
        private readonly ILogService logs;

        public SuppliersController(UserManager<User> userManager,
                                    ISuppliersService suppliers,
                                    ILogService logs)
        {
            _userManager = userManager;
            this.suppliers = suppliers;
            this.logs = logs;
        }




        public IActionResult Local()
        {
            return View("SuppliersView", GetSupplierModel(false));
        }

        public IActionResult Importers()
        {
            return View("SuppliersView", GetSupplierModel(true));
        }


        [Authorize]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create(SupplierFormModel supplierModel)
        {

            if (!ModelState.IsValid)
            {
                return View(supplierModel);
            }

            this.suppliers.Create(supplierModel.Name, supplierModel.IsImporter);

            var userId = this.GetUserId();
            var date = DateTime.Now;

            this.logs.Add(userId, "Add", "Suppliers", date);

            string suppType = supplierModel.IsImporter ? "Importers" : "Local";

            return RedirectToAction(suppType);
        }

        [Authorize]
        [Route("Suppliers/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var supplierExists = suppliers.Exists(id);

            if (!supplierExists)
            {
                return RedirectToAction(nameof(Local));
            }

            SupplierEditModel supplier = this.suppliers.ById(id);

            return View(new SupplierFormModel
            {
                Name = supplier.Name,
                IsImporter = supplier.IsImporter
            });
        }

        [Authorize]
        [HttpPost]
        [Route("Suppliers/edit/{id}")]
        public IActionResult Edit(int id, SupplierFormModel supplierModel)
        {
            var supplierExists = suppliers.Exists(id);

            if (!supplierExists)
            {
                return RedirectToAction(nameof(Local));
            }

            if (!ModelState.IsValid)
            {
                return View(supplierModel);
            }

            this.suppliers.Edit(id, supplierModel.Name, supplierModel.IsImporter);
            var userId = this.GetUserId();
            var date = DateTime.Now;
            this.logs.Add(userId, "Edit", "Suppliers", date);


            string suppType = supplierModel.IsImporter ? "Importers" : "Local";

            return RedirectToAction(suppType);
        }

        [Authorize]
        [Route("Suppliers/delete/{id}")]
        public IActionResult Delete(int id)
        {
            return View(id);
        }


        [Authorize]
        [Route("Suppliers/confirmDelete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            this.suppliers.Delete(id);

            string userId = this.GetUserId();
            var date = DateTime.Now;
            this.logs.Add(userId, "Delete", "Suppliers", date);

            return RedirectToAction(nameof(Local));
        }



        private IEnumerable<SupplierModel> GetSupplierModel(bool isImporter)
        {
            return suppliers.Suppliers(isImporter);
        }


        public string GetUserId()
         =>_userManager.GetUserId(HttpContext.User);






    }
}