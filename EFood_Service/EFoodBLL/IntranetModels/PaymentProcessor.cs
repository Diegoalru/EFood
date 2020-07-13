using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.IntranetModels
{
    public class PaymentProcessor
    {
        [Required(ErrorMessage = "Debe incluir el nombre del procesador")]
        [DisplayName("Procesador")]
        [MaxLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres.")]
        public string ProcessorName { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar el medio de pago.")]
        [DisplayName("Tipo")]
        public int PaymentType { get; set; }
        
        [DisplayName("Estado")]
        public bool IsActive { get; set; }
    }

    public class PaymentChanges
    {
        public int PkCode { get; set; }
        
        [Required(ErrorMessage = "Debe incluir el nombre del procesador")]
        [DisplayName("Procesador")]
        [MaxLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres.")]
        public string NewProcessorName { get; set; }
        
        [DisplayName("Estado")]
        public bool NewStatus { get; set; }
    }

    public class ReturnPaymentProcessor
    {
        public int PkCode { get; set; }
        
        [DisplayName("Codigo")]
        public string? Code { get; set; }
        
        [DisplayName("Procesador")]
        public string Processor { get; set; }
        
        [DisplayName("Tipo")]
        public int PaymentType { get; set; }
        
        [DisplayName("Estado")]
        public bool Status { get; set; }
    }

    public class PaymentProcessorList
    {
        public int PkCode { get; set; }
        
        [DisplayName("Codigo")]
        public string Code { get; set; }
        
        [DisplayName("Procesador")]
        public string ProcessorName { get; set; }
        
        [DisplayName("Tipo")]
        public string Type { get; set; }
        
        [DisplayName("Estado")]
        public bool Status { get; set; }
    }
}