using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class Lecture
    {
        [Key]
        public Guid LectureId { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = default!;

        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; } = default!;

        public string? LectureTopic { get; set; }
        public DateTime? LectureDate { get; set; }
    }
}
