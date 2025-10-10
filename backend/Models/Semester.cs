using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class Semester
    {
        [Key]
        public Guid SemesterId { get; set; } = Guid.NewGuid();

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty; // e.g., Fall 2025

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<CourseOffering> Offerings { get; set; } = new List<CourseOffering>();
        public ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
    }
}
