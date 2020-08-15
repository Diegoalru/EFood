using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFood_Client.UtilsMethdos;
using EFoodBLL.ClientModels;
using EFoodDB.EFood_Client;

namespace EFood_Client.Controllers
{
    public class ClientController : Controller
    {
        private readonly AdministrationMethods _administrationMethods = new AdministrationMethods();
        
        /// <summary>
        /// Retorna la vista del registro para el cliente.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClientRegister()
        {
            var data = ClientUtils.GetClient();
            
            //Si existen datos.
            return data != null ? View(data) : View(new Client()
            {
                Discount = string.Empty
            });
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
                    data.Discount = string.Empty;
                    Utils.SetDiscountCode(String.Empty);
                }
            }
            catch (Exception)
            {
                data.Discount = string.Empty;
                Utils.SetDiscountCode(string.Empty);
            }

            data.Transaction = Transaction.GetTransaction();
            ClientUtils.SetClient(data);
            return RedirectToAction("PayMethod", "Pay");
        }
    }
}