using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductImage>> GetProductImages(int? productId);
        Task<IEnumerable<Product>> GetProductList(int page, int pageSize = 5);
    }
}
