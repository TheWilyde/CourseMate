using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Student : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Major { get; set; } = string.Empty;

        public int Year { get; set; } = DateTime.UtcNow.Year;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        public Guid DegreeId { get; set; }
        public Degree Degree { get; set; } = default!;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<StudentSemester> StudentSemesters { get; set; } = new List<StudentSemester>();
    }
}
