using System;
using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class StudentSemester
    {
        [Key]
        public Guid StudentSemesterId { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = default!;

        [Range(0, 4)]
        public decimal SGPA { get; set; }

        [Range(0, 4)]
        public decimal CGPA { get; set; }

        public ICollection<StudentCourseGrade> StudentCourseGrades { get; set; } = new List<StudentCourseGrade>();
    }
}
