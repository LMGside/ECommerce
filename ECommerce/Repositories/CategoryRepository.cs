using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories()
        {
            var subCat = await _db.SubCategories
                .Include(x => x.Category).ToListAsync();

            return subCat;
        }
    }
}
