
using System;

namespace CarDealer.Services
{
    public interface ILogService
    {
        void Add(string userId, string operation, string modifiedTable, DateTime Date);
    }
}
