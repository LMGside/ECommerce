using ECommerce.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Required]
        public int OrderDetailsId { get; set; }
        [MaxLength(50)]
        public string OrderNo { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        [Required]
        public int PaymentId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool IsCancel { get; set; }

        [ValidateNever]
        public Payment Payment { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
