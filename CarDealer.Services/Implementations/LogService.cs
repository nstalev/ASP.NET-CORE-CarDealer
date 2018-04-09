
using CarDealer.Data;
using CarDealer.Data.Models;
using CarDealer.Services.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Services.Implementations
{
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

        public IEnumerable<LogModel> All(int page, int pageSize)
        {
            return db.Logs
                .OrderByDescending(l => l.ModifiedTable)
                .Skip((page -1) * pageSize)
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

        public int Total()
        {
            return this.db.Logs.Count();
        }
    }
}
