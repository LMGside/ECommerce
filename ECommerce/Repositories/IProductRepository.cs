using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductImage>> GetProductImages(int? productId);
    }
}
