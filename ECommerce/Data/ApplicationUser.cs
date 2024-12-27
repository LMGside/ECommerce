using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(MAX)")]
        public string? ImageUrl { get; set; }
        [PersonalData]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(MAX)")]
        public string? Address { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string? PostCode { get; set; }
    }
}
