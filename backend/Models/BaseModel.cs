using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; }
        public int IsActive { get; set; }   
        public int Role { get; set; }       

        [Required, Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Required, Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = string.Empty;

        [Required, Column(TypeName = "varchar(255)")]
        public string Email { get; set; } = string.Empty;

        [Required, Column(TypeName = "varchar(255)"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$")]
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string? Address { get; set; }

        [Column(TypeName = "varchar(60)")]
        public string? City { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? State { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string? ZipCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Country { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
