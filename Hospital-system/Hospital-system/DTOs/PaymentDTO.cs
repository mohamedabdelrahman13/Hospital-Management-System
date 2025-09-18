namespace Hospital_system.DTOs
{
    public class PaymentDTO
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
