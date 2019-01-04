using EasyPayment.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyPayment.Controllers
{
    /// <summary>
    /// پس از اینکه کاربر روش پرداخت را انتخاب کرد، اطلاعات لازم را مطابق مثال زیر به ان اکشن می فرستیم:
    /// http://localhost:port/ProceedToPayment/?SelectedPaymentMethodId=4 Token=c22cf0a9c26a4c07904acb125b2718f1&CallbackUrl=http://google.com
    /// </summary>
    [Route("ProceedToPayment")]
    public class ProceedToPaymentController : BaseController
    {
        // GET ProceedToPayment
        [HttpGet]
        public async Task<ViewResult> Get([FromQuery]ProceedToPaymentRequest model)
        {
            return View(await Payments.ProceedToPayment(model));
        }

        //// POST ProceedToPayment
        //[HttpPost]
        //public ViewResult Post([FromBody]ProceedToPaymentRequest model) => View(Payments.ProceedToPayment(model));
    }
}