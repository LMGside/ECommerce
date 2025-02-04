namespace ECommerce.Models.DisplayModels
{
    public class Details
    {
        public Product Product { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
    }
}
