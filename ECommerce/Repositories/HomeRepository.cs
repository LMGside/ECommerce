using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts(string search, int subCategory)
        {
            search = search.ToLower();
            IEnumerable<Product> products = await (from product in _db.Products
                                                   join subcategory in _db.SubCategories
                                                   on product.SubCategoryId equals subcategory.SubCategoryId
                                                   where product.IsActive == true && (string.IsNullOrWhiteSpace(search) || (product != null && product.ProductName.ToLower().Contains(search)))
                                                   select new Product
                                                   {
                                                       ProductName = product.ProductName,
                                                       Price = product.Price,
                                                       Quantity = product.Quantity,
                                                       OnSale = product.OnSale,
                                                       DiscountPercentage = product.DiscountPercentage,
                                                       OldPrice = product.OldPrice,
                                                       AdditionalDescription = product.AdditionalDescription,
                                                       ShortDescription = product.ShortDescription,
                                                       LongDescription = product.LongDescription

                                                   }).ToListAsync();
            if(subCategory > 0)
            {
                products = products.Where(a => a.SubCategoryId == subCategory).ToList();
            }

            return products;
        }

        public async Task<IEnumerable<Product>> GetNewlyAddedProducts()
        {
            var products = await _db.Products.OrderBy(a => a.CreatedDate).ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByPrice(int order)
        {
            if(order == 1) //Low to High
            {
                var products = await _db.Products.OrderBy(a => a.Price).ToListAsync();
                return products;
            }
            else // High to Low
            {
                var products = await _db.Products.OrderByDescending(a => a.Price).ToListAsync();
                return products;
            }
        }
    }
}
