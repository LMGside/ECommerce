using ECommerce.Data;

namespace ECommerce.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }


    }
}
