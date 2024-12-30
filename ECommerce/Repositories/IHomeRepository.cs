using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetProducts(string search, int subCategory);
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Product>> GetNewlyAddedProducts();
    }
}
