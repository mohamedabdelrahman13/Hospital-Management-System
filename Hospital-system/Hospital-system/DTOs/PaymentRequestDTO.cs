namespace Hospital_system.DTOs
{
    public class PaymentRequestDTO
    {
        public decimal Amount { get; set; }
        public string StripePaymentId { get; set; }
        public string Status { get; set; }
    }
}
