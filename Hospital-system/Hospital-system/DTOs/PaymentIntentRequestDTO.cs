namespace Hospital_system.DTOs
{
    public class PaymentIntentRequestDTO
    {
        public string InvoiceId { get; set; }
        public string PatientId { get; set; }
        public decimal Amount { get; set; }
    }
}
