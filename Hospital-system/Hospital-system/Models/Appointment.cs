using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class Appointment
    {
        [Key]
        public string Id { get; set; }
        public DateOnly? Date {  get; set; }
        public TimeOnly StartTime {  get; set; }
        public TimeOnly? EndTime { get; set; }
        public DateTime BookedAt { get; set; } = DateTime.Now;

        public decimal Cost { get; set; }
        public bool isScheduled { get; set; }

        [ForeignKey("Doctor")]
        public string doctorID { get; set; }
        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }


        [ForeignKey("Patient")]
        public string patientID { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }


        [ForeignKey("ConsultationHour")]
        public string? ConsultationHourID { get; set; }
        [JsonIgnore]
        public virtual ConsultationHour ConsultationHour { get; set; }
    }
}
