using Hospital_system.Controllers;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Hospital_system.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IBaseRepository<InvoiceModel> invoiceRepo;
        private readonly IBaseRepository<Payment> paymentRepo;

        public InvoiceService(IBaseRepository<InvoiceModel> invoiceRepo,
            IBaseRepository<Payment> paymentRepo)
        {
            this.invoiceRepo = invoiceRepo;
            this.paymentRepo = paymentRepo;
        }


        public async Task<InvoiceModel> CreateInvoice(CreateInvoiceDTO invoiceDTO)
        {
            var invoice = new InvoiceModel
            {
                DoctorUserId = invoiceDTO.DoctorId,
                PatientId = invoiceDTO.PatientId,
                Amount = invoiceDTO.Amount,
                Speciality = invoiceDTO.Speciality,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                Payments = new List<Payment>()
            };

            await invoiceRepo.AddAsync(invoice);
            await invoiceRepo.SaveAsync();

            return invoice;
        }


        public async Task<InvoiceModel> MarkAsPaid(string invoiceId ,PaymentRequestDTO paymentRequestDTO)
        {
            var invoice = await invoiceRepo.GetByID(invoiceId);
            if (invoice == null)
                return null;

            var payment = new Payment
            {
                InvoiceId = invoiceId,
                Amount = paymentRequestDTO.Amount,
                StripePaymentId = paymentRequestDTO.StripePaymentId,
                Status = paymentRequestDTO.Status,
                CreatedAt = DateTime.UtcNow
            };
            invoice.Status = "Paid";

           await paymentRepo.AddAsync(payment);
           await paymentRepo.SaveAsync();

            return invoice;
        }

    }
}
