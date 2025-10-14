using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models.Dtos
{
    public class SignUpRequestDto
    {
        [Required]
        [DataType(DataType.Text)]
        public required string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public required string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateTime DateOfBirth { get; set; }

        // Student-specific fields
        public string? Major { get; set; }
        public int? Year { get; set; }

        // Instructor-specific fields
        public string? Designation { get; set; }
    }
}
