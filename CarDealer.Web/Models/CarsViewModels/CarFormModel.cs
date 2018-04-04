
namespace CarDealer.Web.Models.CarsViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarFormModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(0, long.MaxValue)]
        public long TravelledDistance { get; set; }

        public IEnumerable<int> PartsIds { get; set; }

        public IEnumerable<SelectListItem> Parts { get; set; }
    }
}
