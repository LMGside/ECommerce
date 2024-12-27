using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [Required]
        public int PaymentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string CardNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string ExpiryDate { get; set; }
        [Required]
        public int CvvNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PaymentNo { get; set; }
    }
}
