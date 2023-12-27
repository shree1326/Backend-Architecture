using Microservices.Product.Models;

namespace Microservices.Product.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Products>> GetAllProductsAsync();
        public Task<Products> GetProductByIdAsync(int id);
        public Task<Products> AddProductAsync(Products product);
       public Task<int> UpdateProductAsync(Products product);
        public Task<int> DeleteProductAsync(int id);
    }
}
