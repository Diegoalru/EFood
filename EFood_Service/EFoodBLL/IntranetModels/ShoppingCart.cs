namespace EFoodBLL.IntranetModels
{
    public class ShoppingCart
    {
        public int PkCode { get; set; }
        public string? TransactionID { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
    }
}