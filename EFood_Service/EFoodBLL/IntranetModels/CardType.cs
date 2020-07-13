namespace EFoodBLL.IntranetModels
{
    public class CardType
    {
        public string Type { get; set; }
    }

    public class ReturnCardType
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
    }

    public class TypeCardsList
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }
}