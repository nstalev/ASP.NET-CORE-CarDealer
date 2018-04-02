
namespace CarDealer.Web.Models.CustomerViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
