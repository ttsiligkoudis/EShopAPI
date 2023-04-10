using DataModels;
using Enums;

namespace Repositories.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(bool checkQuantity = false, Category? category = null);
        Task<Product> GetProduct(int id);
        Task<Product> Put(Product product);
        Task<Product> Post(Product product);
        Task<object> Delete(int id);
        Task SaveChangesAsync();
        Task<int> GetProductsCount(bool checkQuantity = false, Category? category = null);
        Task<List<Product>> GetRandomProducts(int length, bool checkQuantity = false, Category? category = null);
        Task<List<Product>> GetProductsbyIds(List<int> ids);
        Task<bool> CheckIfExists(int id);
        Task<ProductRates> GetRate(int productId, int customerId);
        Task<ProductRates> GetRateById(int id);
        Task<List<ProductRates>> GetRatesByProductId(int productId);
        Task<ProductRates> PostRate(ProductRates rate);
        Task<ProductRates> PutRate(ProductRates rate);
    }
}