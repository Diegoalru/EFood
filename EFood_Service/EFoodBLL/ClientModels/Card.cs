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
        public string CardOwner { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string CVV { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public string Transaction { get; set; }
    }
}