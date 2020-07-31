using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFoodBLL.ClientModels;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Client;
using ShoppingCart = EFoodBLL.ClientModels.ShoppingCart;

namespace EFood_Client.Controllers
{
    public class ShopingController : Controller
    {
        private readonly AdministrationMethods _administrationMethods = new AdministrationMethods();
        private readonly ClientMethods _clientMethods = new ClientMethods();

        /*
         * FIXME:
         *     1- Investigar la razon por la que al volver al index 0 de los tipos de linea, (Pantalla: ProductList)
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
        public ActionResult Product(int type, int amount)
        {
            var data = new ShoppingCart
            {
                ProductPrice = type, Quantity = amount, Transaction = Transaction.GetTransaction()
            };

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
            var data = Utils.GetClient();
            return data != null ? View(data) : View(new Client(){Discount = string.Empty});
        }
        
        /// <summary>
        /// Metodo que realiza el regtsiro del cliente.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ClientRegister(Client data)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult<ActionResult>(View(data));
            }

            try
            {
                if (!data.Discount.Equals(" "))
                {
                    var existsDiscount = await _administrationMethods.ExistsDiscount(data.Discount);
                    switch (existsDiscount)
                    {
                        case true:
                            Utils.SetDiscountCode(data.Discount);
                            break;

                        case false:
                            ModelState.AddModelError("", "¡El tiquete de descuento no es valido!.\n");
                            return await Task.FromResult<ActionResult>(View());

                        default:
                            ModelState.AddModelError("", "¡Error! Ha ocurrido un error.\n");
                            return await Task.FromResult<ActionResult>(View());
                    }
                }
                else
                {
                    Utils.SetDiscountCode(String.Empty);
                    data.Discount = string.Empty;    
                }
            }
            catch (Exception)
            {
                data.Discount = string.Empty;
                Utils.SetDiscountCode(string.Empty);
            }
            Utils.SetClient(data);
            return RedirectToAction("PayMethod", "Shoping");
        }

        /// <summary>
        /// Este metodo se encarga de cancelar todas las ordenes hechos por el usuario.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CancelOrder()
        {
            await _clientMethods.UpdateTransactionStatus(Transaction.GetTransaction(), 2);
            Transaction.DeleteTransaction();
            Shopping.DeleteOrders();
            Utils.DeleteData();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> PayMethod()
        {
            Shopping.AmountShoping();
            //decimal discountValue = subtotalValue - (decimal) (discount / 100.0);
            
            Utils.SetSubTotal(Shopping.GetAmount());    
            if (Utils.GetDiscount() != -1)
            {
                Utils.SetDiscount(await _administrationMethods.ReturnDiscountValue(Utils.GetDiscountCode()));
            } 
            return View();
        }
        
        public ActionResult MethodCash()
        {
            ViewBag.Subtotal = Utils.GetSubTotal();
            if (Utils.GetDiscount() != -1)
            {
                ViewBag.Discount = Utils.GetDiscount();
                ViewBag.Total = Utils.GetSubTotal() - ( (decimal) Utils.GetDiscount() / 100);    
            }
            return View();
        }

        public async Task<ActionResult> MethodCard()
        {
            var cardTypeList = await _administrationMethods.CardTypes();
            ViewBag.VBCardTypeList = new SelectList(cardTypeList, "PkCode", "Type");
            ViewBag.Subtotal = Utils.GetSubTotal();
            ViewBag.Discount = Utils.GetDiscount();
            ViewBag.Total = Utils.GetSubTotal() - ( (decimal) Utils.GetDiscount() / 100);
            return View();
        }
        
        public async Task<ActionResult> MethodCard(Card_Client data)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Datos incompletos:\n");
                return View(data);
            }

            var result = await _clientMethods.InsertCard(data);
            if (result) return RedirectToAction("PayPage", "Shoping");
            ModelState.AddModelError("", "Ha ocurrido un error al realizar la transaccion.\n");
            return View(data);
        }

        public ActionResult MethodCheck()
        {
            throw new System.NotImplementedException();
        }
        
        public ActionResult PayPage()
        {
            return View();
        }

        public ActionResult ShoppingList()
        {
            return View();
        }
        
        public ActionResult DeleteProductShopping(int id)
        {
            Shopping.DeletePurchase(id);
            return RedirectToAction("ShoppingList");
        }
    }
}