using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.ClientModels
{
    public class ShoppingCart
    {
        public string Transaction { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}