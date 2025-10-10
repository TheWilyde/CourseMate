using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class CourseOffering
    {
        [Key]
        public Guid OfferingId { get; set; } = Guid.NewGuid();

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = default!;

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = default!;

        [Range(1, 500)]
        public int Capacity { get; set; }

        [ForeignKey(nameof(Instructor))]
        public Guid? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
