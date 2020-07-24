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

        /*
         * FIXME:
         *     1- Investigar la razon por la que al volver al index 0 de los tipos de linea,
         *     deja de funcionar. No cambia los datos que se muestran en la pantalla
         *     solo se muestra cuando carga.
         */
        
        
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

        /// <summary>
        /// Vista que muestra la informacion del producto.
        /// </summary>
        /// <param name="id">Codigo interno del producto.</param>
        /// <returns>Vista que contiene la informacion del producto.</returns>
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

        /// <summary>
        /// Guarda la compra realizada en el sistema.
        /// </summary>
        /// <param name="data">Información de la orden.</param>
        /// <returns>Retorna a vista que contiene los distintos productos.</returns>
        [HttpPost]
        public ActionResult CheckProduct(EFoodBLL.ClientModels.ShoppingCart data)
        {
            data.Transaction = Transaction.GetTransaction(); 
            Shopping.InsertPurchase(data);
            return RedirectToAction("ProductList", "Shoping");
        }

        /// <summary>
        /// Retorna la vista del registro para el cliente.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClientRegister()
        {
            return View();
        }
        
        /// <summary>
        /// Metodo que realiza la 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ClientRegister(Client data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var existsDiscount = await _administrationMethods.ExistsDiscount(data.Discount);
            
            /*
             * Todo: Problemas con el cupon, ya que a la hora de aceptar y continuar, no 
             */ 
            switch (existsDiscount)
            {
                case true:
                    var result = await _clientMethods.InsertClient(data);
                    if (result)
                    {
                        Utils.SetDiscount(data.Discount);   
                        return RedirectToAction("PayMethod", "Shoping");
                    }
                    ModelState.AddModelError("", "¡Error! Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View());
                
                case false:
                    ModelState.AddModelError("", "¡El tiquete de descuento no es valido!.\n");
                    break;
                
                default:
                    ModelState.AddModelError("", "¡Error! Ha ocurrido un error.\n");
                    break;
            }
            return await Task.FromResult<ActionResult>(View());
        }

        /// <summary>
        /// Este metodo se encarga de cancelar todas las ordenes hechoas por el usuario.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CancelOrder()
        {
            await _clientMethods.UpdateTransactionStatus(Transaction.GetTransaction(), 2);
            Transaction.DeleteTransaction();
            Shopping.DeleteOrders();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PayMethod()
        {
            return View();
        }

        public ActionResult MethodCash()
        {
            Shopping.AmountShoping();
            ViewBag.Subtotal = Shopping.GetAmount();
            
            return View();
        }

        public ActionResult PayPage()
        {
            return View();
        }

        public ActionResult MethodCard()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult MethodCheck()
        {
            throw new System.NotImplementedException();
        }
    }
}