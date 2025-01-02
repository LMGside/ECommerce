using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetSubCategories();
    }
}
