using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class Semester
    {
        [Key]
        public Guid SemesterId { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty; // e.g., Fall 2025

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // Navigation
        public ICollection<CourseOffering> Offerings { get; set; } = new List<CourseOffering>();
        public ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
    }
}
