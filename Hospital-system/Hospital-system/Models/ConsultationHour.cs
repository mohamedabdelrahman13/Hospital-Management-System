using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class ConsultationHour
    {
        public string Id { get; set; }
        public DayOfWeek DayOfWeek  { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorID { get; set; }
        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }

    }
}
