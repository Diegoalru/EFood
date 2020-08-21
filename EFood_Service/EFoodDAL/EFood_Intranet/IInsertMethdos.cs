using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.IntranetModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    /// <summary>
    /// Contiene todos las definiciones de los metodos que ayudaran con los registros en la base de datos.
    /// </summary>
    public interface IInsertMethods
    {
        /// <summary>
        /// Metodo que realiza la creación de un usuario en la base de datos.
        /// </summary>
        /// <param name="register">Objeto que contiene las propiedades minimas para crear un Usuario.</param>
        Task<bool> Register(Register register);
        
        /// <summary>
        /// Inserta un registro a la bitacora.
        /// </summary>
        /// <param name="log">Propiedades basicas de una Bitacora.</param>
        Task<bool> InsertLog(Log log);
        
        /// <summary>
        /// Crea un consecutivo para las distintas tablas de la base de datos.
        /// </summary>
        /// <param name="consecutive">Propiedades basicas para el registro de un Consecutivo.</param>
        Task<bool> InsertConsecutive(Consecutive consecutive);
        
        /// <summary>
        /// Crea un cupon de descuento.
        /// </summary>
        /// <param name="discount">Propiedades basicas para el registro de los cupones de Descuento.</param>
        Task<bool> InsertDiscount(Discount discount);
        
        /// <summary>
        /// Crea un registro del error sucedido en la aplicación.
        /// </summary>
        /// <param name="error">Propiedades basicas para el registro de un Error.</param>
        Task<bool> InsertError(Error error);
        
        /// <summary>
        /// Crea un registro de un precio.
        /// </summary>
        /// <param name="price">Propiedades basicas para el registro un Precio.</param>
        Task<bool> InsertPrice(Price price);
        
        /// <summary>
        /// Crea el registro de un Procesador de Pago.
        /// </summary>
        /// <param name="paymentProcessor">Propiedades basicas para el registro de un Procesador de Pago.</param>
        Task<bool> InsertPaymentProcessor(PaymentProcessor paymentProcessor);
        
        /// <summary>
        /// Crea el registro de un producto.
        /// </summary>
        /// <param name="product">Propiedades basicas para el registro de un Producto.</param>
        Task<bool> InsertProduct(Product product);
        
        /// <summary>
        /// Crea la relacion entre un tipo de tarjeta y un Procesador de Pago. <remarks>IMPORANTE:</remarks> Este toma el PkCode de el tipo de la tarjeta y el PkCode del Procesador de Pago.
        /// </summary>
        /// <param name="cardType">Esto es el PkCode de el Tipo de Tarjata.</param>
        /// <param name="processor">Esto es el PkCode de el Procesador de Pago.</param>
        Task<bool> CreateRelation_Card_Processor(int cardType, int processor);
        
        /// <summary>
        /// Crea el registro de un Tipo de Linea. 
        /// </summary>
        /// <param name="lineType">Propiedades basicas para el registro de un Tipo de Linea.</param>
        Task<bool> InsertLineType(LineType lineType);
        
        /// <summary>
        /// Crea el registro de un Tipo de Precio.
        /// </summary>
        /// <param name="priceType">Propiedades basicas para el registro de un Tipo de Precio.</param>
        Task<bool> InsertPriceType(PriceType priceType);
        
        /// <summary>
        /// Crea el registro de un Tipo de Tarjeta.
        /// </summary>
        /// <param name="cardType">Propiedades basicas para el registro de un Tipo de Tarjeta.</param>
        Task<bool> InsertCardType(CardType cardType);
    }

    public class InsertMethods : IInsertMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();
        public Task<bool> Register(Register register)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("REGISTRO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar).Value = register.Username;
                        cmd.Parameters.Add("@CLAVE", SqlDbType.NVarChar).Value = register.Password;
                        cmd.Parameters.Add("@PREGUNTA", SqlDbType.Int).Value = register.Question;
                        cmd.Parameters.Add("@RESPUESTA", SqlDbType.NVarChar).Value = register.Answer;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertLog(Log log)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_BITACORA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODIGO", SqlDbType.Int).Value = log.Code;
                        cmd.Parameters.Add("@MENSAJE", SqlDbType.NVarChar).Value = log.Message;
                        cmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar).Value = log.User;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertConsecutive(Consecutive consecutive)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_CONSECUTIVO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = consecutive.TypeConsecutive;
                        cmd.Parameters.Add("@ID_CONSECUTIVO", SqlDbType.Int).Value = consecutive.IdConsecutive;
                        cmd.Parameters.Add("@POSEE_PREFIJO", SqlDbType.Bit).Value = consecutive.HasPrefix;
                        cmd.Parameters.Add("@PREFIJO", SqlDbType.NVarChar).Value = consecutive.Prefix;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertDiscount(Discount discount)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_DESCUENTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODIGO", SqlDbType.NVarChar).Value = discount.Code;
                        cmd.Parameters.Add("@DESCRIPCION", SqlDbType.NVarChar).Value = discount.Description;
                        cmd.Parameters.Add("@DISPONIBLES", SqlDbType.Int).Value = discount.Available;
                        cmd.Parameters.Add("@DESCUENTO", SqlDbType.Int).Value = discount.Percentage;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertError(Error error)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_ERROR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODIGO", SqlDbType.Int).Value = error.Code;
                        cmd.Parameters.Add("@METODO", SqlDbType.NVarChar).Value = error.Method;
                        cmd.Parameters.Add("@MENSAJE", SqlDbType.NVarChar).Value = error.Message;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertPrice(Price price)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_PRECIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = price.PriceType;
                        cmd.Parameters.Add("@PRODUCTO", SqlDbType.Int).Value = price.Product;
                        cmd.Parameters.Add("@MONTO", SqlDbType.Decimal).Value = price.Amount; 
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertPaymentProcessor(PaymentProcessor paymentProcessor)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_PROCESADOR_PAGO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PROCESADOR", SqlDbType.NVarChar).Value = paymentProcessor.ProcessorName;
                        cmd.Parameters.Add("@TIPOPAGO", SqlDbType.Int).Value = paymentProcessor.PaymentType;
                        cmd.Parameters.Add("@NOMBRE_DISPLAY", SqlDbType.NVarChar).Value = paymentProcessor.NameUI;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = paymentProcessor.IsActive;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertProduct(Product product)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_PRODUCTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DESCRIPCION", SqlDbType.NVarChar).Value = product.Description;
                        cmd.Parameters.Add("@LINEA", SqlDbType.Int).Value = product.LineType;
                        cmd.Parameters.Add("@CONTENIDO", SqlDbType.NVarChar).Value = product.Content;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> CreateRelation_Card_Processor(int cardType, int processor)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_TARJETA_PROCESADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = cardType;
                        cmd.Parameters.Add("@PROCESADOR", SqlDbType.Int).Value = processor;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertLineType(LineType lineType)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_TIPO_LINEA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", SqlDbType.NVarChar).Value = lineType.Type;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertPriceType(PriceType priceType)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_TIPO_PRECIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", SqlDbType.NVarChar).Value = priceType.Type;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertCardType(CardType cardType)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_TIPO_TARJETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TIPO", SqlDbType.NVarChar).Value = cardType.Type;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }
    }
}