using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyPayment.Controllers
{
    [Route("PaymentCallBack")]
    public class PaymentCallBackController : BaseController
    {
        // GET FinalizePayment
        [HttpGet]
        public async Task<ViewResult> Get()
        {
            return View(await Payments.PaymentCallBack(Request));
        }

        // POST FinalizePayment
        [HttpPost]
        public async Task<ViewResult> Post()
        {
            return View("Get", await Payments.PaymentCallBack(Request));
        }
    }
}