namespace EFoodBLL.IntranetModels
{
    public class PriceType
    {
        public string Type { get; set; }
    }

    public class ReturnPriceType
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }

    public class PriceTypeList
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }
}