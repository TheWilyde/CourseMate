using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models.Dtos
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now.Date;

        public Guid DepartmentId { get; set; }
        public Guid StudentSemesterId { get; set; }
        public Guid EnrollmentId { get; set; }
        public int Role { get; set; }

        public bool SetIsGraduatedStatus { get; set; }

        public string Major { get; set; } = string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;
    }
}