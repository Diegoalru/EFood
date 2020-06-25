namespace EFoodBLL.IntranetModels
{
    public class Consecutive
    {
        public int TypeConsecutive { get; set; }
        public int IdConsecutive { get; set; }
        public bool HasPrefix { get; set; }
        public string? Prefix { get; set; }
    }

    public class ReturnConsecutive
    {
        public int PkCode { get; set; }
        public int Type { get; set; }
        public int IdConsecutive { get; set; }
        public bool HasPrefix { get; set; }
        public string? Prefix { get; set; }
        public int NumConsecutive { get; set; }
    }

    public class ConsecutiveList
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
        public int ConsecutiveId { get; set; }
    }
}