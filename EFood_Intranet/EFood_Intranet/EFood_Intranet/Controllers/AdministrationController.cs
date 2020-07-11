using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Intranet;

namespace EFood_Intranet.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IQueryMethods _queryMethods = new QueryMethods();
        private readonly IReturnMethods _returnMethods = new ReturnMethods();
        private readonly IUpdateMethods _updateMethods = new UpdateMethods();
        private readonly IInsertMethods _insertMethods = new InsertMethods();
        private readonly IDeleteMethods _deleteMethods = new DeleteMethods();

        #region Consecutives
        public ActionResult ConsecutiveList()
        {
            return View();
        }
        
        public ActionResult ConsecutiveCreate()
        {
            return View();
        }
        
        public ActionResult ConsecutiveEdit()
        {
            return View(); 
        }
        #endregion

        #region Discount
        
        public ActionResult DiscountList()
        {
            var list = ConvertDStoList_Discount(_queryMethods.Discounts().Result);
            return View(list);
        }
        
        public ActionResult DiscountCreate()
        {
            ModelState.AddModelError(key: "",errorMessage: "¡Credenciales invalidas!");
            return View();
        }
        
        public ActionResult DiscountEdit()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<ActionResult> DiscountDelete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var discount = _returnMethods.ReturnDiscount(id).Result;
            if (discount == null)
            {
                return HttpNotFound();
            }

            return View(discount);
        }
        
        [HttpGet]
        public Task<ActionResult> DiscountDeleteConfirmed(int id)
        {
            Console.WriteLine($"ID obtenido: {id}");
            var resut = _deleteMethods.DeleteDiscount(id).Result;
            return Task.FromResult<ActionResult>(RedirectToAction("DiscountList"));
        }

        #endregion

        #region TypeCards

        public ActionResult TypeCardList()
        {
            return View();
        }
        public ActionResult TypeCardCreate()
        {
            return View();
        }
        
        public ActionResult TypeCardEdit()
        {
            return View();
        }
        
        #endregion
        
        #region TypeLine
        public ActionResult TypeLineList()
        {
            return View();
        }

        public ActionResult TypeLineCreate()
        {
            return View();
        }
        
        public ActionResult TypeLineEdit()
        {
            return View();
        }

        #endregion

        #region PayMethod
        public ActionResult PayMethodList()
        {
            return View();
        }
        public ActionResult PayMethodCreate()
        {
            return View();
        }
        public ActionResult PayMethodEdit()
        {
            return View();
        }

        #endregion
        
        #region ProductPrice

        public ActionResult PriceProductList()
        {
            return View();
        }
        public ActionResult PriceProductCreate()
        {
            return View();
        }
        
        public ActionResult PriceProductEdit()
        {
            return View();
        }

        #endregion

        #region TypePrice
        public ActionResult TypePriceList()
        {
            return View();
        }
        
        public ActionResult TypePriceCreate()
        {
            return View();
        }
        
        public ActionResult TypePriceEdit()
        {
            return View();
        }

        #endregion
        
        #region Produts
        public ActionResult ProductList()
        {
            return View();
        }
        public ActionResult ProductCreate()
        {
            return View();
        }
        
        public ActionResult ProductEdit()
        {
            return View();
        }

        #endregion
        
        #region DataSetToList
        private List<DiscountList> ConvertDStoList_Discount(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<DiscountList > list = new List<DiscountList >();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new DiscountList { 
                    PkCode = (int) (dr["CODE"])
                    ,Code = Convert.ToString(dr["CODIGO"])
                    ,Description = Convert.ToString("DESCRIPCION")
                    ,Available = (int) (dr["DISPONIBLES"])
                    ,Discount = (int) (dr["DESCUENTO"]) 
                });
            }
            return list;
        }
        #endregion
    }
}