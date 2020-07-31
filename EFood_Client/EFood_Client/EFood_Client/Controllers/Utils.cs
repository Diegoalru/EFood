using System;
using System.CodeDom;
using System.Collections.Generic;
using EFoodBLL.ClientModels;

namespace EFood_Client.Controllers
{
    public static class Utils
    {
        #region Variables

        private static Client _client = null;
        private static string _discountCode;
        private static decimal _subtotal;
        private static int _discount = -1;

        #endregion
        
        #region Gets
        public static string GetDiscountCode() => _discountCode;
        public static decimal GetSubTotal() => _subtotal;
        public static int GetDiscount() => _discount;
        public static Client GetClient() => _client;
        #endregion

        #region Sets

        public static void SetDiscountCode(string discount) => _discountCode = discount;
        public static void SetSubTotal(decimal subtotal) => _subtotal = subtotal;
        public static void SetDiscount(int discount) => _discount = discount;
        public static void SetClient(Client data) => _client = data;

        #endregion

        public static void DeleteData()
        {
            SetSubTotal(0);
            SetDiscountCode(string.Empty);
            SetDiscount(-1);
            SetClient(null);
        }
    }
}