using System.Web.Mvc;
using EFood_Client.Utils;
using EFoodDB.DBSettings;
using EFoodDB.EFood_Client;
using static System.DateTime;
    
namespace EFood_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientMethods _clientMethods = new ClientMethods();
        
        public ActionResult Index()
        {
            IDbSettings dbSettings = new EFoodAdministration();
            ViewBag.Connection = false;
            return View();
        }
        
        [HttpGet]
        public ActionResult Initialize()
        {
            Transaction.SetTransaction(CreateTransaction());
            return RedirectToAction("ProductList", "Shoping");
        }
        
        private static string CreateTransaction() => Now.ToString("yyyyMMddHHmmssffff");
        
    }
}