namespace EFoodBLL.IntranetModels
{
    public class Discount
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Available { get; set; }
        public int Percentage { get; set; }
    }

    public class DiscountStatus
    {
        public int PkCode { get; set; }
        public bool Status { get; set; } = false;
    }

    public class ReturnDiscount
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Available { get; set; }
        public int Percentage { get; set; }
        public bool Status { get; set; }

    }

    public class DiscountList
    {
        public int PkCode { get; set; }
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