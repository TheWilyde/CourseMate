using System.ComponentModel.DataAnnotations.Schema;

namespace CourseMate.Models
{
    public class Instructor : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Designation { get; set; } = string.Empty;   //ToDo: Enum for designations

        // Office hours represented as start and end times
        public TimeOnly StartHours { get; set; }
        public TimeOnly EndHours { get; set;  }
    }
}
