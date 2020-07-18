using System;
using System.Data;
using System.Data.SqlClient;

namespace EFoodDB.DBSettings
{
    public interface IDbSettings
    {
        /// <summary>
        /// Genera el <code>SQLConnection</code> de la base de datos. 
        /// </summary>
        /// <returns>Retorna el <code>SQLConnection</code> con las credenciales de la base de datos. </returns>
        SqlConnection GetConnection();
        
        /// <summary>
        /// Valida que exista conexion con el servidor.
        /// </summary>
        /// <returns>Retoraná un <code>True</code> en caso de que exista conexion con el servidor. </returns>
        bool ValidateConnection();
    }
    
    /// <summary>
    /// Esta clase contiene las propiedades para conectarse a la base de datos de la intranet.
    /// </summary>
    public class EFoodAdministration : IDbSettings
    {
        private readonly string _server = "den1.mssql7.gear.host";
        private readonly string _dbName = "efood";
        private readonly string _uid = "efood";
        private readonly string _psw = "Qr5wUp?4i~7u";
        private readonly string _appName = "Efood_Administration";

        /// <summary>
        /// Genera el <code>SQLConnection</code> de la base de datos. 
        /// </summary>
        /// <returns>Retorna el <code>SQLConnection</code> con las credenciales de la base de datos. </returns>
        public SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = _server
                ,ApplicationName = _appName 
                ,InitialCatalog = _dbName
                ,UserID = _uid
                ,Password = _psw
                ,ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled
            };
            
            return new SqlConnection(builder.ToString());
        }

        public bool ValidateConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return (conn.State & ConnectionState.Open) != 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
    /// <summary>
    /// Esta clase contiene las propiedades para conectarse a la base de datos del cliente.
    /// </summary>
    public class EFoodCliente : IDbSettings
    {
        private readonly string _server = "den1.mssql7.gear.host";
        private readonly string _dbName = "efoodcliente";
        private readonly string _uid = "efoodcliente";
        private readonly string _psw = "Wv91G!cy04~x";
        private readonly string _appName = "EFood_Cliente";

        /// <summary>
        /// Genera el <code>SQLConnection</code> de la base de datos. 
        /// </summary>
        /// <returns>Retorna el <code>SQLConnection</code> con las credenciales de la base de datos. </returns>
        public SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = _server
                ,ApplicationName = _appName 
                ,InitialCatalog = _dbName
                ,UserID = _uid
                ,Password = _psw
                //,ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled
            };
            return new SqlConnection(builder.ToString());
        }

        public bool ValidateConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return (conn.State & ConnectionState.Open) != 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}