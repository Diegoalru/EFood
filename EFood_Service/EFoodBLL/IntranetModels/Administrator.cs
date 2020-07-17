using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.IntranetModels
{
    /// <summary>
    /// Esta clase contiene los datos necesarios para el login.
    /// </summary>
    public class Login
    {
        [Required(ErrorMessage = "Usuario no ingresado.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Clave no ingreada.")]
        public string Password { get; set; }
    }

    /// <summary>
    /// Clase que contiene los datos necesarios para hacer el cambio de clave de un usuario.
    /// Este o sus propiedades pueden usarse en los metodos <example>ComparePasswords y UpdatePassword</example>. 
    /// </summary>
    public class PasswordChange
    {
        public string Username { get; set; }
        public string ActualPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
    }

    /// <summary>
    /// Contiene los datos para ingresar la nformacion de un usuario.
    /// </summary>
    public class Register
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Question { get; set; }
        public string Answer { get; set; }
    }

    /// <summary>
    /// Contiene las propiedades basicas para ver el listado total de los usuarios registrados.
    /// Puede usarse en los metodos <example>TotalUsers</example>
    /// </summary>
    public class UsersList
    {
        public int PkCode { get; set; }
        
        [DisplayName("Usuario")]
        public string Username { get; set; }
    }

    /// <summary>
    /// Contiene los datos para poder actualizar el estado de un usuario.
    /// </summary>
    public class UserStatus
    {
        [DisplayName("Usuario")]
        public string Username { get; set; }
        
        [DisplayName("Estado")]
        public bool NewStatus { get; set; }
    }
}