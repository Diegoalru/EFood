using System.ComponentModel;

namespace EFoodBLL.IntranetModels
{
    public class Discount
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Available { get; set; }
        public int Percentage { get; set; }
    }

    public class DiscountCupons
    {
        public int PkCode { get; set; }
        public int NewCupons { get; set; }
    }

    public class ReturnDiscount
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Available { get; set; }
        public int Percentage { get; set; }
    }

    public class DiscountList
    {
        public int PkCode { get; set; }
        
        [DisplayName("Codigo")]
        public string Code { get; set; }
        
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        
        /// <summary>
        /// Numero o porcentaje de descuento.
        /// </summary>
        public int Discount { get; set; }
        /// <summary>
        /// Cantidad de cupones disponibles.
        /// </summary>
        public int Available { get; set; }
    }
    
}