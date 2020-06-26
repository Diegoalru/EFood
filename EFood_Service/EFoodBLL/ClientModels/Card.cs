namespace EFoodBLL.ClientModels
{
    public class CardList
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
    }
    
    public class Card_Client
    {
        public string CardOwner { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string CVV { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public string Transaction { get; set; }
    }
}