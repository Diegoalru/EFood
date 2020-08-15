using System.Threading.Tasks;
using System.Web.Mvc;
using EFood_Client.UtilsMethdos;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Client;
using ShoppingCart = EFoodBLL.ClientModels.ShoppingCart;

namespace EFood_Client.Controllers
{
    public class ShopingController : Controller
    {
        private readonly AdministrationMethods _administrationMethods = new AdministrationMethods();

        #region ProductList

        /// <summary>
        /// Carga la vista de los tipos de linea que existen y los productos asociados.
        /// </summary>
        /// <returns>Retorna la vista conn los datos predeterminados.</returns>
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
        
        /// <summary>
        /// Sirve para recibir el tipo de linea que eligio el usuario y cargar los nuevos productos.
        /// </summary>
        /// <param name="typeList">Objeto que contiene el tipo de linea que se desea ver.</param>
        /// <returns>Vista con los datos seleccionados por el usuario.</returns>
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

        #endregion

        
        #region Product
        
        /// <summary>
        /// Vista que muestra la informacion del producto.
        /// </summary>
        /// <param name="id">Codigo interno del producto.</param>
        /// <returns>Vista que contiene la informacion del producto.</returns>
        [HttpGet]
        public async Task<ActionResult> Product(int id)
        {
            var product = await _administrationMethods.ReturnProduct(id);
            
            if (product == null)
                return HttpNotFound();

            var prices = await _administrationMethods.ProductPrices(id);
            ViewBag.VBPrices = prices;
            ViewBag.VBProduct = product;
            return View();
        }
        
        /// <summary>
        /// Guarda la compra realizada en el sistema.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Product(int type, int amount, string name)
        {
            var price = await _administrationMethods.ProductPrice(type);
            
            var data = new ShoppingCart
            {
                ProductPrice = type, Quantity = amount, Transaction = Transaction.GetTransaction(), Name = name, Price = price
            };

            data.Transaction = Transaction.GetTransaction(); 
            Shopping.InsertPurchase(data);
            return RedirectToAction("ProductList", "Shoping");
        }

        #endregion

        #region ShoppingList

        public ActionResult ShoppingList()
        {
            return View();
        }
        
        public ActionResult DeleteProductShopping(int id)
        {
            Shopping.DeletePurchase(id);
            return RedirectToAction("ShoppingList");
        }
        
        #endregion
    }
}