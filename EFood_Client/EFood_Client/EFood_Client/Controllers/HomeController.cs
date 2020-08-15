using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFood_Client.UtilsMethdos;
using EFoodBLL.ClientModels;
using EFoodDB.DBSettings;
using EFoodDB.EFood_Client;
using static System.DateTime;
    
namespace EFood_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdministrationMethods _administration = new AdministrationMethods();
        
        public ActionResult Index()
        {
            IDbSettings dbSettings = new EFoodAdministration();
            ViewBag.Connection = dbSettings.ValidateConnection();
            return View();
        }
        
        [HttpGet]
        public ActionResult Initialize()
        {
            Transaction.SetTransaction(CreateTransaction());
            return RedirectToAction("ProductList", "Shoping");
        }
        
        /// <summary>
        /// Este metodo se encarga de cancelar todas las ordenes hechos por el usuario.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CancelOrder()
        {
            Transaction.DeleteTransaction();
            Shopping.DeleteOrders();
            //Utils.DeleteData();
            
            await _administration.InsertOrder(order: new Order()
            {
                Discount = string.Empty
                ,Processor = null
                ,CardType = null
                ,Transaction = Transaction.GetTransaction()
                ,Status = 3
            });
            
            return RedirectToAction("Index", "Home");
        }
        
        private static string CreateTransaction() => Now.ToString("yyyyMMddHHmmssffff");
        
    }
}