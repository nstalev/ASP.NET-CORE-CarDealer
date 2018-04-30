

namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models.Logs;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LogService : ILogService
    {

        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }


        public void Add(string userId, string operation, string modifiedTable, DateTime date)
        {

            var log = new Log()
            {
                UserId = userId,
                Operation = operation,
                ModifiedTable = modifiedTable,
                Date = date
            };

            db.Logs.Add(log);
            db.SaveChanges();

        }

        public IEnumerable<LogModel> All(string search, int page, int pageSize)
        {

            return GetLogsAsQuerable(search)
                .OrderByDescending(l => l.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new LogModel
                {
                    ModifiedTable = l.ModifiedTable,
                    Operation = l.Operation,
                    Time = l.Date,
                    UserName = l.User.UserName

                })
                .ToList();
        }

        public void Clear()
        {
            var sqlQuery = $"DELETE FROM {nameof(Log)}s";
            this.db.Database.ExecuteSqlCommand(sqlQuery);
            this.db.SaveChanges();
        }

        public int Total(string search)
        {
            return this.db.Logs
               .Select(l => l.User)
                .Where(u => u.UserName.ToLower().Contains(search.ToLower()))
               .Count();
        }




        private IEnumerable<Log> GetLogsAsQuerable(string search)
        {
            var logsAsQuerable = db.Logs.Include(l => l.User).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                logsAsQuerable = logsAsQuerable
                                    .Where(l => l.User.UserName.ToLower().Contains(search.ToLower()));
            }

            return logsAsQuerable.ToList();

        }
    }
}
