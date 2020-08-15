using System;
using System.Collections.Generic;
using EFoodBLL.ClientModels;
using EFoodDB.EFood_Client;

namespace EFood_Client.UtilsMethdos
{
    public static class Shopping
    {
        private static readonly AdministrationMethods _administrationMethods = new AdministrationMethods();
        
        /// <summary>
        /// Lista que contiene las compras realizadas en la sesion.
        /// </summary>
        private static List<ShoppingCart> _listShopping = new List<ShoppingCart>();

        /// <summary>
        /// Variable que contiene la cantidad de dinero a pagar.
        /// Necesita ejecutar <code>AmountShoping</code> para actualizar el monto.
        /// </summary>
        private static decimal _amount;

        /// <summary>
        /// Este metodo retorna la cantidad de compras que se han hecho con el carrito.
        /// </summary>
        /// <returns>Retorna la cantidad de compras hechas hasta el momento.</returns>
        public static int GetNumOfPurchases() => _listShopping.Count;

        /// <summary>
        /// Se inserta los productos selleccionados por el usuario en la lista.
        /// </summary>
        /// <param name="data">Contiene la informacion del producto a comprar.</param>
        public static void InsertPurchase(ShoppingCart data) => _listShopping.Add(data);

        /// <summary>
        /// Muestra la cantidad de compras realizadas por el usuario.
        /// </summary>
        /// <returns>Retorna la lista de compras realizadas por el usuario.</returns>
        public static IEnumerable<ShoppingCart> ShowPurchases() => _listShopping;

        /// <summary>
        /// Elimina una compra de la lista.
        /// </summary>
        /// <param name="index">Indice de la compra que se desa eliminar.</param>
        /// <returns>Retorna un booleano que indica is el elemento fue eliminado no.</returns>
        public static bool DeletePurchase(int index)
        {
            if (index < 0 || _listShopping.Count <= index) return false;
            _listShopping.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Elimina todas las ordenes guardadas hasta el momento.
        /// </summary>
        public static void DeleteOrders()
        {
            _listShopping.Clear();
            _amount = 0;
        }

        /// <summary>
        /// Actualiza el total a pagar.
        /// </summary>
        /// <returns>Actualiza el precio total a pagar.</returns>
        public static async void AmountShoping()
        {
            _amount = 0;
            foreach (var item in _listShopping)
            {
                _amount += item.Quantity * item.Price;
            }
            Console.WriteLine($"Total: {_amount}");
        }

        /// <summary>
        /// Devuelve el monto a pagar.
        /// </summary>
        /// <returns>Retorna el monto total a pagar.</returns>
        public static decimal GetAmount() => _amount;
        
    }
}