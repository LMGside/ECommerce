using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [Required]
        public string ContactId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(2);
    }
}
