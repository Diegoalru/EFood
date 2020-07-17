using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Intranet;

namespace EFood_Intranet.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUtilsMethods _utilsMethods = new AdministrationUtils();
        private readonly IReturnMethods _returnMethods = new ReturnMethods(); 

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login login)
        {
            var result = await _utilsMethods.Login(login);
            switch (result)
            {
                case 1:

                    var userRoles = await _returnMethods.ReturnRole(login.Username);

                    if (userRoles != null)
                    {
                        AppAccount.SetLogin(userRoles);
                        return RedirectToAction("Index", "Home");    
                    }

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return View();
                    
                case 0:
                    ModelState.AddModelError(key: "", errorMessage: "Credenciales Incorectas\n");
                    return await Task.FromResult<ActionResult>(View());
                case -1:
                    ModelState.AddModelError(key: "", errorMessage: "El usuario no existe\n");
                    return await Task.FromResult<ActionResult>(View());
                default:
                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }

        [HttpGet]
        public ActionResult CloseSession()
        {
            AppAccount.CloseSession();
            return RedirectToAction("Index", "Home");
        }
    }
}