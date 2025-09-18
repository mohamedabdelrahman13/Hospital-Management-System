using Hospital_system.Interfaces;
using Stripe;

namespace Hospital_system.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly string _secretKey;
        public PaymentService(IConfiguration Config)
        {
            _secretKey = Config["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = _secretKey;
        }


        //public async Task<PaymentIntent> CreatePaymentIntent(decimal amount)
        //{
        //    //var options = new PaymentIntentCreateOptions
        //    //{
        //    //    Amount = (long)(amount * 100),
        //    //    Currency = "usd",
        //    //    PaymentMethodTypes = new List<string> { "card" },
        //    //};

        //    //var service = new PaymentIntentService();
        //    //return await service.CreateAsync(options);
        //}
    }
}
