using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_system.Controllers
{
   
    
    [Route("api/[controller]")]
    [ApiController]  
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        [HttpPost("CreateInvoice")]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceDTO invoiceDTO)
        {

           var newInvoice = await invoiceService.CreateInvoice(invoiceDTO);

            return Ok(newInvoice);
        }

        [HttpPost("MarkPaid/{InvoiceId}")]
        public async Task<IActionResult> MarkAsPaid(string InvoiceId , PaymentRequestDTO paymentRequest)
        {
            var invoice = await invoiceService.MarkAsPaid(InvoiceId,paymentRequest);
            return Ok(invoice);
        }
    }


}
