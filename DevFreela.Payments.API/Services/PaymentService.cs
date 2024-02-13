using DevFreela.Payments.API.Models;
using System.Threading.Tasks;

namespace DevFreela.Payments.API.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> Process(PaymentInfoInputModel paymentInfoInputModel)
        {
            // It does not matter in this context to process the payment
            // since this project is just to exercise both types of integration with microservices
            return Task.FromResult(true);
        }
    }
}
