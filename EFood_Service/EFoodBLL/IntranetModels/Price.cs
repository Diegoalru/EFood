using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace EFoodBLL.IntranetModels
{
    public class Price
    {
        [Required]
        [DisplayName("Tipo Precio")]
        public int PriceType { get; set; }
        public int Product { get; set; }
        [Required]
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
    }

    public class ProductPrice
    {
        public int Price { get; set; }
        public decimal Amount { get; set; }
    }

    public class ReturnPrice
    {
        public int PkCode { get; set; }
        
        public int TypeCod { get; set; }
        
        [DisplayName("Nombre")]
        public string Type { get; set; } = null!;

        [DisplayName("Producto")]
        public int Product { get; set; }
        
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
    }

    public class ProductPriceList
    {
        public int PkCode { get; set; }
        public int Product { get; set; }
        [DisplayName("Tipo")]
        public string Type { get; set; } = null!;
        [DisplayName("Monto")]
        public decimal Amount { get; set; }
    }
}