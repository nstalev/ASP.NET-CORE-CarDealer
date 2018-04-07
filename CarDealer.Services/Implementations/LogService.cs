
using CarDealer.Data;
using CarDealer.Data.Models;
using System;

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
    }
}
