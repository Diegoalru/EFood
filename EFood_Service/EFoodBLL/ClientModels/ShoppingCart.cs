namespace EFoodBLL.ClientModels
{
    public class ShoppingCart
    {
        public string Transaction { get; set; } = null!;
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}