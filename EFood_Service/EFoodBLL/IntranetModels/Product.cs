using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EFoodBLL.IntranetModels
{
    public class Product
    {
        [Required(ErrorMessage = "Debe incluir la descripción")]
        [MaxLength(30)]
        [DisplayName("Descripción")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Debe incluir el tipo de linea del producto")]
        [DisplayName("Tipo de Linea")]
        public int LineType { get; set; }
        
        [Required(ErrorMessage = "Debe incluir el contenido")]
        [MaxLength(200)]
        [DisplayName("Contenido")]
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
        
        [DisplayName("Codigo")]
        public string Code { get; set; }
        
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Introduce una descripción")]
        public string Description { get; set; }
        
        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Selecciona un tipo de linea")]
        public int LineType { get; set; }
        
        [DisplayName("Contenido")]
        [Required(ErrorMessage = "Ingresa el contenido del producto")]
        public string Content { get; set; }
    }

    public class ProductList
    {
        public int PkCode { get; set; }
        
        [DisplayName("Codigo")]
        public string Code { get; set; }
        
        [DisplayName("Descripcion")]
        public string Description { get; set; }
    }
}