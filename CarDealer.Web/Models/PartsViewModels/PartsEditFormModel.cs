
namespace CarDealer.Web.Models.PartsViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PartsEditFormModel
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
