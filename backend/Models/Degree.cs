using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class Degree
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string DegreeName { get; set; } = string.Empty;

        public short DurationInYears { get; set; }

        [Required]
        public int TotalSemesters { get; set; }

        public int CurrentSemester { get; set; }
        public int Status { get; set; }  
        public bool IsSemesterSystem { get; set; }
        public int TotalCredits { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation: students & courses belong to a degree
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
