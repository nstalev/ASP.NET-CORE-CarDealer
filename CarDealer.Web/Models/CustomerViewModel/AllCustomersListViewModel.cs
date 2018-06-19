using CarDealer.Services.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.CustomerViewModel
{
    public class AllCustomersListViewModel
    {

        public IEnumerable<CustomerModel> Customers { get; set; }

        public string Search { get; set;  }
    }
}
