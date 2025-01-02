using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("ErrorLog")]
    public class ErrorLog
    {
        public int ErrorLogId { get; set; }
        [Required]
        public string ErrorMsg { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
