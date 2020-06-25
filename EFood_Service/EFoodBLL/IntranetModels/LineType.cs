using System.Runtime;

namespace EFoodBLL.IntranetModels
{
    public class LineType
    {
        public string Type { get; set; }
    }

    public class ReturnLineType
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
    }

    public class LineTypeList
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }
}