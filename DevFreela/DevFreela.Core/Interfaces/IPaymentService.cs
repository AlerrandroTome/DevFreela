using DevFreela.Core.DTOs;

namespace DevFreela.Core.Interfaces
{
    public interface IPaymentService
    {
        void ProcessPayment(PaymentInfoDTO paymentInfo);
    }
}
