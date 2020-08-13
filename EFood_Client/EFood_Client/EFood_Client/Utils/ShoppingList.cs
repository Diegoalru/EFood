using System.Collections.Generic;

namespace EFood_Client.Utils
{
    public class ShoppingList
    {
        private static List<CartItems> Cart = new List<CartItems>();
        //todo: metodos para ingresar y eliminar.
    }

    public class CartItems
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public int Price { get; set; }
    }
}