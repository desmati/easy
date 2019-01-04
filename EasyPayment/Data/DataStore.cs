using EasyPayment.Data.Entities;
using System;
using System.Data;
using System.IO;

namespace EasyPayment.Data
{
    public class DataStore
    {
        public static readonly string StoragePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "Data", "Storage");
        public class PaymentsStore : EasyJsonFileStorage<PaymentEntity, long>
        {
            public PaymentsStore(string StoragePath) : base(StoragePath)
            {
            }
        }


        private static PaymentsStore payments;
        public static PaymentsStore Payments =>
            payments ?? (payments = new PaymentsStore(Path.Combine(StoragePath, "Payments")));


    }
}
