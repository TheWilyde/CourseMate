using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Student : BaseModel
    {

        [Column(TypeName = "varchar(100)")]
        public string Major { get; set; } = string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;

        [ForeignKey("Department")]
        public Guid DepartmentID { get; set; }

        public Department Department { get; set; } = null!;
    }
}
