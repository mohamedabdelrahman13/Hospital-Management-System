namespace Hospital_system.DTOs
{
    public class InvoiceDTO
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PaymentDTO> Payments { get; set; }
    }
}
