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
            var resultTransaction = await Transaction.InitalizeTransaction();
            if (resultTransaction == 1)
            {
                await _clientMethods.CreateTransaction(Transaction.GetTransaction());
                return RedirectToAction("ProductList", "Shoping");
            }

            ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error. ¡Reintente de nuevo!\n");
            return RedirectToAction("Index", "Home");

        }
    }
}