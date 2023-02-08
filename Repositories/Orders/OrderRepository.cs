using Microsoft.EntityFrameworkCore;
using DataModels;
using Context;

namespace Repositories.Orders
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly AppDbContext _context = null;

        public OrderRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Customer).ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders.Include(o => o.Customer).SingleOrDefaultAsync(o => o.Id == id);
        }
        public async Task<List<Order>> GetOrderByCustomerId(int id)
        {
            return await _context.Orders.Include(o => o.Customer).Where(o => o.CustomerId == id).ToListAsync();
        }
        public async Task<List<OrderProducts>> GetOrderProducts(int orderId)
        {
            return await _context.OrderProducts.Include(o => o.Product).Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<List<OrderProducts>> GetOrderProductsByProductId(int productId)
        {
            return await _context.OrderProducts.Include(o => o.Product).Where(o => o.ProductId == productId).ToListAsync();
        }

        public async Task<Order> Post(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Put(Order order)
        {
            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderProducts> PostOrderProduct(OrderProducts orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();
            return orderProduct;
        }
        public async Task<IEnumerable<OrderProducts>> PostOrderProducts(IEnumerable<OrderProducts> orderProducts)
        {
            var orderProductsEnumerable = orderProducts.ToList();
            for (var i = 0; i < orderProductsEnumerable.Count; i++)
            {
                orderProductsEnumerable[i] = await PostOrderProduct(orderProductsEnumerable[i]);
            }
            return orderProductsEnumerable;
        }

        public async Task<object> DeleteOrderProducts(int orderId)
        {
            var orderProducts = await GetOrderProducts(orderId);
            if (!orderProducts.Any()) return null;
            foreach (var product in orderProducts)
            {
                _context.OrderProducts.Remove(product);
            }

            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<object> DeleteOrder(int orderId)
        {
            var order = await GetOrder(orderId);
            if (order != null)
            {
                await DeleteOrderProducts(orderId);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return null;
        }
    }
}
