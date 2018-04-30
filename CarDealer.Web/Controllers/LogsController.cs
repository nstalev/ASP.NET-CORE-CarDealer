
namespace CarDealer.Web.Controllers
{
    using System;
    using CarDealer.Services;
    using CarDealer.Web.Models.LogsViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LogsController : Controller
    {
        private const int pageSize = 5;
        private readonly ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }

        [Authorize]
        public IActionResult All(string search ="", int page = 1)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }

            int totalLogs = this.logs.Total(search);

            var totalPages = (int)Math.Ceiling(totalLogs / (double)pageSize);

            page = Math.Max(page, 1);
            page = Math.Min(page, totalPages);

            return View(new LogsListViewModel
            {
                AllLogs = this.logs.All(search, page, pageSize),
                CurrentPage = page,
                Search = search,
                TotalPages = totalPages
            });
        }


        [Authorize]
        public IActionResult Clear()
        {
            this.logs.Clear();

            return RedirectToAction(nameof(All),
                new { search = string.Empty, page = 1 });
        }
    }
}