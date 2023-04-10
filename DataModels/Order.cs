
namespace DataModels
{
    public class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProducts>();
        }

        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Completed { get; set; }
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }
    }
}