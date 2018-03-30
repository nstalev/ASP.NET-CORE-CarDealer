using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Services;
using CarDealer.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService service;

        public CustomersController(ICustomersService service)
        {
            this.service = service;
        }


        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var result = this.service.OrderedCustomers(orderDirection);

            return View(result);
        }
    }
}