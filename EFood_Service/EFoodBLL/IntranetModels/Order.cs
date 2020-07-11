using System;
using System.ComponentModel;

namespace EFoodBLL.IntranetModels
{
    
    /// <summary>
    /// Se puede utilizar para cambiar el estado de la orden.
    /// </summary>
    [Obsolete(message:"Esta clase debe ser utlizada solo en el cliente.")]
    public class OrderStatus
    {
        public int PkCode { get; set; }
        
        /// <summary>
        /// Indica el estado de la orden
        /// </summary>
        public int State { get; set; }
    }

    /// <summary>
    /// Clase que contiene las propiedades base para poder crear la lista de los pedidos.
    /// </summary>
    public class OrderList
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int PkCode { get; set; }
        
        /// <summary>
        /// Codigo de la transaccion
        /// </summary>
        [DisplayName("Transaccion")]
        public string TransactionId { get; set; }
        
        /// <summary>
        /// Estdo del Pedido. Ejemplo: En curso, Procesada o Cancelada.
        /// </summary>
        [DisplayName("Estado")]
        public string Status { get; set; }
        
        /// <summary>
        /// Fecha y Hora en el que se realizó la compra. 
        /// </summary>
        [DisplayName("Fecha y Hora")]
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Esto es el total que se debe pagar.
        /// </summary>
        [DisplayName("Precio Bruto")]
        public decimal GrossProfit { get; set; }
        
        /// <summary>
        /// Muestra la cantidad de descuento realizado.
        /// </summary>
        [DisplayName("Descuento Porcentaje")]
        public string DiscountPercentage { get; set; }
        
        /// <summary>
        /// Muestra el total que se desconto a la compra. Este retornará -1 en caso de que no tenga un descuento.
        /// </summary>
        [DisplayName("Descuento Monto")]
        public decimal Discount { get; set; }
        
        [DisplayName("Total a Pagar")]
        public decimal Total { get; set; }
    }
}