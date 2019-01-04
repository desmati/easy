using EasyPayment.Abstraction;
using EasyPayment.Data.Entities;
using System;
using System.Threading.Tasks;

namespace EasyPayment.Providers
{
    public class BPMProvider : IPaymentProvider
    {
        public Task<PaymentEntity> FinalizePayment(PaymentEntity payment)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentEntity> ProceedToPayment(PaymentEntity payment)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentEntity> RequestPayment(PaymentRequest paymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
