using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using EFoodBLL.ClientModels;
using EFoodDB.EFood_Client;

namespace EFood_Client.Controllers
{
    /// <summary>
    /// Esta clase contiene las compras que se han realizado en la sesión actual.
    /// </summary>
    public  static class Shopping
    {
        private static readonly ClientMethods _clientMethods = new ClientMethods();
        private static readonly AdministrationMethods _administrationMethods = new AdministrationMethods();
        
        /// <summary>
        /// Lista que contiene las compras realizadas en la sesion.
        /// </summary>
        private static List<ShoppingCart> listShopping = new List<ShoppingCart>();

        /// <summary>
        /// Variable que contiene la cantidad de dinero a pagar.
        /// Necesita ejecutar <code>AmountShoping</code> para actualizar el monto.
        /// </summary>
        private static decimal Amount;

        /// <summary>
        /// Este metodo retorna la cantidad de compras que se han hecho con el carrito.
        /// </summary>
        /// <returns>Retorna la cantidad de compras hechas hasta el momento.</returns>
        public static int GetNumOfPurchases() => listShopping.Count;

        /// <summary>
        /// Se inserta los productos selleccionados por el usuario en la lista.
        /// </summary>
        /// <param name="data">Contiene la informacion del producto a comprar.</param>
        public static void InsertPurchase(ShoppingCart data) => listShopping.Add(data);

        /// <summary>
        /// Muestra la cantidad de compras realizadas por el usuario.
        /// </summary>
        /// <returns>Retorna la lista de compras realizadas por el usuario.</returns>
        public static IEnumerable<ShoppingCart> ShowPurchases() => listShopping;

        /// <summary>
        /// Elimina una compra de la lista.
        /// </summary>
        /// <param name="index">Indice de la compra que se desa eliminar.</param>
        /// <returns>Retorna un booleano que indica is el elemento fue eliminado no.</returns>
        public static bool DeletePurchase(int index)
        {
            if (index < 0 || listShopping.Count <= index) return false;
            listShopping.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Elimina todas las ordenes guardadas hasta el momento.
        /// </summary>
        public static void DeleteOrders()
        {
            listShopping = null;
            Amount = 0;
        }

        /// <summary>
        /// Actualiza el total a pagar.
        /// </summary>
        /// <returns>Actualiza el precio total a pagar.</returns>
        public static async void AmountShoping()
        {
            Amount = 0;
            foreach (var item in listShopping)
            {
                var prices = await _administrationMethods.ProductPrice(item.ProductPrice);
                Amount += item.Quantity * prices;
            }
        }

        /// <summary>
        /// Devuelve el monto a pagar.
        /// </summary>
        /// <returns>Retorna el monto total a pagar.</returns>
        public static decimal GetAmount() => Amount;
    }
}