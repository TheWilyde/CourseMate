using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Instructor : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Designation { get; set; } = string.Empty;

        public TimeOnly? StartHours { get; set; }
        public TimeOnly? EndHours { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;

        public ICollection<CourseOffering> CourseOfferings { get; set; } = [];

        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
