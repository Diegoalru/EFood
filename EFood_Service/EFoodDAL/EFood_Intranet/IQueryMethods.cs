using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.IntranetModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    /// <summary>
    /// Contiene todos los metodos donde se realizarán las consutas.
    /// </summary>
    public interface IQueryMethods
    {
        /// <summary>
        /// Devuelve la totalidad de usuarios registrados.
        /// </summary>
        Task<DataSet> TotalUsers();
        
        /// <summary>
        /// Devulve todos los registros de la bitacora.
        /// </summary>
        Task<DataSet> Logs();
        
        /// <summary>
        /// Muestra el listado de consecutivos.
        /// </summary>
        Task<DataSet> Consecutives();
        
        /// <summary>
        /// Muestra las tablas a las que se puede asociar el consecutivo.
        /// </summary>
        Task<DataSet> ConsecutivesTypes();
        
        /// <summary>
        /// Muestra todos los descuentos que esten activos.
        /// </summary>
        Task<DataSet> Discounts();
        
        /// <summary>
        /// Muestra los errores que han sucedido en el sistema.
        /// </summary>
        Task<DataSet> Errors();
        
        /// <summary>
        /// Muestra todos los pedidos que han sido realizados.
        /// </summary>
        Task<DataSet> Orders();
        
        /// <summary>
        /// Muestra los precios asociados a un producto.
        /// </summary>
        /// <param name="pkProduct">PkCode del producto</param>
        Task<DataSet> ProductPrices(int pkProduct);
        
        /// <summary>
        /// Muestra las preguntas de seguridad.
        /// </summary>
        Task<DataSet> Questions();
        
        /// <summary>
        /// Muestra los Procesadores de Pago. 
        /// </summary>
        Task<DataSet> PaymentProcessors();

        /// <summary>
        /// Filtra los productos relacionados con un tipo especifico de linea.  
        /// </summary>
        /// <param name="lineType">PkCode del tipo de linea que se desea buscar.</param>
        Task<DataSet> ProductsByLineType(int lineType);
        
        /// <summary>
        /// Muestra los estados que posee una orden.
        /// </summary>
        Task<DataSet> StatusTypes();
        
        /// <summary>
        /// Muestra los tipos de linea de productos queisten.
        /// </summary>
        Task<DataSet> LineType();
        
        /// <summary>
        /// Muestra los medios de pago que exissten.
        /// </summary>
        Task<DataSet> PayMethods();
        
        /// <summary>
        /// Muestra los tipos de precio queisten para los productos.
        /// </summary>
        Task<DataSet> PriceTypes();
        
        Task<DataSet> CardsTypes();
        
        /// <summary>
        /// Muestra todas las tarjetas que no hayan sido utilizadas.
        /// </summary>
        Task<DataSet> CardsWithoutProcessor();

        /// <summary>
        /// Muestra todas las tarjetas que han sido utilizadas por un procesador.
        /// </summary>
        Task<DataSet> CardsWithProcessor(int pkProcessor);
    }

    public class QueryMethods : IQueryMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();

        public Task<DataSet> TotalUsers()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_ADMINISTRADORES;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> Logs()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_BITACORA;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<DataSet> Consecutives()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_CONSECUTIVO;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> ConsecutivesTypes()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_CONSECUTIVOS;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> Discounts()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_DESCUENTOS;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> Errors()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_ERRORES;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> Orders()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_PEDIDOS;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> ProductPrices(int pkProduct)
        {
            try
            {
                var ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $"SELECT * FROM V_PRECIOS_DE_PRODUCTO({pkProduct});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> Questions()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_PREGUNTAS;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> PaymentProcessors()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_PROCESADOR;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> ProductsByLineType(int lineType)
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_PRODUCTOS_POR_TIPOLINEA({lineType});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> StatusTypes()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_ESTADO;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> LineType()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_LINEA;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> PayMethods()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_PAGO;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> PriceTypes()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_PRECIO;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public Task<DataSet> CardsTypes()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_TARJETAS;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> CardsWithoutProcessor()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_TARJETAS_SIN_USAR;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<DataSet> CardsWithProcessor(int pkProcessor)
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_TARJETAS_USADAS_PROCESADOR({pkProcessor});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ds);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}