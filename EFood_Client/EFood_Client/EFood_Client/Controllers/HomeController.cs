using System.Threading.Tasks;
using System.Web.Mvc;
using EFoodDB.EFood_Client;

namespace EFood_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClientMethods _clientMethods = new ClientMethods();
        
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> Initialize()
        {
            await Transaction.InitalizeTransaction();
            var result = await _clientMethods.CreateTransaction(Transaction.GetTransaction());
            
            if (result)
                return RedirectToAction("ProductList", "Shoping");

            ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error al conectar con la base de datos.\n");
            return RedirectToAction("Index", "Home");
        }
    }
}