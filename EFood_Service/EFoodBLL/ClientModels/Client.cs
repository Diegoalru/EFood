using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.ClientModels
{
    /// <summary>
    /// Clase con propiedades para crear un cliente.
    /// </summary>
    public class Client
    {
        [MaxLength(length:32)]
        [DisplayName("Nombre")]
        [Required]
        public string Name { get; set; }
        
        [MaxLength(length:64)]
        [DisplayName("Apellidos")]
        [Required]
        public string Surname { get; set; }
        
        [MaxLength(length:255)]
        [DisplayName("Direccion")]
        [Required]
        public string Direction { get; set; }
        
        [MaxLength(length:8)]
        [DisplayName("Telefono")]
        [Required]
        public string Phone { get; set; }
        
        [MaxLength(length:6)] 
        [DisplayName("Tiquete de Descuento:")]
        public string? Discount { get; set; }
        
        [MaxLength(length:18)]
        public string Transaction { get; set; }
    }
}