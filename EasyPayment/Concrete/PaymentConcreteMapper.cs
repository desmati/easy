using EasyPayment.Data.Entities;
using System.Threading.Tasks;

namespace EasyPayment.Concrete
{
    public static class PaymentConcreteMapper
    {
        public static async Task<PaymentEntity> RequestPayment(PaymentRequest paymentRequest, PaymentMethod paymentMethod)
        {
            try
            {
                return await PaymentConcrete.PaymentRequest(paymentRequest, paymentMethod);
            }
            catch
            {
                return null;
            }
        }
        public static async Task<PaymentEntity> ProceedToPayment(PaymentEntity payment)
        {
            return await PaymentConcrete.ProceedToPayment(payment);
        }

        public static async Task<PaymentEntity> FinalizePayment(PaymentEntity payment)
        {
            return await PaymentConcrete.FinalizePayment(payment);
        }
    }
}
