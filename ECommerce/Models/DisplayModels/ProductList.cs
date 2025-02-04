namespace ECommerce.Models.DisplayModels
{
    public class ProductList
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
    }
}
