namespace EFoodBLL.IntranetModels
{
    public class Product
    {
        public string Description { get; set; }
        public int LineType { get; set; }
        public string Content { get; set; }
    }

    public class ProductChanges
    {
        public int PkCode { get; set; }
        public string NewDescription { get; set; }
        public int NewLineType { get; set; }
        public string NewContent { get; set; }
    }

    public class ReturnProduct
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int LineTye { get; set; }
        public string Content { get; set; }
    }

    public class ProductList
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}