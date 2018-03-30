using CarDealer.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services
{
    public interface ICarsService
    {
        IEnumerable<CarModel> ByMake(string make);
    }
}
