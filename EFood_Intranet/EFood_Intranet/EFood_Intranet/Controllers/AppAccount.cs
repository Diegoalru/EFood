using System;
using System.Security.Cryptography.X509Certificates;
using EFoodBLL.IntranetModels;

namespace EFood_Intranet.Controllers
{
    public static class AppAccount
    {
        private static UserRole _userRole { get; set; } = null;

        public static void SetLogin(string loginUsername, ReturnRole userRoles)
        {
            _userRole = new UserRole()
            {
                Username = loginUsername, IsAdministrator = userRoles.IsAdministrator, IsAudit = userRoles.IsAudit,
                IsMaintenance = userRoles.IsMaintenance, IsSecurity = userRoles.IsSecurity
            };
        }
        
        
        /// <summary>
        /// Retorna el nombre de usuario y los roles asignados.
        /// </summary>
        public static UserRole GetLogin() { return _userRole; }

        public static bool IsLoggedIn()
        {
            return _userRole != null;
        }

        public static bool HasAdministrationRole()
        {
            return _userRole.IsAdministrator;
        }
        public static bool HasSecurityRole()
        {
            return _userRole.IsSecurity;
        }
        public static bool HasAuditRole()
        {
            return _userRole.IsAudit;
        }
        public static bool HasMaintenanceRole()
        {
            return _userRole.IsMaintenance;
        }
        
        
        
        public static void CloseSession()
        {
            _userRole = null;
        }
    }
}