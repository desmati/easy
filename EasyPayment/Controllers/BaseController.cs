using EasyPayment.Business;
using Microsoft.AspNetCore.Mvc;

namespace EasyPayment.Controllers
{
    public class BaseController : Controller
    {
        public PaymentBusiness Payments;
        protected BaseController()
        {
            Payments = new PaymentBusiness();
        }
    }
}