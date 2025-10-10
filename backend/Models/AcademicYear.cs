using CourseMate.Models.Enums;

namespace CourseMate.Models
{
    public class AcademicYear
    {
        public Guid Id { get; set; }
        public string Year { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public ICollection<CourseOffering>? CourseOfferings { get; set; }
        public ICollection<Semester>? Semesters { get; set; }

        public string getAcademicSession(DateOnly year)
        {
            var month = year.Month;
            if (month >= 8 || month <= 12)
            {
                return $"{enumSemester.Fall} {year.Year}";
            }
            if(month >= 1 && month <= 5)
            {
                return $"{enumSemester.Spring} {year.Year}";
            }
            
            return $"{enumSemester.Summer} {year.Year}";
        }
    }
}
