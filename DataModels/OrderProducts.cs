namespace DataModels
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }

        public OrderProducts()
        {

        }

        public OrderProducts(int orderId, int productId, decimal productPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Price = productPrice;
        }
    }
}