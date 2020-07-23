using System.ComponentModel;

namespace EFoodBLL.ClientModels
{
    public class Product
    {
        public int PkCode { get; set; }
        
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        
        [DisplayName("Contenido")]
        public string Content { get; set; }
    }
}