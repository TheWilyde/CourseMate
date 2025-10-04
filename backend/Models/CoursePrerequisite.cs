using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class CoursePrerequisite
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        [ForeignKey("PrerequisiteCourse")]
        public Guid PrerequisiteId { get; set; }

        public Course Course { get; set; } = null!;
        public Course PrerequisiteCourse { get; set; } = null!;
    }
}
