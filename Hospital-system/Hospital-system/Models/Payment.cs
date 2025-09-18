using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class Payment
    {
        public string Id { get; set; }

        [ForeignKey(nameof(Invoice))]
        public string InvoiceId { get; set; }    
        public decimal Amount { get; set; }

        // comes from Stripe
        public string StripePaymentId { get; set; } 
        public string Status { get; set; }        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual InvoiceModel Invoice { get; set; }
    }
}
