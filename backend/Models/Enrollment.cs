using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class Enrollment
    {
        [Key]
        public Guid EnrollmentId { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public Guid OfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = default!;

        [Required, StringLength(20)]
        public string Status { get; set; } = "Enrolled"; // Enrolled, Dropped, Completed

        [StringLength(5)]
        public string? Grade { get; set; } // optional; link to GradeScale
    }
}
