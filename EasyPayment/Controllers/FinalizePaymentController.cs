using EasyPayment.Data.Enumerables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Web;

namespace EasyPayment.Controllers
{
    [Produces("application/json")]
    [Route("FinalizePayment")]
    public class FinalizePaymentController : BaseController
    {
        // GET FinalizePayment
        [HttpGet]
        public async Task<RedirectResult> Get(string token)
        {
            var payment = await Payments.FinalizePayment(token);
            var result = PaymentStatus.InvalidTransaction;
            if (payment != null)
            {
                result = payment.Status;
            }

            var callback = new Uri(payment.Proceed.CallbackUrl).AddOrUpdateParameter("status", result.ToString());
            return Redirect(callback.ToString());
        }

        // POST FinalizePayment
        [HttpPost]
        public async Task<RedirectResult> Post(string token, bool isDeposit = false)
        {
            var payment = await Payments.FinalizePayment(token);
            var result = PaymentStatus.InvalidTransaction;
            if (payment != null)
            {
                result = payment.Status;
            }

            var callback = new Uri(payment.Proceed.CallbackUrl).AddOrUpdateParameter("status", result.ToString());
            return Redirect(callback.ToString());
        }

    }
}