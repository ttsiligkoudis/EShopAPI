using DataModels;

namespace Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task<List<Order>> GetOrderByCustomerId(int id);
        Task<List<OrderProducts>> GetOrderProducts(int orderId);
        Task<List<OrderProducts>> GetOrderProductsByProductId(int productId);
        Task<Order> Post(Order order);
        Task<Order> Put(Order order);
        Task<OrderProducts> PostOrderProduct(OrderProducts orderProduct);
        Task<IEnumerable<OrderProducts>> PostOrderProducts(IEnumerable<OrderProducts> orderProducts);
        Task<object> DeleteOrderProducts(int orderId);
        Task<object> DeleteOrder(int orderId);
    }
}
