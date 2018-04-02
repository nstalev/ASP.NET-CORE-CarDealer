

namespace CarDealer.Web.Models.PartsViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PartsFormModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }
    }
}
