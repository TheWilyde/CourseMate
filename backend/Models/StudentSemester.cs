using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class StudentSemester
    {
        [Key]
        public Guid StudentSemesterId { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }

        [ForeignKey("Semester")]
        public Guid SemesterId { get; set; }

        [Range(0, 4)]
        public decimal SGPA { get; set; }

        [Range(0, 4)]
        public decimal CGPA { get; set; }

        // Navigation
        public Student Student { get; set; } = null!;
        public Semester Semester { get; set; } = null!;
    }
}
