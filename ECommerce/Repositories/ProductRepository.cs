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
    }
}
