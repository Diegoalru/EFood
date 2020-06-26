using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.ClientModels
{
    /// <summary>
    /// Clase con propiedades para crear un cliente.
    /// </summary>
    public class Client
    {
        [MaxLength(length:32)]
        public string Name { get; set; }
        
        [MaxLength(length:64)]
        public string Surname { get; set; }
        
        [MaxLength(length:255)]
        public string Direction { get; set; }
        
        [MaxLength(length:8)]
        public string Phone { get; set; }
        
        [MaxLength(length:6)] 
        public string? Discount { get; set; }
        
        [MaxLength(length:18)] 
        public string Transaction { get; set; }
    }
}