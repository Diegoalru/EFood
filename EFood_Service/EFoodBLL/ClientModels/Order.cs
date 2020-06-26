namespace EFoodBLL.ClientModels
{
    public class Order
    {
        public string Transaction { get; set; }
        public string Discount { get; set; }
        public int Processor { get; set; }
        public int? CardType { get; set; }
    }
}