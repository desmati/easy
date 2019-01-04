using EasyPayment.Data.Enumerables;

namespace EasyPayment.Data.Entities
{
    /// <summary>
    /// درخواست شروع عملیات پرداخت به روش انتخاب شده
    /// </summary>
    public class ProceedToPaymentRequest
    {
        /// <summary>
        /// کد روش انتخاب شده
        /// </summary>
        public PaymentMethods SelectedPaymentMethodId { get; set; }

        /// <summary>
        /// کد یکتای پاسخ درخواست پرداخت که در مراحل قبل به درخواست کننده داده شده بود
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// آدرسی که پس از اتمام عملیات پرداخت، نتیجه به آن ارسال می شود.
        /// </summary>
        public string CallbackUrl { get; set; }
    }
}
