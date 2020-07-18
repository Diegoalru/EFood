using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFoodBLL.IntranetModels
{
    /// <summary>
    /// Contiene las propiedades basicas de los roles del usuario.
    /// </summary>
    public class UserRole
    {
        public string Username { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsSecurity { get; set; }
        public bool IsMaintenance { get; set; }
        public bool IsAudit { get; set; }
    }

    public class ReturnRole
    {
        [DisplayName("Usuario")] public string Username { get; set; }
        [DisplayName("Administrador")] public bool IsAdministrator { get; set; }
        [DisplayName("Seguridad")] public bool IsSecurity { get; set; }
        [DisplayName("Mantenimiento")] public bool IsMaintenance { get; set; }
        [DisplayName("Auditoria")] public bool IsAudit { get; set; }
    }
}