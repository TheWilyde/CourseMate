namespace CourseMate.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public ICollection<Course>? Courses { get; set; }
    }
}
