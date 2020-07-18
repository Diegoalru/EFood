using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.IntranetModels
{
    public class PriceType
    {
        [Required(ErrorMessage = "El nombre del tipo de precio en necesario")]
        [DisplayName("Tipo")]
        [MaxLength(30,ErrorMessage = "No puede exceder los 30 carácteres.")]
        public string Type { get; set; }
    }

    public class PriceTypeList
    {
        public int PkCode { get; set; }
        
        [DisplayName("Codigo")]
        public string Code { get; set; }
        
        [DisplayName("Tipo")]
        public string Type { get; set; }
    }

    public class ReturnPriceType
    {
        public int PkCode { get; set; }
        
        [DisplayName("Codigo")]
        public string Code { get; set; }
        
        [DisplayName("Tipo")]
        public string Type { get; set; }
    }

    public class PriceTypeEdit
    {
        public int PkCode { get; set; }
        [DisplayName("Codigo")]
        public string Code { get; set; }
        [DisplayName("Tipo")]
        public string Type { get; set; }
    }
}