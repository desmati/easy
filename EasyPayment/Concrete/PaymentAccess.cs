using EasyPayment.Data.Entities;
using EasyPayment.Data.Enumerables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyPayment.Concrete
{
    internal static class PaymentAccess
    {
        public static async Task<PaymentEntity> RequestPayment(PaymentRequest paymentRequest, List<PaymentMethods> excludeMethods = null)
        {
            var result = new PaymentEntity();
            var methods = PaymentMethod.Methods;
            if (excludeMethods != null)
            {
                methods = methods.Where(m => !excludeMethods.Contains(m.Id)).ToList();
            }

            var tasks = Enumerable.Range(0, methods.Count()).Select(i => PaymentConcreteMapper.RequestPayment(paymentRequest, methods[i]));
            result.Methods = (await Task.WhenAll(tasks)).Where(x => x?.Methods?.Count() > 0).SelectMany(x => x?.Methods).ToList();
            return result;
        }

        public static async Task<PaymentEntity> ProceedToPayment(PaymentEntity payment)
        {
            return await PaymentConcreteMapper.ProceedToPayment(payment);
        }

        public static async Task<PaymentEntity> FinalizePayment(PaymentEntity payment)//Dictionary<string, string> parameters, ref Payment payment, bool blockDuplicate = false, bool isPackage = false, PaymentMethod paymentMethod
        {
            return await PaymentConcreteMapper.FinalizePayment(payment);
        }


    }
}
