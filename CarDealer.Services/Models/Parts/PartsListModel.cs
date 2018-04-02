namespace CarDealer.Services.Models.Parts
{
    public class PartsListModel: PartModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string Supplier { get; set; }
    }
}
