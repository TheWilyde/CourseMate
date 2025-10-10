using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Admin : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Position { get; set; } = string.Empty;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
    }
}
