using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Intranet;
using Microsoft.Ajax.Utilities;

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
            var list = ConvertDStoList_Consecutives(_queryMethods.Consecutives().Result);
            return View(list);
        }

        public ActionResult ConsecutiveCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConsecutiveCreate(Consecutive data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = _existsMethods.ExistConsecutive(data.TypeConsecutive, data.Prefix).Result;
            switch (result)
            {
                case false:
                    var resultInsertConsecutive = await _insertMethods.InsertConsecutive(data);
                    if (resultInsertConsecutive)
                        return RedirectToAction("ConsecutiveList");

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El consecutivo ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }

        [HttpGet]
        public ActionResult ConsecutiveEdit(int id)
        {
            var consecutive = _returnMethods.ReturnConsecutive(id).Result;
            if (consecutive == null)
            {
                return HttpNotFound();
            }
            return View(consecutive);
        }

        [HttpPost]
        public Task<ActionResult> ConsecutiveEdit(ReturnConsecutive consecutive)
        {
            var result = _updateMethods.UpdateConsecutive(new ConsecutiveEdit
            {
                PkCode = consecutive.PkCode
                ,
                Prefix = consecutive.Prefix
            }).Result;

            if (!result)
            {
                ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
            }
            else
            {
                ModelState.AddModelError(key: "", errorMessage: "Guardado con exito.\n");
            }

            return Task.FromResult<ActionResult>(RedirectToAction("ConsecutiveList"));
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
            var result = _updateMethods.UpdateDiscount(new DiscountCupons
            {
                PkCode = discount.PkCode
                ,Description = discount.Description
                ,NewCupons = discount.Available
            }).Result;

            if (result)
            {
                
                return Task.FromResult<ActionResult>(RedirectToAction("DiscountList"));
            }
            ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
            return Task.FromResult<ActionResult>(View());
            
        }


        [HttpGet]
        public async Task<ActionResult> DiscountDelete(int id)
        {
            if (id == 0)
                return await Task.FromResult(new HttpStatusCodeResult(HttpStatusCode.BadRequest));

            var discount = _returnMethods.ReturnDiscount(id).Result;
            if (discount == null)
            {
                return await Task.FromResult(HttpNotFound());
            }

            return await Task.FromResult(View(discount));
        }

        [HttpGet]
        public Task<ActionResult> DiscountDeleteConfirmed(int id)
        {
            _deleteMethods.DeleteDiscount(id);
            return Task.FromResult<ActionResult>(RedirectToAction("DiscountList"));
        }

        #endregion

        #region TypeCards
        
        [HttpGet]
        public ActionResult TypeCardList()
        {
            var list = ConvertDStoList_CardType(_queryMethods.CardsTypes().Result);
            return View(list);
        }
        
        [HttpGet]
        public ActionResult TypeCardCreate()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> TypeCardCreate(CardType data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = await _existsMethods.ExistsTypeOfCard(data.Type);
            
            switch (result)
            {
                case false:
                    var resultInsertLineType = await _insertMethods.InsertCardType(data);
                    if (resultInsertLineType)
                    {
                        ModelState.AddModelError(key: "", errorMessage: "Insertado correctamente.\n");
                        return RedirectToAction("TypeCardList");
                    }

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El tipo de tarjeta ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> TypeCardEdit(int id)
        {
            var cardType = await _returnMethods.ReturnCardType(id);
            if (cardType == null)
            {
                return HttpNotFound();
            }
            return await Task.FromResult<ActionResult>(View(cardType));
        }

        [HttpPost]
        public async Task<ActionResult> TypeCardEdit(ReturnCardType data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = await  _updateMethods.UpdateCardType(data.PkCode, data.Type);

            if (!result)
            {
                ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                return await Task.FromResult<ActionResult>(View(data));
            }
            return await Task.FromResult<ActionResult>(RedirectToAction("TypeCardList"));
        }

        [HttpGet]
        public async Task<ActionResult> TypeCardDelete(int id)
        {
            var result = await _deleteMethods.DeleteCardType(id);
            if(!result) ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
            return await Task.FromResult<ActionResult>(RedirectToAction("TypeCardList"));
            
        }

        #endregion

        #region LineType
        public ActionResult LineTypeList()
        {
            var list = ConvertDStoList_LineType(_queryMethods.LineType().Result);
            return View(list);
        }

        public ActionResult LineTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LineTypeCreate(LineType data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = _existsMethods.ExistsLineType(data.Type).Result;
            switch (result)
            {
                case false:
                    var resultInsertLineType = await _insertMethods.InsertLineType(data);
                    if (resultInsertLineType)
                        return RedirectToAction("LineTypeList");

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El tipo de comida ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }

        [HttpGet]
        public ActionResult LineTypeEdit(int id)
        {
            var lineType = _returnMethods.ReturnLineType(id).Result;
            if (lineType == null)
            {
                return HttpNotFound();
            }
            return View(lineType);
        }

        [HttpPost]
        public async Task<ActionResult> LineTypeEdit(ReturnLineType data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = await  _updateMethods.UpdateLineType(data.PkCode, data.Type);

            if (!result)
            {
                ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                return await Task.FromResult<ActionResult>(View(data));
            }
            return await Task.FromResult<ActionResult>(RedirectToAction("LineTypeList"));
        }

        [HttpGet]
        public Task<ActionResult> LineTypeDeleteConfirmed(int id)
        {
            _deleteMethods.DeleteLineType(id);
            return Task.FromResult<ActionResult>(RedirectToAction("LineTypeList"));
        }

        #endregion

        #region PayMethod
        [HttpGet]
        public ActionResult PayMethodList()
        {
            var list = ConvertDStoList_Processor(_queryMethods.PaymentProcessors().Result);
            return View(list);
        }

        [HttpGet]
        public ActionResult PayMethodCreate()
        {
            var list = ConvertDStoList_PayMethods(_queryMethods.PayMethods().Result);
            ViewBag.VBPayMethods = list;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PayMethodCreate(PaymentProcessor data)
        {
            
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = await _existsMethods.ExistsPaymentProcessor(data.ProcessorName);
            switch (result)
            {
                case false:
                    var resultInsert = await _insertMethods.InsertPaymentProcessor(data);
                    if (resultInsert)
                        return RedirectToAction("PayMethodList");

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El procesador ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }

        [HttpGet]
        public ActionResult PayMethodEdit(int id)
        {
            var list = ConvertDStoList_PayMethods(_queryMethods.PayMethods().Result);

            var processor = _returnMethods.ReturnPaymentProcessor(id).Result;

            if (processor == null)
                return HttpNotFound();

            if (processor.Code.IsNullOrWhiteSpace())
                processor.Code = "No posee";

            ViewBag.VBPayMethods = list;

            return View(processor);
        }

        [HttpPost]
        public Task<ActionResult> PayMethodEdit(ReturnPaymentProcessor data)
        {
            var result = _updateMethods.UpdatePaymentProcessor(new PaymentChanges
            {
                PkCode = data.PkCode
                ,
                NewStatus = data.Status
                ,
                NewProcessorName = data.Processor
                ,
                NewNameUI = data.NameUI
            }).Result;

            if (result) return Task.FromResult<ActionResult>(RedirectToAction("PayMethodList"));

            ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
            return Task.FromResult<ActionResult>(View());

        }

        [HttpGet]
        public Task<ActionResult> PayMethodDeleteConfirmed(int id)
        {
            _deleteMethods.DeletePaymentProcessor(id);
            return Task.FromResult<ActionResult>(RedirectToAction("PayMethodList"));
        }

        [HttpGet]
        public async Task<ActionResult> RelationCard(int id)
        {
            var list = await GetProcessorCardList(id);
            return View(list);
        }

        [HttpPost]
        public async Task<ActionResult> RelationCard(IEnumerable<RelationCardProcessor> data, int id)
        {
            try
            {
                var listPost = (List<RelationCardProcessor>)data;
                SetRelationList(listPost, id);
                return await Task.FromResult<ActionResult>(RedirectToAction("PayMethodList"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await Task.FromResult<ActionResult>(RedirectToAction("PayMethodList"));
            }

        }

        /// <summary>
        /// Este metodo es para poder obtener la lista de tarjetas relacionadas con el procesador o aquellas que esten sin procesador. 
        /// </summary>
        private async Task<List<RelationCardProcessor>> GetProcessorCardList(int id)
        {
            var list = new List<RelationCardProcessor>();

            var unusedCardslist = ConvertDStoList_TypeCard_RelationCard(await _queryMethods.CardsWithoutProcessor());
            var usedCardslist = ConvertDStoList_TypeCard_RelationCard(await _queryMethods.CardsWithProcessor(id));

            foreach (var item in unusedCardslist)
            {
                list.Add(new RelationCardProcessor()
                {
                    PkCode = item.PkCode
                    ,
                    Type = item.Type
                    ,
                    Status = false
                });
            }

            foreach (var item in usedCardslist)
            {
                list.Add(new RelationCardProcessor()
                {
                    PkCode = item.PkCode
                    ,
                    Type = item.Type
                    ,
                    Status = true
                });
            }
            return list;
        }

        private async void SetRelationList(List<RelationCardProcessor> data, int id)
        {
            /*
             * FIXME: Solucionar el error de que la lista obtenida (parametro data) se recibe como nula.
             */

            var originalValues = await GetProcessorCardList(id);
            for (int index = 0; index < originalValues.Count; index++)
            {
                var result = originalValues[index].Status ^ data[index].Status;
                if (!result)
                {
                    if (!originalValues[index].Status)
                    {
                        await _insertMethods.CreateRelation_Card_Processor(originalValues[index].PkCode, id);
                    }
                    else
                    {
                        await _deleteMethods.DeleteRelation_Card_Processor(originalValues[index].PkCode, id);
                    }
                }
            }
        }

        #endregion

        #region ProductPrice

        public ActionResult PriceProductList(int id)
        {
            var list = ConvertDStoList_ProductPriceList(_queryMethods.ProductPrices(id).Result);
            return View(list);
        }
        public ActionResult PriceProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PriceProductCreate(ReturnPrice data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = _existsMethods.ExistsPrice(data.Type, data.Product).Result;
            switch (result)
            {
                case false:
                    var resultInsertPriceProduct= await _insertMethods.InsertPrice(data);
                    if (resultInsertPriceProduct)
                        return RedirectToAction("PriceProductList");

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El precio ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }

        public ActionResult PriceProductEdit()
        {
            return View();
        }

        #endregion

        #region PriceType

        public ActionResult PriceTypeList()
        {
            var list = ConvertDStoList_PriceType(_queryMethods.PriceTypes().Result);
            return View(list);
        }

        public ActionResult PriceTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PriceTypeCreate(PriceType data)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(data));

            var result = _existsMethods.ExistsPriceType(data.Type).Result;
            switch (result)
            {
                case false:
                    var resultInsertPriceType = await _insertMethods.InsertPriceType(data);
                    if (resultInsertPriceType)
                        return RedirectToAction("PriceTypeList");

                    ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                    return await Task.FromResult<ActionResult>(View(data));

                case true:
                    ModelState.AddModelError("", "¡El tipo de precio ya existe!\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }
        
        [HttpGet]
        public ActionResult PriceTypeEdit(int id)
        {
            var priceType = _returnMethods.ReturnPriceType(id).Result;
            if (priceType == null)
            {
                return HttpNotFound();
            }
            return View(priceType);
        }

        [HttpPost]
        public async Task<ActionResult> PriceTypeEdit(ReturnPriceType priceType)
        {
            var result = await _updateMethods.UpdatePriceType(priceType.PkCode, priceType.Type);
            var returnPriceType = await _returnMethods.ReturnPriceType(priceType.PkCode);

            if (result)
            {
                return await Task.FromResult<ActionResult>(RedirectToAction("PriceTypeList"));
            }

            ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
            return View(returnPriceType);
        }

        [HttpGet]
        public async Task<ActionResult> PriceTypeDelete(int id)
        {
            var result = await _deleteMethods.DeletePriceType(id);
            if (!result) ModelState.AddModelError("", "¡Error al eliminar el tipo de precio!\n");
            return await Task.FromResult<ActionResult>(RedirectToAction("PriceTypeList"));
        }

        #endregion

        #region Produts

        [HttpGet]
        public ActionResult ProductList()
        {
            // Se realiza la consulta donde se obtiene los tipos de linea que existen.
            var lineTypelist = ConvertDStoList_LineType(_queryMethods.LineType().Result);

            // Si existiera algún tipo de linea, automaticamente se mostraria los productos relacionados al primer datos de la lista.
            var productList = lineTypelist.Count != 0 ? ConvertDStoList_Product(_queryMethods.ProductsByLineType(lineTypelist[0].PkCode).Result) : null;

            //Se crea el tipo de objeto SelectList, el cual es retornado a la pagina para mostrarse en el dropdown con el id typeLinelist (primer parametro)
            var selectList = new SelectList(lineTypelist, "PkCode", "Type");

            //Se guardan los datos en el ViewBag para luego obtenelos con el id VBTypeLineList
            ViewBag.VBTypeLineList = selectList;

            //Se envian los datos a la pagina
            return View(productList);
        }

        [HttpPost]
        public ActionResult ProductList(LineTypeList typeList)
        {
            // Se crea la lista que contiene los tipos de linea que existen.
            var lineTypelist = ConvertDStoList_LineType(_queryMethods.LineType().Result);

            //Se crea un campo para almacenar la llave de tipo de linea.
            int lineTypeCode = 0;

            //Se recorre la lista hasta encontrar el dato obtenido de la pagina.
            foreach (var item in lineTypelist)
            {
                if (item.Type.Equals(typeList.Type))
                    lineTypeCode = item.PkCode;
            }

            //Se crear la lista con los productos segun el tipo de linea.
            var productList = ConvertDStoList_Product(_queryMethods.ProductsByLineType(lineTypeCode).Result);

            //Se crea el objeto de tipo SelectList que será envia a el dropdown de la pagina, el cual obtendra el id de lineTypelist (primer parametro).
            var selectList = new SelectList(lineTypelist, "PkCode", "Type");

            //Se agrega el obteto selectList al ViewBag de la pagina.
            ViewBag.VBTypeLineList = selectList;

            //Se envia los datos obtenidos, en otras palabras aquí se retornan dos listas (tipos de linea y productos)
            return View(productList);
        }

        [HttpGet]
        public ActionResult ProductCreate()
        {
            var lineTypelist = ConvertDStoList_LineType(_queryMethods.LineType().Result);
            ViewBag.VBTypeLineList = lineTypelist;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProductCreate(Product product)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(product));

            var result = _existsMethods.ExistsProduct(product.Description).Result;
            switch (result)
            {
                case false:
                    var resultInsertProduct = await _insertMethods.InsertProduct(product);
                    if (resultInsertProduct)
                        return RedirectToAction("ProductList");

                        
                    return await Task.FromResult<ActionResult>(View(product));

                case true:
                    ModelState.AddModelError("", "¡La el producto ya existe! (Descripción)\n");
                    return await Task.FromResult<ActionResult>(View());

                default:
                    ModelState.AddModelError("", "¡Error! Conexion con servidor perdida.\n");
                    return await Task.FromResult<ActionResult>(View());
            }
        }

        [HttpGet]
        public ActionResult ProductEdit(int id)
        {
            var lineTypelist = ConvertDStoList_LineType(_queryMethods.LineType().Result);
            var product = _returnMethods.ReturnProduct(id).Result;

            if (product == null)
                return HttpNotFound();

            if (product.Code.IsNullOrWhiteSpace())
                product.Code = "No posee";

            ViewBag.VBTypeLineList = lineTypelist;

            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> ProductEdit(ReturnProduct product)
        {
            var lineTypelist = ConvertDStoList_LineType(_queryMethods.LineType().Result);
            ViewBag.VBTypeLineList = new SelectList(lineTypelist, "PkCode", "Type");

            if (!ModelState.IsValid)
                return await Task.FromResult<ActionResult>(View(product));

            var result = _updateMethods.UpdateProduct(new ProductChanges
            {
                PkCode = product.PkCode
                ,
                NewContent = product.Content
                ,
                NewDescription = product.Description
                ,
                NewLineType = product.LineType
            }).Result;

            if (!result)
            {
                ModelState.AddModelError(key: "", errorMessage: "Ha ocurrido un error.\n");
                return await Task.FromResult<ActionResult>(View(product));
            }
            return await Task.FromResult<ActionResult>(RedirectToAction("ProductList"));

        }

        [HttpGet]
        public Task<ActionResult> ProductDeleteConfirmed(int id)
        {
            _deleteMethods.DeleteProduct(id);
            return Task.FromResult<ActionResult>(RedirectToAction("ProductList"));
        }

        #endregion

        #region DataSetToList

        private List<DiscountList> ConvertDStoList_Discount(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<DiscountList> list = new List<DiscountList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new DiscountList
                {
                    PkCode = (int)(dr["CODE"])
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    Description = (string)dr["DESCRIPCION"]
                    ,
                    Available = (int)(dr["DISPONIBLES"])
                    ,
                    Discount = (int)(dr["DESCUENTO"])
                });
            }
            return list;
        }

        private List<ReturnPrice> ConvertDStoList_ProductPriceList(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<ReturnPrice> list = new List<ReturnPrice>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new ReturnPrice
                {
                    PkCode = (int)(dr["CODE"])
                    ,
                    Type = (int)dr["TIPO"]
                    ,
                    Product = (int)dr["PRODUCTO"]
                    ,
                    Amount = (decimal)dr["CANTIDAD"]
                });
            }
            return list;
        }

        private List<LineTypeList> ConvertDStoList_LineType(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<LineTypeList> list = new List<LineTypeList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new LineTypeList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }
        
        private List<TypeCardsList> ConvertDStoList_CardType(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<TypeCardsList> list = new List<TypeCardsList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new TypeCardsList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }

        private List<ProductList> ConvertDStoList_Product(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<ProductList> list = new List<ProductList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new ProductList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    Description = (string)dr["DESCRIPCION"]
                });
            }
            return list;
        }

        private List<ConsecutiveList> ConvertDStoList_Consecutives(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<ConsecutiveList> list = new List<ConsecutiveList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new ConsecutiveList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Type = (string)dr["TIPO"]
                    ,
                    ConsecutiveId = (int)dr["ID_CONSECUTIVO"]
                });
            }
            return list;
        }

        private List<PaymentProcessorList> ConvertDStoList_Processor(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<PaymentProcessorList> list = new List<PaymentProcessorList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new PaymentProcessorList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    ProcessorName = (string)dr["PROCESADOR"]
                    ,
                    Status = (bool)dr["ESTADO"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }

        private List<TypeCardsList> ConvertDStoList_TypeCard(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<TypeCardsList> list = new List<TypeCardsList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new TypeCardsList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }


        /// <summary>
        /// Este metodo es solo para RelationCard
        /// </summary>
        /// <param name="dataSet"></param>
        [Obsolete("Este metodo es solo para el controlador 'RelationCard'.", false)]
        private List<TypeCardsList> ConvertDStoList_TypeCard_RelationCard(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<TypeCardsList> list = new List<TypeCardsList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new TypeCardsList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }

        private List<PriceTypeList> ConvertDStoList_PriceType(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<PriceTypeList> list = new List<PriceTypeList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new PriceTypeList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Code = (string)dr["CODIGO"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }

        private List<PayMethodList> ConvertDStoList_PayMethods(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<PayMethodList> list = new List<PayMethodList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new PayMethodList
                {
                    PkCode = (int)dr["CODE"]
                    ,
                    Type = (string)dr["TIPO"]
                });
            }
            return list;
        }

        private List<TypeConsecutive> ConvertDStoList_TypeConsecutives(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<TypeConsecutive> list = new List<TypeConsecutive>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new TypeConsecutive
                {
                    PkCode = (int)dr["CODE"]
                    ,Type = (string)dr["TIPO"]
                });
            }
            return list;
        }

        #endregion
    }
}
