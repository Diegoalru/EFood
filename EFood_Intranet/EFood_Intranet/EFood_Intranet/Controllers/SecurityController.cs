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
            
            dynamic mymodel = new ExpandoObject();
            mymodel.userList = usersList;
            ViewBag.VBUsersList = usersList;
            return View();
        }
        
        public async Task<ActionResult> StatusUsers(string user)
        {
            var usersList = ConvertDStoList_TotalUsers(await _queryMethods.TotalUsers());
            var status = await _returnMethods.ReturnUserStatus(user);

            ViewBag.VBUsersList = usersList;
            
            if (status == null)
            {
                return View();
            }
            
            return View(new UserStatus()
            {
                Username = user
                ,NewStatus = (bool) status
            });
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
                });
            }
            return list;
        }
        
    }
}
