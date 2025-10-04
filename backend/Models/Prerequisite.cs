using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Prerequisite
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        [ForeignKey("PrereqCourse")]
        public Guid PrereqId { get; set; }

        // Navigation
        public Course Course { get; set; } = null!;
        public Course PrereqCourse { get; set; } = null!;
    }
}
