using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages(int? productId)
        {
            var products = await _db.ProductImages.Where(p => p.ProductId == productId).ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductList(int page, int pageSize = 5)
        {
            IEnumerable<Product> prod = await _db.Products.Skip(page*pageSize).Take(pageSize)
                .Include(a => a.ProductImages)
                .ToListAsync();

            return prod;
        }
    }
}
