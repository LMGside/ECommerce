namespace ECommerce.Models.DisplayModels
{
    public class ProductList
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
