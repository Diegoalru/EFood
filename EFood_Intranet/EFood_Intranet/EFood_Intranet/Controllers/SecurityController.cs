using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
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
        private readonly IExistsMethods _existsMethods = new ExistsMethods();
        private readonly IInsertMethods _insertMethods = new InsertMethods();
        private readonly IUtilsMethods _utilsMethods = new AdministrationUtils();
        
        [HttpGet]
        public async Task<ActionResult> ChangePasswordUsers()
        {
            var usersList = ConvertDStoList_TotalUsers(await _queryMethods.TotalUsers());
            return View(usersList);
        }

        [HttpGet]
        public async Task<ActionResult> ChangePasswordEdit(string username)
        {
            PasswordChange passwordChange = new PasswordChange() { Username = username };
            return View(passwordChange);
        }
        
        [HttpPost]
        public async Task<ActionResult> ChangePasswordEdit(PasswordChange data)
        {
            PasswordChange passwordChange = new PasswordChange() { Username = data.Username };

            if (!data.NewPassword.Equals(data.NewPasswordConfirmation, StringComparison.Ordinal))
            {
                ModelState.AddModelError(key: "", errorMessage: "Las contraseñas no coinciden.\n");
                return await Task.FromResult<ActionResult>(View(passwordChange));
            }
            
            var result = await _utilsMethods.ComparePassword(data.Username, data.ActualPassword);
            
            switch (result)
            {
                case true:
                    var resultUpdate = _utilsMethods.UpdatePassword(data.Username, data.NewPassword);
                    if (resultUpdate) return RedirectToAction("ChangePasswordUsers");
                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error al cambiar la contraseña.\n");
                    return await Task.FromResult<ActionResult>(View(passwordChange));
                
                case false:
                    ModelState.AddModelError(key: "", errorMessage: "La contraseña con coincide con la del sistema.\n");
                    return await Task.FromResult<ActionResult>(View(passwordChange));
                
                default:
                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(passwordChange));
            }
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var questionsList = ConverList_Questions(await _queryMethods.Questions());
            ViewBag.VBQuestionList = questionsList;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Register data)
        {
            var questionsList = ConverList_Questions(await _queryMethods.Questions());
            ViewBag.VBQuestionList = questionsList;
            
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            if (data.Password.Equals(data.PasswordConf, StringComparison.Ordinal))
            {
                ModelState.AddModelError(key: "", errorMessage: "Las contraseñas no coinciden.\n");
                return await Task.FromResult<ActionResult>(View(data));
            }

            var result = await _existsMethods.ExistsUser(data.Username);
            switch (result)
            {
                case false:
                    var resultInsert = await _insertMethods.Register(data);
                    
                    if (resultInsert)
                        return RedirectToAction("Users");

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El usuario ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
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
        
        [HttpGet]
        public ActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> RoleUserList()
        {
            var usersList = ConvertDStoList_TotalUsers(await _queryMethods.TotalUsers());
            return View(usersList);
        }

        [HttpGet]
        public async Task<ActionResult> RoleUserEdit(string user)
        {
            var userStatus = _returnMethods.ReturnRole(user).Result;
            
            if (userStatus != null) return View(userStatus);
            
            ModelState.AddModelError("", "¡El usuario no existe!\n");
            return RedirectToAction("RoleUserList");
        }
        
        [HttpPost]
        public ActionResult RoleUserEdit(string user, ReturnRole data)
        {
            var userStatus = _returnMethods.ReturnRole(data.Username).Result;
            var result = _updateMethods.UpdateUserRole(new UserRole()
            {
                Username = user, IsAdministrator = data.IsAdministrator, IsAudit = data.IsAudit, 
                IsMaintenance = data.IsMaintenance, IsSecurity = data.IsSecurity
            }).Result;

            if (!result)
            {
                ModelState.AddModelError("", "¡Error al guardar los datos!\n");
                return RedirectToAction("RoleUserList");
            }
            return RedirectToAction("RoleUserList");
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

        private List<QuestionList> ConverList_Questions(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<QuestionList> list = new List<QuestionList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new QuestionList()
                {
                    PkCode = (int) dr[0]
                    ,Question = (string) dr[1]
                });
            }
            return list;
        }
    }
}
