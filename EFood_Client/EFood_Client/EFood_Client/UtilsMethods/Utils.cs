namespace EFood_Client.UtilsMethdos
{
    public class Utils
    {
        #region Variables
        private static string _discountCode = string.Empty;
        private static decimal _subtotal;
        private static int _discount = -1;

        #endregion
        
        #region Gets
        public static string GetDiscountCode() => _discountCode;
        public static decimal GetSubTotal() => _subtotal;
        public static int GetDiscount() => _discount;
        #endregion

        #region Sets

        public static void SetDiscountCode(string discount) => _discountCode = discount;
        public static void SetSubTotal(decimal subtotal) => _subtotal = subtotal;
        public static void SetDiscount(int discount) => _discount = discount;

        #endregion

        public static void DeleteData()
        {
            SetSubTotal(0);
            SetDiscountCode(string.Empty);
            SetDiscount(-1);
        }
    }
}