using Microsoft.EntityFrameworkCore;
using Context;
using DataModels;

namespace Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly AppDbContext _context = null;

        public CustomerRepository(AppDbContext context)
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

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerByUserId(int id)
        {
            return await _context.Customers.Include(c => c.User).SingleOrDefaultAsync(c => c.UserId == id);
        }

        public async Task<Customer> Post(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Put(Customer customer)
        {
            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<object> Delete(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (customer != null)
            {
                DeleteCustomerDependencies(customer);
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public void DeleteCustomerDependencies(Customer customer)
        {
            if (customer.UserId != null)
            {
                DeleteUserDependencies((int)customer.UserId);
            }
            var customerOrders = _context.Orders.Where(c => c.CustomerId == customer.Id).ToList();
            if (customerOrders.Any())
            {
                foreach (var order in customerOrders)
                {
                    DeleteOrderDependencies(order);
                    _context.Orders.Remove(order);
                }
            }
        }

        public void DeleteOrderDependencies(Order order)
        {
            var orderProducts = _context.OrderProducts.Where(c => c.OrderId == order.Id).ToList();
            if (orderProducts.Any())
            {
                foreach (var product in orderProducts)
                {
                    _context.OrderProducts.Remove(product);
                }
            }
        }

        public void DeleteUserDependencies(int id)
        {
            var user = _context.Users.Single(c => c.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }
    }
}
