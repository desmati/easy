namespace EasyPayment.Data.Enumerables
{
    public enum PaymentStatus
    {
        /// <summary>
        /// درخواست ارسال شده و منتظر انتخاب روش پرداخت توسط کاربر
        /// </summary>
        JustRequested = 28,

        /// <summary>
        /// درخواست شروع عملیات ارسال شده
        /// </summary>
        Proceeding = 29,

        /// <summary>
        /// عملیات پرداخت تمام شده
        /// </summary>
        Successful = 2,

        Cancelled = 0,
        PaymentRejected = 1,
        CanceledByUser = 3,
        InvalidAmount = 4,
        InvalidTransaction = 5,
        InvalidCardNumber = 6,
        NoSuchIssuer = 7,
        ExpiredCardPickUp = 8,
        AllowablePINTriesExceededPickUp = 9,
        IncorrectPIN = 10,
        ExceedsWithdrawalAmountLimit = 11,
        TransactionCannotBeCompleted = 12,
        ResponseReceivedTooLate = 13,
        SuspectedFraudPickUp = 14,
        NoSufficientFunds = 15,
        IssuerDownSlm = 16,
        TMEError = 17,
        NotConfirmed = 18,
        DigitalReceiptsNotConfirmed = 19,
        ConnectionError = 20,
        AmountNotEqual = 21,
        Reverse = 22,
        BankServerError = 23,
        SubmissionsValuesNotValid = 24,
        ProblemsAssociatedWithTheSwitch = 25,
        NotRecivePaymentMassage = 26,
        Duplicate = 27,
        Done = 30
    }
}
