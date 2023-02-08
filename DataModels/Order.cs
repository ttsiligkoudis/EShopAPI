
namespace DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public float FinalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Completed { get; set; }
    }
}