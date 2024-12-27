using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("ProductImages")]
    public class ProductImage
    {
        [Key]
        [Required]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int ProductId { get; set; }
        public bool DefaultImage { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
    }
}
