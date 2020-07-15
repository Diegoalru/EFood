using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    public interface IExistsMethods
    {
        /// <summary>
        /// Metodo para validar si existe un usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <returns>
        /// Retornará <code>true</code> en caso de existir el nombre de usuario ingresado.
        /// Ademas retornará NULL en caso de haber un error con el servidor.</returns>
        Task<bool?> ExistsUser(string username);

        /// <summary>
        /// Valida que no exista el consecutivo (segundo textbox pag 11)
        /// </summary>
        /// <param name="consecutive">Id del consecutivo brindado por el usuario.</
        /// <param name="prefix">Prefijo asignado por el usuario.</param>
        Task<bool?> ExistConsecutive(int consecutive, string prefix);
        
        /// <summary>
        /// Valida si existe el codigo para el Cupon.
        /// </summary>
        /// <param name="codeDiscount">Codigo del cupon. MAX 6 caracteres alfanumericos.</param>
        /// <returns>Retornar</returns>
        Task<bool?> ExistsDiscount(string codeDiscount);
        Task<bool?> ExistsPrice(int priceType, int product);
        Task<bool?> ExistsPaymentProcessor(string paymentProcessor);
        Task<bool?> ExistsProduct(string description);
        Task<bool?> ExistsLineType(string type);
        Task<bool?> ExistsTypeOfPrice(string type);
        Task<bool?> ExistsTypeOfCard(string type);
    }

    public class ExistsMethods : IExistsMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();
        public Task<bool?> ExistsUser(string username)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_USUARIO(N'{username}');";
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

        public Task<bool?> ExistConsecutive(int consecutive, string prefix)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_CONSECUTIVO({consecutive}, N'{prefix}');";
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

        public Task<bool?> ExistsDiscount(string codeDiscount)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_DESCUENTO(N'{codeDiscount}');";
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
        public Task<bool?> ExistsPrice(int priceType, int product)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_PRECIO({priceType}, {product});";
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
        public Task<bool?> ExistsPaymentProcessor(string paymentProcessor)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_PROCESADOR(N'{paymentProcessor}');";
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
        public Task<bool?> ExistsProduct(string description)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_PRODUCTO(N'{description}');";
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
        public Task<bool?> ExistsLineType(string type)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_TIPO_LINEA(N'{type}');";
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
        public Task<bool?> ExistsTypeOfPrice(string type)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_TIPO_PRECIO(N'{type}');";
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
        public Task<bool?> ExistsTypeOfCard(string type)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_TIPO_TARJETA(N'{type}');";
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
    }
}