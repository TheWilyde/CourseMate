using System.ComponentModel.DataAnnotations;

namespace CourseMate.Models
{
    public class GradeScale
    {
        [Key]
        [StringLength(5)]
        public string GradeCode { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Description { get; set; } = string.Empty;

        public decimal GradePoints { get; set; }
    }
}
