using Microservices.Product.Data;
using Microservices.Product.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Product.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //add properties here
        private readonly ProductDBContext _context;
        public ProductRepository(ProductDBContext context)
        {
            _context = context;
        }
        //add get all logic here
        public async Task<List<Products>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        //add get by id logic here
        public async Task<Products> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Products> AddProductAsync(Products product)
        {
            var result = _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        //add update logic here
        public async Task<int> UpdateProductAsync(Products product)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (result != null)
            {
                result.Name = product.Name;
                result.Price = product.Price;
                result.Description = product.Description;
                result.Category = product.Category;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        //add delete logic here
        public async Task<int> DeleteProductAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Products.Remove(result);
               return await _context.SaveChangesAsync();
            }
            return 0;
        }

    }
}
