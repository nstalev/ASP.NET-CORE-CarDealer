
namespace CarDealer.Web.Models.LogsViewModels
{
    using CarDealer.Services.Models.Logs;
    using System.Collections.Generic;

    public class LogsListViewModel
    {
        public IEnumerable<LogModel> AllLogs { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
