
namespace CarDealer.Services.Models.Logs
{
    using System;

    public class LogModel
    {
        public string UserName { get; set; }

        public string Operation { get; set; }

        public string ModifiedTable { get; set; }

        public DateTime Time { get; set; }
    }
}
