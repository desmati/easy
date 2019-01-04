using EasyPayment.Data.Entities;
using System.Threading.Tasks;

namespace EasyPayment.Abstraction
{
    public interface IPaymentProvider
    {
        /// <summary>
        /// درخواست پرداخت
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        Task<PaymentEntity> RequestPayment(PaymentRequest paymentRequest);

        /// <summary>
        /// شروع عملیات پرداخت
        /// </summary>
        Task<PaymentEntity> ProceedToPayment(PaymentEntity payment);

        /// <summary>
        /// نهایی کردن پرداخت پس از اتمام عملیات توسط روش پرداخت انتخاب شده
        /// </summary>
        Task<PaymentEntity> FinalizePayment(PaymentEntity payment);
    }
}
