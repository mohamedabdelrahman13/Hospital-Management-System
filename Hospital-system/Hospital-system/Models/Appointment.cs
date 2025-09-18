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

        [Range(200, 1500, ErrorMessage = "Cost must be between 200 and 1500.")]
        public decimal Cost { get; set; }

        [MaxLength(11)]
        public string Status { get; set; } = "Scheduled";
        public string DoctorUserID { get; set; }

        [ForeignKey(nameof(DoctorUserID))]
        [JsonIgnore]
        public virtual ApplicationUser doctorUser { get; set; }


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
