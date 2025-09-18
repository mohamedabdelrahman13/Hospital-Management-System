using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IInvoiceService
    {
        public Task<InvoiceModel> CreateInvoice(CreateInvoiceDTO invoiceDTO);
        public Task<InvoiceModel> MarkAsPaid(string InvoiceId , PaymentRequestDTO paymentRequestDTO);
    }
}