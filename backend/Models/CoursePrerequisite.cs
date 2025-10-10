using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class CoursePrerequisite
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = default!;

        public Guid PrerequisiteId { get; set; }
        public Course PrerequisiteCourse { get; set; } = default!;
    }
}
