namespace EFoodBLL.IntranetModels
{
    public class PaymentProcessor
    {
        public string ProcessorName { get; set; }
        public int PaymentType { get; set; }
        public bool IsActive { get; set; }
    }

    public class PaymentChanges
    {
        public int PkCode { get; set; }
        public string NewProcessorName { get; set; }
        public bool NewStatus { get; set; }
    }

    public class ReturnPaymentProcessor
    {
        public int PkCode { get; set; }
        public string? Code { get; set; }
        public string Processor { get; set; }
        public int PaymentType { get; set; }
        public bool Status { get; set; }
    }

    public class PaymentProcessorList
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string ProcessorName { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}