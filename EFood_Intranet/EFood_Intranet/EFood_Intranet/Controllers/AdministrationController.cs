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
        private readonly IExistsMethods _existsMethods = new ExistsMethods();
        
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
        
        /// <summary>
        /// Metodo que devuelve la vista con los datos de los descuentos. 
        /// </summary>
        public ActionResult DiscountList()
        {
            var list = ConvertDStoList_Discount(_queryMethods.Discounts().Result);
            return View(list);
        }
        
        /// <summary>
        /// Crear tiquete de descuento.
        /// </summary>
        public ActionResult DiscountCreate()
        {
            return View();
        }
        
        /// <summary>
        /// Crea los tiquetes de descuento.
        /// </summary>
        /// <param name="data">Informacion del cupón</param>
        [HttpPost]
        public async Task<ActionResult> DiscountCreate(Discount data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = _existsMethods.ExistsDiscount(data.Code).Result;
            switch (result)
            {
                case false:
                    var resultInsertDiscount = await _insertMethods.InsertDiscount(data);
                    if (resultInsertDiscount)
                        return RedirectToAction("DiscountList");
                    
                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));
                
                case true:
                    ModelState.AddModelError("", "¡El tiquete de descuento ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());
                
                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }
        
        /// <summary>
        /// Edita el Cupon.
        /// </summary>
        /// <param name="id">Id primario del cupón.</param>
        [HttpGet]
        public ActionResult DiscountEdit(int id)
        {
            var discount = _returnMethods.ReturnDiscount(id).Result;
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }
        
        
        /// <summary>
        /// Metodo que realiza la actualizacion del cupon.
        /// </summary>
        /// <param name="discount"></param>
        [HttpPost]
        public Task<ActionResult> DiscountEdit(ReturnDiscount discount)
        {
            var result =_updateMethods.UpdateDiscount(new DiscountCupons
            {
                PkCode = discount.PkCode
                ,Description = discount.Description
                ,NewCupons = discount.Available
            }).Result;

            Console.WriteLine(" Codigo: " + discount.PkCode);

            if (!result)
            {
                ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
            }
            else
            {
                ModelState.AddModelError(key: "", errorMessage: "Guardado con exito.\n");    
            }
            return Task.FromResult<ActionResult>(View());
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
            _deleteMethods.DeleteDiscount(id);
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
                    ,Code = (string) dr["CODIGO"]
                    ,Description = (string) dr["DESCRIPCION"]
                    ,Available = (int) (dr["DISPONIBLES"])
                    ,Discount = (int) (dr["DESCUENTO"]) 
                });
            }
            return list;
        }
        #endregion
    }
}
