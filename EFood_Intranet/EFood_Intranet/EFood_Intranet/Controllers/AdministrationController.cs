using System.Web.Mvc;

namespace EFood_Intranet.Controllers
{
    public class AdministrationController : Controller
    {
        // GET
        public ActionResult ProductsList()
        {
            return View();
        }
    }
}