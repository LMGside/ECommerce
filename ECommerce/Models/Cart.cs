using ECommerce.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(2);

        [ValidateNever]
        public Product Product { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
