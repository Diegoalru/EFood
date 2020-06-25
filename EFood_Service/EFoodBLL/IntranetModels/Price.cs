namespace EFoodBLL.IntranetModels
{
    public class Price
    {
        public int PriceType { get; set; }
        public int Product { get; set; }
        public decimal Amount { get; set; }
    }

    public class ProductPrice
    {
        public int Product { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
    }

    public class ReturnPrice
    {
        public int PkCode { get; set; }
        public int Type { get; set; }
        public int Product { get; set; }
        public decimal Amount { get; set; }
    }

    public class ProductPriceList
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}