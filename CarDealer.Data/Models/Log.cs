using System;

namespace CarDealer.Data.Models
{
    public class Log
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Operation { get; set; }

        public string  ModifiedTable { get; set; }

        public DateTime Date { get; set; }

    }
}
