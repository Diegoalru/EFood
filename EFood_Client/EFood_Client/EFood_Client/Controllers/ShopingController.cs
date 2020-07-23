using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFoodBLL.ClientModels;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Client;

namespace EFood_Client.Controllers
{
    public class ShopingController : Controller
    {
        private readonly AdministrationMethods _administrationMethods = new AdministrationMethods();
        private readonly ClientMethods _clientMethods = new ClientMethods();

        [HttpGet]
        public async Task<ActionResult> ProductList()
        {
            // Se realiza la consulta donde se obtiene los tipos de linea que existen.
            var lineTypelist = await _administrationMethods.LineTypeList();

            // Si existiera algún tipo de linea, automaticamente se mostraria los productos relacionados al primer datos de la lista.
            var productList = lineTypelist.Count != 0 ? (await _administrationMethods.ProductList(lineTypelist[0].PkCode)): null;

            //Se crea el tipo de objeto SelectList, el cual es retornado a la pagina para mostrarse en el dropdown con el id typeLinelist (primer parametro)
            var selectList = new SelectList(lineTypelist, "PkCode", "Type");

            //Se guardan los datos en el ViewBag para luego obtenelos con el id VBTypeLineList
            ViewBag.VBTypeLineList = selectList;

            //Se envian los datos a la pagina
            return View(productList);
        }
        
        [HttpPost]
        public async Task<ActionResult> ProductList(LineTypeList typeList)
        {
            // Se crea la lista que contiene los tipos de linea que existen.
            var lineTypelist = await _administrationMethods.LineTypeList();

            //Se crea un campo para almacenar la llave de tipo de linea.
            int lineTypeCode = 0;

            //Se recorre la lista hasta encontrar el dato obtenido de la pagina.
            foreach (var item in lineTypelist)
            {
                if (item.Type.Equals(typeList.Type))
                    lineTypeCode = item.PkCode;
            }

            //Se crear la lista con los productos segun el tipo de linea.
            var productList = await _administrationMethods.ProductList(lineTypeCode);

            //Se crea el objeto de tipo SelectList que será envia a el dropdown de la pagina, el cual obtendra el id de lineTypelist (primer parametro).
            var selectList = new SelectList(lineTypelist, "PkCode", "Type");

            //Se agrega el obteto selectList al ViewBag de la pagina.
            ViewBag.VBTypeLineList = selectList;

            //Se envia los datos obtenidos, en otras palabras aquí se retornan dos listas (tipos de linea y productos)
            return View(productList);
        }

        [HttpGet]
        public async Task<ActionResult> CheckProduct(int id)
        {
            var product = await _administrationMethods.ReturnProduct(id);
            
            if (product == null)
                return HttpNotFound();

            var prices = await _administrationMethods.ProductPrices(id);
            ViewBag.VBPrices = prices;
            ViewBag.VBProduct = product;
            return View();
        }

        [HttpPost]
        public ActionResult CheckProduct(EFoodBLL.ClientModels.ShoppingCart data)
        {
            data.Transaction = Transaction.GetTransaction(); 
            Shopping.InsertPurchase(data);
            return RedirectToAction("ProductList", "Shoping");
        }

        [HttpGet]
        public ActionResult ClientRegister()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ClientRegister(Client data)
        {
            return View();
        }

    }
}