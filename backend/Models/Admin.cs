using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Admin : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public int Position { get; set; }  //ToDo: Enum for positions

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
