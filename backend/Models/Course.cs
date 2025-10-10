using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string CourseCode { get; set; } = string.Empty;

        [Range(0, 50)]
        public int CreditHours { get; set; }

        public Guid DegreeId { get; set; }
        public Degree Degree { get; set; } = default!;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;

        public ICollection<CourseOffering> Offerings { get; set; } = new List<CourseOffering>();
        public ICollection<CoursePrerequisite> PrerequisiteCourses { get; set; } = new List<CoursePrerequisite>();
        public ICollection<CoursePrerequisite> IsPrerequisiteFor { get; set; } = new List<CoursePrerequisite>();
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
