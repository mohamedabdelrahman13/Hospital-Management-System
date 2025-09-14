using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class Doctor
    {
        public string Id { get; set; }

        [MaxLength(40)]
        [MinLength(10)]
        public decimal Cost { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        [ForeignKey("Department")]
        public string DepartmentID { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
        [JsonIgnore]
        public virtual List<ConsultationHour> ConsultationHours { get; set; }
  
    }
}
