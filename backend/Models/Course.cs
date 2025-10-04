using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        [Required, StringLength(10)]
        public string Code { get; set; } = string.Empty; // e.g., CS101

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credits { get; set; }

        // Course-level status (Active/Inactive)
        [Required, StringLength(20)]
        public string Status { get; set; } = string.Empty; // e.g., Active, Inactive

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        // Navigation
        public ICollection<CourseOffering> Offerings { get; set; } = new List<CourseOffering>();


        // Self-referencing many-to-many relationship explained:
        //
        // Each course can depend on other courses (its prerequisites),
        // and each course can also *be* a prerequisite for other courses.
        //
        // EF Core can't directly model a self-referencing many-to-many, so
        // we use a junction entity (CoursePrerequisite) to represent the link.
        //
        // ┌────────────────────────────┐
        // │        Course              │
        // ├────────────────────────────┤
        // │   CourseId (PK)            │
        // │   Code, Title, ...         │
        // └────────────┬───────────────┘
        //              │
        //      CoursePrerequisite
        //     (CourseId, PrerequisiteId)
        //              │
        //              ▼
        //           Course
        // EF Core will automatically handle the Ids by creating two foreign keys in the junction table.
        // CourseID -> references main  course
        // PrerequisiteID -> references prerequisite course
        

        // Courses that THIS course *depends on*
        public ICollection<CoursePrerequisite> PrerequisiteCourses { get; set; } = new List<CoursePrerequisite>();

        // Courses that *depend on THIS course* as a prerequisite
        public ICollection<CoursePrerequisite> IsPrerequisiteFor { get; set; } = new List<CoursePrerequisite>();

        
    }
}
