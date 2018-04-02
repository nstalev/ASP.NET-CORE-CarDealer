
namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public List<PartCar> Parts { get; set; } = new List<PartCar>();

        public List<Sale> Sales { get; set; } = new List<Sale>();

    }
}
