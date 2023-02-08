namespace DataModels
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public float ProductPrice { get; set; }

        public OrderProducts()
        {

        }

        public OrderProducts(int orderId, int productId, float productPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductPrice = productPrice;
        }
    }
}