using EasyPayment.Data.Enumerables;
using System;
using System.Collections.Generic;
using System.Data;

namespace EasyPayment.Data.Entities
{
    /// <summary>
    /// کل سابقه یک رداخت، از ابتدای درخواست تا انتهای آن در این کلاس نگهداری می شود.    /// </summary>
    public class PaymentEntity : EasyStorableObject<long>
    {
        public PaymentEntity()
        {
            Token = Guid.NewGuid().ToString("n");
            Status = PaymentStatus.JustRequested;
            Methods = new List<PaymentMethod>();
            RequestParameters = new Dictionary<string, string>();
            ResponseParameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// کد یکتای این پرداخت که ادامه، عملیات پرداخت با استفاده از آن پیگیری می شود.
        /// پس از اینکه ارسال کننده درخواست پرداخت، این کد را دریافت کرد، 
        /// می بایست آن را در جایی ثبت کند و همراه سایر درخواست هایش ارسال کند.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// درخواست پرداخت انجام شده
        /// </summary>
        public PaymentRequest Request { get; set; }

        /// <summary>
        /// روش های پرداختی که به کاربر پیشنهاد شده است.
        /// </summary>
        public List<PaymentMethod> Methods { get; set; }

        /// <summary>
        /// روش پرداختی که کاربر انتخاب شده است
        /// </summary>
        public PaymentMethod SelectedMethod { get; set; }

        /// <summary>
        /// درخواست آغاز عملیات پرداخت
        /// </summary>
        public ProceedToPaymentRequest Proceed { get; set; }

        public PaymentStatus Status { get; set; }

        /// <summary>
        /// پارامترهای بازگشتی از روش پرداخت انتخاب شده
        /// </summary>
        public Dictionary<string, string> ResponseParameters { get; set; }

        /// <summary>
        /// پارامترهایی که باید به آدرس یو آر ال متود انتخابی پست شوند.
        /// </summary>
        public Dictionary<string, string> RequestParameters { get; set; }

        /// <summary>
        /// کد تراکنشی که بانک بر می گرداند
        /// </summary>
        public string TransactionId { get; set; }

    }
}
