using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.IntranetModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    
    public interface IDeleteMethods
    {
        Task<bool> DeleteProductPrice(int pkProduct, int pkType);
        Task<bool> DeletePaymentProcessor(int pkCode);
        Task<bool> DeleteDiscount(int pkCode);
        Task<bool> DeleteProduct(int pkCode);
        Task<bool> DeleteRelation_Card_Processor(int card, int processor);
        Task<bool> DeleteLineType(int pkCode);
        Task<bool> DeletePriceType(int pkCode);
        Task<bool> DeleteCardType(int pkCode);
    }

    public class DeleteMethods : IDeleteMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();
        
        public Task<bool> DeleteProductPrice(int pkProduct, int pkType)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_PRECIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PRODUCTO", SqlDbType.Int).Value = pkProduct;
                        cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = pkType;
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

        public Task<bool> DeletePaymentProcessor(int pkCode)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_PROCESADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
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

        public Task<bool> DeleteDiscount(int pkCode)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("DESHABILITA_DESCUENTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
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

        public Task<bool> DeleteProduct(int pkCode)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_PRODUCTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
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

        public Task<bool> DeleteRelation_Card_Processor(int card, int processor)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_TARJETA_PROCESADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TARJETA", SqlDbType.Int).Value = card;
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

        public Task<bool> DeleteLineType(int pkCode)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_TIPO_LINEA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
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

        public Task<bool> DeletePriceType(int pkCode)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_TIPO_PRECIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
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

        public Task<bool> DeleteCardType(int pkCode)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ELIMINA_TIPO_TARJETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
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