using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class StudentCourseGrade
    {
        [Key]
        public Guid StudentCourseGradeId { get; set; } = Guid.NewGuid();
        public Guid StudentSemesterId { get; set; }
        [ForeignKey(nameof(StudentSemesterId))]
        public StudentSemester StudentSemester { get; set; } = default!;

        public Guid CourseOfferingId { get; set; }
        [ForeignKey(nameof(CourseOfferingId))]
        public CourseOffering CourseOffering { get; set; } = default!;

        public Guid? InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Instructor? Instructor { get; set; }

        [StringLength(5)]
        public string? Grade { get; set; }

        [Range(0, 4)]
        public decimal? GradePoints { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } // e.g., Enrolled, Dropped, Completed, Failed
    }
}
