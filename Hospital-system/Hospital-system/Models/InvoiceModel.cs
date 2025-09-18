using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class InvoiceModel
    {
        public string Id { get; set; }

        //[ForeignKey(nameof(Appointment))]
        //public string? AppointmentId { get; set; }

        [ForeignKey(nameof(Patient))]
        public string PatientId { get; set; }    

        [ForeignKey(nameof(doctorUser))]
        public string? DoctorUserId { get; set; }    
        
        public string Status { get; set; }
        public string Speciality { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        //[JsonIgnore]
        //public virtual Appointment Appointment { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser doctorUser { get; set; }
        [JsonIgnore]
        public virtual List<Payment> Payments { get; set; }
    }
}
