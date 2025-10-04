using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class CourseOffering
    {
        [Key]
        public Guid OfferingId { get; set; }

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        [ForeignKey("Semester")]
        public Guid SemesterId { get; set; }

        [Range(1, 500)]
        public int Capacity { get; set; }

        // Navigation
        public Course Course { get; set; } = null!;
        public Semester Semester { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
