using EasyPayment.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyPayment.Controllers
{
    /// <summary>
    /// مرحله 1- ثبت درخواست پرداخت و دریافت یک توکن جهت ادامه مراحل
    /// </summary>
    [Produces("application/json")]
    [Route("[controller]")]
    public class PaymentRequestController : BaseController
    {
        // POST PaymentRequest
        [HttpPost]
        public async Task<PaymentEntity> Post([FromBody]PaymentRequest model)
        {
            return await Payments.RequestPayment(model);
        }
    }
}
