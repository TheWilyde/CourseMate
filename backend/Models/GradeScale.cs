using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class GradeScale
    {
        [Key, StringLength(5)]
        public string GradeCode { get; set; } = string.Empty; // e.g. A, B+, F

        [Range(0, 4)]
        public decimal GradePoints { get; set; } // e.g. 4.00, 3.33, 0.0

        [StringLength(50)]
        public string Description { get; set; } = string.Empty;
    }
}
