using EasyPayment.Concrete;
using EasyPayment.Data;
using EasyPayment.Data.Entities;
using EasyPayment.Data.Enumerables;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyPayment.Business
{
    public class PaymentBusiness
    {
        public async Task<PaymentEntity> RequestPayment(PaymentRequest paymentRequest)
        {
            var result = await PaymentAccess.RequestPayment(paymentRequest);
            if (result == null)
            {
                return new PaymentEntity();
            }

            result.Request = paymentRequest;
            await DataStore.Payments.SaveAsync(result);
            var methods = new List<PaymentMethod>();
            foreach (var method in result.Methods)
            {
                if (method != null)
                {
                    methods.Add(new PaymentMethod()
                    {
                        Id = method.Id,
                        Logo = method.Logo,
                        Name = method.Name,
                        Url = method.Url
                    });
                }
            }

            result.Methods = methods;

            return result;
        }

        internal async Task<PaymentEntity> PaymentCallBack(HttpRequest request)
        {
            var parameters = request.Query.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)).ToDictionary(q => q.Key, s => s.Value);
            if (!parameters.Any())
            {
                parameters = request.Form.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)).ToDictionary(q => q.Key, s => s.Value);
            }

            var token = parameters.TryGetValue("orderid", "ResNum", "echoToken", "CRN");
            if (token == null)
            {
                return null;
            }

            var payment = await DataStore.Payments.FirstOrDefaultAsync(x => x.Token == token);
            payment.ResponseParameters = parameters;
            await DataStore.Payments.SaveAsync(payment);
            return payment;
        }

        internal async Task<PaymentEntity> FinalizePayment(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var payment = await DataStore.Payments.FirstOrDefaultAsync(x => x.Token == token);
            var result = await PaymentAccess.FinalizePayment(payment);
            await DataStore.Payments.SaveAsync(payment);
            return result;
        }

        public async Task<PaymentEntity> ProceedToPayment(ProceedToPaymentRequest request)
        {
            var payment = await DataStore.Payments.FirstOrDefaultAsync(x => x.Token == request.Token);
            if (payment == null)
            {
                return null;
            }

            payment.Proceed = request;
            payment.SelectedMethod = PaymentMethod.Methods.FirstOrDefault(x => x.Id == request.SelectedPaymentMethodId);
            var result = await PaymentAccess.ProceedToPayment(payment);
            await DataStore.Payments.SaveAsync(payment);
            return result;
        }

        public async Task<bool> VerifyPayment(string token)
        {
            var payment = await DataStore.Payments.FirstOrDefaultAsync(x => x.Token == token);
            if (payment == null)
            {
                return false;
            }

            if (payment.Status != PaymentStatus.Successful)
            {
                return false;
            }

            payment.Status = PaymentStatus.Done;
            return true;
        }
    }
}
