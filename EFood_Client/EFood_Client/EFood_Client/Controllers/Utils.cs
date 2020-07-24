namespace EFood_Client.Controllers
{
    public class Utils
    {
        private static string Discount;
        public static string GetDiscount() => Discount;
        public static void SetDiscount(string discount) => Discount = discount;
    }
}