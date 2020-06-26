using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.ClientModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Client
{
    using static DateTime;
    public interface IClientMethods
    {
        #region Exists
        /// <summary>
        /// Verfica si existe el codigo de transaccion.
        /// </summary>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool?> ExistTransacction(string transactionId);
        
        /// <summary>
        ///  Verifica si ya fue usado un cheque.
        /// </summary>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool?> ExistCheck(int checkNum, string account);
        #endregion

        #region Inserts
        /// <summary>
        /// Crea una transaccion en la base de datos.
        /// </summary>
        /// <param name="transactionId">Numero o codigo de transaccion.</param>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool> CreateTransaction(string transactionId);
        
        /// <summary>
        /// Inserta un cliente en la base de datos.
        /// </summary>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool> InsertClient(Client client);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="card">Contiene las propiedabes basicas para insertar una tarjeta.</param>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool> InsertCard(Card_Client card);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="check">Contiene todos los datos del cheque.</param>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool> InsertCheck(Check check);
        #endregion

        #region Updates
        /// <summary>
        /// Actualiza el estado de una transaccion.
        /// </summary>
        /// <param name="transactionId">Codigo de la transaccion.</param>
        /// <param name="status">Estado de la transaccion.</param>
        /// <returns>Retorna un <code>boolean</code> con el resultado.</returns>
        Task<bool> UpdateTransactionStatus(string transactionId, int status);
        #endregion
        
        #region Utils
        /// <summary>
        /// Metodo que genera un codigo para la transaccion.
        /// </summary>
        /// <returns>Retorna el codigo de una transaccion.</returns>
        Task<string> CreateTransaction();
        #endregion
    }

    public class ClientMethods : IClientMethods
    {
        private readonly IDbSettings _settings = new EFoodCliente();

        public Task<bool?> ExistTransacction(string transactionId)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_TRANSACCION(N'{transactionId}');";
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

        public Task<bool?> ExistCheck(int checkNum, string account)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_TRANSACCION({checkNum}, N'{account}');";
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

        public Task<bool> CreateTransaction(string transactionId)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    using (var cmd = new SqlCommand("CREAR_TRANSACCION", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID_TRANSACCION", SqlDbType.NVarChar).Value = transactionId;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }    
        }

        public Task<bool> InsertClient(Client client)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_CLIENTE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NOMBRE", SqlDbType.NVarChar).Value = client.Name;
                        cmd.Parameters.Add("@APELLIDOS", SqlDbType.NVarChar).Value = client.Surname;
                        cmd.Parameters.Add("@DIRECCION", SqlDbType.NVarChar).Value = client.Direction;
                        cmd.Parameters.Add("@TELEFONO", SqlDbType.NVarChar).Value = client.Phone;
                        cmd.Parameters.Add("@DESCUENTO", SqlDbType.Int).Value = client.Discount;
                        cmd.Parameters.Add("@TRANSACCION", SqlDbType.NVarChar).Value = client.Transaction;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertCard(Card_Client card)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    using (var cmd = new SqlCommand("INSERTA_TARJETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NOMBRE_ASOCIADO", SqlDbType.NVarChar).Value = card.CardOwner;
                        cmd.Parameters.Add("@TARJETA", SqlDbType.NVarChar).Value = card.CardNumber;
                        cmd.Parameters.Add("@MES", SqlDbType.NVarChar).Value = card.Month;
                        cmd.Parameters.Add("@YEAR", SqlDbType.NVarChar).Value = card.Year;
                        cmd.Parameters.Add("@CVV", SqlDbType.NVarChar).Value = card.CVV;
                        cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = card.Type;
                        cmd.Parameters.Add("@MONTO", SqlDbType.Decimal).Value = card.Amount;
                        cmd.Parameters.Add("@TRANSACCION", SqlDbType.NVarChar).Value = card.Transaction;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertCheck(Check check)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    using (var cmd = new SqlCommand("INSERTA_CHEQUE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NUMERO", SqlDbType.NVarChar).Value = check.Number;
                        cmd.Parameters.Add("@CUENTA", SqlDbType.NVarChar).Value = check.Account;
                        cmd.Parameters.Add("@MONTO", SqlDbType.Decimal).Value = check.Amount;
                        cmd.Parameters.Add("@TRANSACCION", SqlDbType.NVarChar).Value = check.Transaction;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateTransactionStatus(string transactionId, int status)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ACTUALIZA_TRANSACCION", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID_TRANSACCION", SqlDbType.NVarChar).Value = transactionId;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = status;
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

        public Task<string> CreateTransaction()
        {
            var dt = Now;
            return Task.FromResult(dt.ToString("yyyyMMddHHmmssffff"));
        }
    }
}