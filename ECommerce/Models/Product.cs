using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AdditionalDescription { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [MaxLength(30)]
        public string Size { get; set; }
        [MaxLength(50)]
        public string Colour { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        public int Sold {  get; set; }
        [Required]
        public bool IsCustomised { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(2);
        [Column(TypeName = "decimal(18,2)")]
        public decimal? OldPrice { get; set; }
        public bool OnSale { get; set; }
        public int? DiscountPercentage { get; set; }


        [ValidateNever]
        public SubCategory SubCategory { get; set; }
    }
}
