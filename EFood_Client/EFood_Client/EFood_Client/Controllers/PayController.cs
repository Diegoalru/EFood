using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Services;
using EFood_Client.Models;
using EFoodBLL.ClientModels;
using EFoodDB.EFood_Client;
using EFood_Client.UtilsMethdos;
using EFood_Client.WebServiceEFood;

namespace EFood_Client.Controllers
{
    public class PayController : Controller
    {
        private readonly AdministrationMethods _administrationMethods = new AdministrationMethods();
        
        public async Task<ActionResult> PayMethod()
        {
            Shopping.AmountShoping();
            
            Utils.SetSubTotal(Shopping.GetAmount());    
            if (Utils.GetDiscountCode() != string.Empty)
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
                ViewBag.Total = Utils.GetSubTotal() + (Utils.GetSubTotal() * ( (decimal) Utils.GetDiscount() / 100));    
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> MethodCard()
        {
            var cardTypeList = await _administrationMethods.CardTypes();
            ViewBag.VBCardTypeList = new SelectList(cardTypeList, "PkCode", "Type");
            ViewBag.Subtotal = Utils.GetSubTotal();
            ViewBag.Discount = Utils.GetDiscount();
            ViewBag.Total = Utils.GetSubTotal() + (Utils.GetSubTotal() * ( (decimal) Utils.GetDiscount() / 100));
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> MethodCard(Card_Client data)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Datos incompletos:\n");
                return View(data);
            }

            return View(data);
        }

        [HttpGet]
        public ActionResult MethodCheck()
        {
            ViewBag.Subtotal = Utils.GetSubTotal();
            if (Utils.GetDiscount() != -1)
            {
                ViewBag.Discount = Utils.GetDiscount();
                ViewBag.Total = Utils.GetSubTotal() + (Utils.GetSubTotal() * ( (decimal) Utils.GetDiscount() / 100));    
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult MethodCheck(Check data)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Datos incompletos:\n");
                return View(data);
            }

            try
            {
                var service = new EFoodService();
                var result = service.ExisteCheque(data.Account, data.Number);
                switch (result)
                {
                    case 0:
                        var response = service.RealizaRebajoCheque(
                            data.Account
                            ,Utils.GetDiscount() != -1 ? 
                                Utils.GetSubTotal() :  
                                Utils.GetSubTotal() + (Utils.GetSubTotal() * ( (decimal) Utils.GetDiscount() / 100))
                            );

                        switch (response)
                        {
                            case 1:
                                UtilsMethdos.PayMethod.SetType(3);
                                UtilsMethdos.PayMethod.SetCheckClient(data);
                                return RedirectToAction("PayPage", "Pay");
                            
                            case 0:
                                ModelState.AddModelError("", "No hay fondos suficientes");
                                return View(data);
                            
                            default:
                                ModelState.AddModelError("", "Ha sucedido un error.");
                                return View(data);
                        }
                    case 1:
                        ModelState.AddModelError("", "¡Datos no validos!");
                        return View(data);

                    default:
                        ModelState.AddModelError("", "Error al conectar con la base de datos.\n");
                        return View(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError("", "Error al conectar con la base de datos.\n");
                return View(data);
            }
        }
        
        [HttpGet]
        public ActionResult PayPage()
        {
            var data = new PayData();
            return View(data);
        }
        
        public ActionResult Pay()
        {
            var administrationMethods = new AdministrationMethods();
            
            var responseOrder = administrationMethods.InsertOrder(new Order()
            {
                Transaction = Transaction.GetTransaction()
                ,CardType = (UtilsMethdos.PayMethod.GetType() == 2 ? UtilsMethdos.PayMethod.GetCardClient().Type : 0)
                ,Discount = Utils.GetDiscountCode()
                ,Processor = (UtilsMethdos.PayMethod.GetType() != 2 ? 
                    (
                        UtilsMethdos.PayMethod.GetType() == 1 ? 7 : 3
                    ) : 2)
                ,Status = 2
            }).Result;
            Console.WriteLine($"Resultado del registro del pedido: {responseOrder}");

            foreach (var item in Shopping.ShowPurchases())
            {
                var responseShopping=  administrationMethods.InsertShoppingCart(new ShoppingCart()
                {
                    Transaction = Transaction.GetTransaction()
                    ,ProductPrice = item.ProductPrice
                    ,Quantity = item.Quantity 
                    ,Name = string.Empty
                    ,Price = decimal.Zero
                }).Result;
                Console.WriteLine($"Carrito de producto: {item.Name}, ¿Realizado con exito? {responseShopping}");
            }

            var responseClient = administrationMethods.InsertClient(ClientUtils.GetClient()).Result;
            Console.WriteLine($"Registro del cliente: {responseClient}");
            return RedirectToAction("CancelOrder", "Home");
        }

        public ActionResult CancelPay()
        { 
            var administrationMethods = new AdministrationMethods();
            
            administrationMethods.InsertOrder(new Order()
            {
                Discount = null, Processor = null, CardType = null, Status = 3,
                Transaction = Transaction.GetTransaction()
            });
            
            return RedirectToAction("CancelOrder", "Home");
        }
    }
}