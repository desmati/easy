using EasyPayment.Data.Enumerables;
using System;
using System.Collections.Generic;
using System.IO;

namespace EasyPayment.Data.Entities
{
    /// <summary>
    /// روش های پرداخت
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// کد روش پرداخت از لیست زیر:
        /// BPM
        /// </summary>
        public PaymentMethods Id { get; set; }

        /// <summary>
        /// نام بانک یا روش پرداخت
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// لوگو بانک یا روش پرداخت:
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// توضیح در مورد روش پرداخت
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// آدرسی که درخواست کننده برای انجام پرداخت باید به آن هدایت شود.
        /// </summary>
        public string Url { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string TerminalId { get; set; }

        public Dictionary<string, string> OtherParameters { get; set; }

        public string ProviderClassName { get; set; }

        /// <summary>
        /// روش های پرداخت فعلی سیستم
        /// </summary>
        public static List<PaymentMethod> Methods => methods ?? (methods = File.ReadAllText(Path.Combine(DataStore.StoragePath, "PaymentMethods.json")).FromJson<List<PaymentMethod>>());

        private static List<PaymentMethod> methods { get; set; }
    }
}
