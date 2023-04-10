
namespace DataModels
{
    public class ProductRates
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal Rate { get; set; }
        public string? Comment { get; set; }
    }
}
