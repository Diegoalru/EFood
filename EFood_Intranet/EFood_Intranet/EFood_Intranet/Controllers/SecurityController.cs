using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Intranet;

namespace EFood_Intranet.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IQueryMethods _queryMethods = new QueryMethods();
        private readonly IUpdateMethods _updateMethods = new UpdateMethods();
        private readonly IReturnMethods _returnMethods = new ReturnMethods();
        
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        
        public ActionResult RoleUser()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> StatusUsers()
        {
            var usersList = ConvertDStoList_TotalUsers(await _queryMethods.TotalUsers());
            return View(usersList);
        }
        
        [HttpGet]
        public ActionResult StatusUserEdit(int id)
        {
            var userStatus = _returnMethods.ReturnUserStatus(id).Result;
            
            if (userStatus != null) return View(userStatus);
            
            ModelState.AddModelError("", "¡El usuario no existe!\n");
            return RedirectToAction("StatusUsers");
        }

        [HttpPost]
        public ActionResult StatusUserEdit(UsersList data)
        {
            var result = _updateMethods.UpdateUserStatus(new UserStatus()
            {
                Username = data.Username, NewStatus = data.Status
            }).Result;

            if (!result)
            {
                ModelState.AddModelError("", "¡Error al guardar los datos!\n");
                return RedirectToAction("StatusUsers");
            }
            return RedirectToAction("StatusUsers");
        }
        
        public ActionResult Users()
        {
            return View();
        }
        
        private List<UsersList> ConvertDStoList_TotalUsers(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<UsersList> list = new List<UsersList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new UsersList()
                {
                    PkCode = (int) dr[0]
                    ,Username = (string) dr[1]
                    ,Status = (bool) dr[2]
                });
            }
            return list;
        }
    }
}
