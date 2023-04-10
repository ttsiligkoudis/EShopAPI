using Microsoft.EntityFrameworkCore;
using DataModels;
using Context;
using Enums;
using DataModels.Dtos;

namespace Repositories.Products
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly AppDbContext _context = null;

        public ProductRepository(AppDbContext context)
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

        public async Task<List<Product>> GetProducts(bool checkQuantity = false, Category? category = null)
        {
            var productsInDb = _context.Products
                .Include(c => c.ProductRates)
                .Where(w => (!checkQuantity || w.Quantity > 0)
                         && (!category.HasValue || w.Category == category.Value));
            return await productsInDb.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.Include(c => c.ProductRates).SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product> Put(Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            return product;
        }

        public async Task<Product> Post(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<object> Delete(int id)
        {
            var products = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (products != null)
            {
                _context.Products.Remove(products);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetProductsCount(bool checkQuantity = false, Category? category = null)
        {
            var productsCount = await _context.Products
                .Where(w => (!checkQuantity || w.Quantity > 0)
                         && (!category.HasValue || w.Category == category.Value))
                .CountAsync();
            return productsCount;
        }

        public async Task<List<Product>> GetRandomProducts(int length, bool checkQuantity = false, Category? category = null)
        {
            var productIds = await _context.Products
                .Where(w => (!checkQuantity || w.Quantity > 0)
                         && (!category.HasValue || w.Category == category.Value))
                .Select(s => s.Id)
                .ToListAsync();

            var rand = new Random();
            var resultIds = new List<int>();
            for (int i = 0; i < length; i++)
            {
                var rnd = rand.Next(0, productIds.Count - 1);
                resultIds.Add(productIds[rnd]);
                productIds.RemoveAt(rnd);
            }

            var products = await GetProductsbyIds(resultIds);

            return products;
        }

        public async Task<List<Product>> GetProductsbyIds(List<int> ids)
        {
            ids ??= new List<int>();
            return await _context.Products
                .Where(w => ids.Contains(w.Id))
                .Include(c => c.ProductRates)
                .ToListAsync();
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _context.Products.Where(w => w.Id == id).AnyAsync();
        }

        public async Task<ProductRates> GetRate(int productId, int customerId)
        {
            return await _context.ProductRates.FirstOrDefaultAsync(w => w.ProductId == productId && w.CustomerId == customerId);
        }

        public async Task<ProductRates> GetRateById(int id)
        {
            return await _context.ProductRates.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<ProductRates>> GetRatesByProductId(int productId)
        {
            return await _context.ProductRates.Where(w => w.ProductId == productId).Include(c => c.Customer).ToListAsync();
        }

        public async Task<ProductRates> PostRate(ProductRates rate)
        {
            _context.ProductRates.Add(rate);
            await _context.SaveChangesAsync();
            return rate;
        }

        public async Task<ProductRates> PutRate(ProductRates rate)
        {
            _context.ProductRates.Attach(rate);
            _context.Entry(rate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return rate;
        }
    }
}
