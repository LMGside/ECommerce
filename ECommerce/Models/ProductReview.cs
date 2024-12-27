using ECommerce.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("ProductReview")]
    public class ProductReview
    {
        [Key]
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ValidateNever]
        public Product Product { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
