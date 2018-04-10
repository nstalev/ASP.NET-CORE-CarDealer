
namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using CarDealer.Services.Models.Logs;

    public interface ILogService
    {
        void Add(string userId, string operation, string modifiedTable, DateTime Date);

        IEnumerable<LogModel> All(string search, int page, int pageSize);

        int Total();
    }
}
