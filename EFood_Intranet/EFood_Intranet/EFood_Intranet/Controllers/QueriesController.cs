using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Intranet;

namespace EFood_Intranet.Controllers
{
    public class QueriesController : Controller
    {
        private readonly IQueryMethods _queryMethods = new QueryMethods();
        
        public ActionResult Errors()
        {
            var list = ConvertDSToList_Errors(_queryMethods.Errors().Result);
            return View(list);
        }

        public ActionResult Logs()
        {
            var list = ConvertDSToList_Logs(_queryMethods.Logs().Result);
            return View(list);
        }

        public ActionResult Products()
        {
            /*var list = ConvertDSToList_LineType(_queryMethods.TypeLine().Result);
            return View(list);*/
            return View();
        }

        public ActionResult Orders()
        {
            var list = ConvertDSToList_Orders(_queryMethods.Orders().Result);
            return View(list);
        }

        #region DataSetToList
        private List<ErrorList> ConvertDSToList_Errors(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<ErrorList > list = new List<ErrorList >();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new ErrorList { 
                    PkCode = (int) dr["CODE"]
                    ,Message = Convert.ToString("MENSAJE")
                    ,ErrorCode = (int) dr["ERROR"]
                    ,DateTime = (DateTime) dr["FECHA"]
                });
            }
            return list;
        }
        
        private List<LogList> ConvertDSToList_Logs(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<LogList> list = new List<LogList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new LogList { 
                    PkCode = (int) dr["CODE"]
                    ,Code = (string) dr["CODIGO"]
                    ,Message = (string) dr["MENSAJE"]
                    ,User = (string) dr["USUARIO"]
                    ,DateTime = (DateTime) dr["FECHA"]
                });
            }
            return list;
        }
        
        private List<OrderList> ConvertDSToList_Orders(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<OrderList > list = new List<OrderList >();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new OrderList { 
                    PkCode = (int) dr[0]
                    ,TransactionId = (string) dr[1]
                    ,Status = (string) dr[2] 
                    ,DateTime = (DateTime) dr[3]
                    ,GrossProfit = (decimal) dr[4]
                    ,DiscountPercentage = (string) dr[5]
                    ,Discount = (decimal) dr[6]
                    ,Total = (decimal) dr[7]
                });
            }
            return list;
        }
        
        private List<LineTypeList> ConvertDSToList_LineType(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<LineTypeList > list = new List<LineTypeList >();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new LineTypeList { 
                    PkCode = (int) dr[0]
                    ,Code = (string) dr[1]
                    ,Type = (string) dr[2]
                });
            }
            return list;
        }
        
        private List<ProductList> ConvertDSToList_Products(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<ProductList > list = new List<ProductList >();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new ProductList { 
                    PkCode = (int) dr[0]
                    ,Code = (string) dr[1]
                    ,Description = (string) dr[2]
                });
            }
            return list;
        }
        #endregion
        
    }
}