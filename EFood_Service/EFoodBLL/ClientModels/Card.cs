using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.ClientModels
{
    /// <summary>
    /// Esta clase puede ser utilizada a la hora de mostrar la lista de tarjetas que existen en la base de datos.
    /// </summary>
    public class CardList
    {
        public int PkCode { get; set; }
        
        [Required(ErrorMessage = "Ingrese el nombre del tipo de tarjeta.")]
        [DisplayName("Tipo")]
        [MaxLength(30, ErrorMessage = "No puede contener más de 30 carácteres.")]
        public string Type { get; set; }
    }

    public class Card_Client
    {
        [Required(ErrorMessage = "* Nombre en el nombre del asociado")]
        [DisplayName("Nombre asociado")]
        [MaxLength(30)]
        public string CardOwner { get; set; }
        
        [Required(ErrorMessage = "* Numero de la tarjeta")]
        [DisplayName("Numero")]
        [MaxLength(16)]
        public string CardNumber { get; set; }
        
        [Required(ErrorMessage = "* Vencimiento: Mes")]
        [DisplayName("Mes")]
        [MaxLength(2)]
        public string Month { get; set; }
        
        [Required(ErrorMessage = "* Vencimiento: Año")]
        [DisplayName("Año")]
        [MaxLength(4)]
        public string Year { get; set; }
        
        [Required(ErrorMessage = "* CVV")]
        [DisplayName("CVV")]
        [MaxLength(3)]
        public string CVV { get; set; }
        
        [Required(ErrorMessage = "* Tipo Tarjeta")]
        [DisplayName("Tipo Tarjeta")]
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public string Transaction { get; set; }
    }
}