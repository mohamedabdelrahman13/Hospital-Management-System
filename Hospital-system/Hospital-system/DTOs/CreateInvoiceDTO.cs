using Hospital_system.Models;

namespace Hospital_system.DTOs
{
    public class CreateInvoiceDTO
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Speciality { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // cash or card
    }
}
