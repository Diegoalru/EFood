using System.Web.Mvc;

namespace EFood_Client.Controllers
{
    public class PayController : Controller
    {
        [HttpGet]
        public ActionResult PayPage()
        {
            return View();
        }
    }
}