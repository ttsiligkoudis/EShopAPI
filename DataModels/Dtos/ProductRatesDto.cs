
namespace DataModels.Dtos
{
    public class ProductRatesDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal Rate { get; set; }
        public string? Comment { get; set; }
    }
}
