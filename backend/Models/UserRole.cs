using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "varchar(50)")]
        public string RoleName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(255)")]
        public string? Description { get; set; }

        [Required, Column(TypeName = "varchar(100)")]
        public string EmailDomain { get; set; } = string.Empty;

        public bool CanSelfRegister { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}