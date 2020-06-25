using System;

namespace EFoodBLL.IntranetModels
{
    public class OrderStatus
    {
        public int PkCode { get; set; }
        public int State { get; set; }
    }

    public class OrderList
    {
        public int PkCode { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Esto es el total que se debe pagar 
        /// </summary>
        public decimal GrossProfit { get; set; }
        /// <summary>
        /// Muestra la cantidad de descuento realizado.
        /// </summary>
        public string DiscountPercentage { get; set; }
        /// <summary>
        /// Muestra el total que se desconto a la compra. Este retornará -1 en caso de que no tenga un descuento.
        /// </summary>
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}