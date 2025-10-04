using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Enrollment
    {
        [Key]
        public Guid EnrollmentId { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }

        [ForeignKey("CourseOffering")]
        public Guid OfferingId { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } = "Enrolled";
        // Enrolled, Dropped, Completed

        [StringLength(5)]
        public string? Grade { get; set; } // FK → GradeScale (optional until completed)

        // Navigation
        public Student Student { get; set; } = null!;
        public CourseOffering CourseOffering { get; set; } = null!;
    }
}
