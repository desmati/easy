namespace EasyPayment.Data.Entities
{
    /// <summary>
    /// درخواست پرداخت
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// مبلغ
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// واحد پول
        /// </summary>
        public string ISOCurrencyCode { get; set; } = "IRR";

        /// <summary>
        /// کد کاربر درخواست کننده
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// شماره قرارداد یا صورت حساب مربوطه که این پرداخت باید برای آن انجام شود.
        /// </summary>
        public string ContractId { get; set; }

        /// <summary>
        /// موجودی حساب کمپانی کاربر پیش ما در لحظه ای که درخواست پرداخت کرده. 
        /// این پارامتر را خودمان پر می کنیم.
        /// </summary>
        public decimal UserBalance { get; set; }
    }
}
