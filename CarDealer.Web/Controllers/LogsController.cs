using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Services;
using CarDealer.Web.Models.LogsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Web.Controllers
{
    public class LogsController : Controller
    {
        private const int pageSize = 5;
        private readonly ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }


        public IActionResult All(int page = 1)
        {
            var result  = this.logs.All(page, pageSize);

            return View(new LogsListViewModel
            {
                AllLogs = this.logs.All(page, pageSize),
                CurrentPage= page,
                TotalPages = (int)Math.Ceiling(this.logs.Total() / (double)pageSize)
            });
        }
    }
}