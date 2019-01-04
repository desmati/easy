using EasyPayment.Abstraction;
using EasyPayment.Data.Entities;
using System;
using System.Threading.Tasks;

namespace EasyPayment.Concrete
{
    public class PaymentConcrete
    {
        public static async Task<PaymentEntity> PaymentRequest(PaymentRequest paymentRequest, PaymentMethod paymentMethod)
        {
            return await ProviderInstanse(paymentMethod)?.RequestPayment(paymentRequest);
        }

        public static async Task<PaymentEntity> ProceedToPayment(PaymentEntity payment)
        {
            return await ProviderInstanse(payment.SelectedMethod)?.ProceedToPayment(payment);
        }

        public static async Task<PaymentEntity> FinalizePayment(PaymentEntity payment)
        {
            return await ProviderInstanse(payment.SelectedMethod)?.FinalizePayment(payment);
        }

        private static IPaymentProvider ProviderInstanse(PaymentMethod paymentMethod)
        {
            object providerObject = (Activator.CreateInstance(Type.GetType(paymentMethod.ProviderClassName, true)));

            return (IPaymentProvider)Convert.ChangeType(providerObject, typeof(IPaymentProvider));
        }
    }
}
