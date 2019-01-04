using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyPayment.Controllers
{
    [Produces("application/json")]
    [Route("VerifyPayment")]
    public class VerifyPaymentController : BaseController
    {
        [HttpPost]
        public async Task<JsonResult> Post(string token)
        {
            return Json(await Payments.VerifyPayment(token));
        }
    }
}