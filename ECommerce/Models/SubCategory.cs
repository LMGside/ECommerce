using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("SubCategory")]
    public class SubCategory
    {
        [Key]
        [Required]
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(2);

        [ValidateNever]
        public Category Category { get; set; }
    }
}
