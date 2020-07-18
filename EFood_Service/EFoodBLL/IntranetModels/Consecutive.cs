using System.ComponentModel;

namespace EFoodBLL.IntranetModels
{
    public class Consecutive
    {
        [DisplayName("Tipo Consecutivo")]
        public int TypeConsecutive { get; set; } 

        [DisplayName("ID Consecutivo")]
        public int IdConsecutive { get; set; }
        [DisplayName("Tiene Prefijo")]
        public bool HasPrefix { get; set; }
   
        [DisplayName("Prefijo")]
        public string? Prefix { get; set; }
    }

    public class ReturnConsecutive
    {
        
        public int PkCode { get; set; }
        [DisplayName("Tipo")]
        public int Type { get; set; }
        [DisplayName("ID Consecutivo")]
        public int IdConsecutive { get; set; }
        [DisplayName("Tiene Prefijo")]
        public bool HasPrefix { get; set; }
        [DisplayName("Prefijo")]
        public string? Prefix { get; set; }
        [DisplayName("Numero Consecutivo")]
        public int NumConsecutive { get; set; }
    }

    public class ConsecutiveList
    {
        
        public int PkCode { get; set; }
        [DisplayName("Tipo")]
        public string Type { get; set; }
        [DisplayName("Consecutivo")]
        public int ConsecutiveId { get; set; }
    }

    public class ConsecutiveEdit
    {
        public int PkCode { get; set; }
        public string Prefix { get; set; }
    }
}