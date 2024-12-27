using ECommerce.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Wishlist")]
    public class Wishlist
    {
        [Key]
        [Required]
        public int WishlistId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [ValidateNever]
        public Product Product { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
