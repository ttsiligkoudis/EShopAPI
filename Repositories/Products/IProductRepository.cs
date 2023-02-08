using DataModels;

namespace Repositories.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(bool checkQuantity);
        Task<Product> GetProduct(int id);
        Task<Product> Put(Product product);
        Task<Product> Post(Product product);
        Task<object> Delete(int id);
    }
}