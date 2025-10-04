using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
