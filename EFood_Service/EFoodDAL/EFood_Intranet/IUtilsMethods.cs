using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using EFoodBLL.IntranetModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    
    /// <summary>
    /// Esta clase contiene metodos varios de la base de datos.
    /// </summary>
    public interface IUtilsMethods
    {
        /// <summary>
        /// Este metodo valida el inicio de sesion de un usuario en la Intranet.
        /// </summary>
        /// <param name="data">Contiene los datos necesarios para el inicio de sesion.</param>
        /// <returns>Este retonrará datos de tipo <code>int</code>.</returns>
        Task<int> Login(Login data);

        /// <summary>
        /// Este metodo valida que la clave ingresada sea la actual del usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="actualPassword">Clave actual del usuario</param>
        /// <returns>Retornará <code>True</code> en caso de que la clave coincida con la clave actual del usuario.</returns>
        Task<bool?> ComparePassword(string username, string actualPassword);
        
        /// <summary>
        /// Actualiza la clave del usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="newPassword">Nueva clave del usuario.</param>
        /// <returns>Retorna <code>True</code>  en caso de que se haya completado el cambio de clave.</returns>
        bool UpdatePassword(string username, string newPassword);
        
        /// <summary>
        /// Este metodo genera codigos automaticos al crear cupones de descuento.
        /// </summary>
        /// <returns>Retorna una cadena de texto con el valor del codigo unico para el cupón.</returns>
        string DiscountCodeGenerator();
    }

    public class AdministrationUtils : IUtilsMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();
        
        public Task<int> Login(Login data)
        {
            try
            {
                Task<int> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.LOGIN(N'{data.Username}', N'{data.Password}');";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        result = Task.FromResult((int) cmd.ExecuteScalar());
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return Task.FromResult(-4);
            }
        }
        
        public Task<bool?> ComparePassword(string username, string password)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.COMPARACION_CLAVES(N'{username}', N'{password}');";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        result = Task.FromResult((bool?) cmd.ExecuteScalar());
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public bool UpdatePassword(string username, string password)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ACTUALIZAR_CLAVE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar).Value = username;
                        cmd.Parameters.Add("@CLAVE", SqlDbType.NVarChar).Value = password;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public string DiscountCodeGenerator()
        {
            var code = new StringBuilder();
            var rnd = new Random();
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var length = 6;
            while (0 < length--)
            {
                code.Append(valid[rnd.Next(valid.Length)]);
            }
            return code.ToString();
        }
    }
}