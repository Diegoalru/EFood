using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.IntranetModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    public interface IUpdateMethods
    {
        /*
         * TODO: Crear comentarios.
         */
        Task<bool> UpdateUserStatus(UserStatus userStatus);
        Task<bool> UpdateDiscount(DiscountCupons discountCupons);
        Task<bool> UpdateOrderStatus(OrderStatus orderStatus);
        Task<bool> UpdateProductPrice(ProductPrice productPrice);
        Task<bool> UpdatePaymentProcessor(PaymentChanges payment);
        Task<bool> UpdateProduct(ProductChanges product);
        Task<bool> UpdateUserRole(UserRole role);
        Task<bool> UpdateLineType(int pkCode, string type);
        Task<bool> UpdatePriceType(int pkCode, string type);
        Task<bool> UpdateCardType(int pkCode, string type);
    }

    public class UpdateMethods : IUpdateMethods
    {

        private readonly IDbSettings _settings = new EFoodAdministration();
        
        public Task<bool> UpdateUserStatus(UserStatus userStatus)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("ACTUALIZA_ESTADO_ADMINISTRADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar).Value = userStatus.Username;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = userStatus.NewStatus;
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

        public Task<bool> UpdateDiscount(DiscountCupons discountCupons)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_DESCUENTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = discountCupons.PkCode;
                        cmd.Parameters.Add("@DESCRIPCION", SqlDbType.NVarChar).Value = discountCupons.Description;
                        cmd.Parameters.Add("@DISPONIBLES", SqlDbType.Int).Value = discountCupons.NewCupons;
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

        public Task<bool> UpdateOrderStatus(OrderStatus orderStatus)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_ESTADO_PEDIDO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = orderStatus.PkCode;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = orderStatus.State;
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

        public Task<bool> UpdateProductPrice(ProductPrice productPrice)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_PRECIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PRODUCTO", SqlDbType.Int).Value = productPrice.Product;
                        cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = productPrice.Type;
                        cmd.Parameters.Add("@MONTO", SqlDbType.Decimal).Value = productPrice.Amount;
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

        public Task<bool> UpdatePaymentProcessor(PaymentChanges payment)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_PROCESADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = payment.PkCode;
                        cmd.Parameters.Add("@PROCESADOR", SqlDbType.NVarChar).Value = payment.NewProcessorName;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = payment.NewStatus;
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

        public Task<bool> UpdateProduct(ProductChanges product)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_PRODUCTO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = product.PkCode;
                        cmd.Parameters.Add("@DESCRIPCION", SqlDbType.NVarChar).Value = product.NewDescription;
                        cmd.Parameters.Add("@LINEA", SqlDbType.Int).Value = product.NewLineType;
                        cmd.Parameters.Add("@CONTENIDO", SqlDbType.NVarChar).Value = product.NewContent;
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

        public Task<bool> UpdateUserRole(UserRole role)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_ROL_ADMINISTRADOR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = role.Username;
                        cmd.Parameters.Add("@ADMINISTRADOR", SqlDbType.Bit).Value = role.IsAdministrator;
                        cmd.Parameters.Add("@SEGURIDAD", SqlDbType.Bit).Value = role.IsSecurity;
                        cmd.Parameters.Add("@MANTENIMIENTO", SqlDbType.Bit).Value = role.IsMaintenance;
                        cmd.Parameters.Add("@CONSULTA", SqlDbType.Bit).Value = role.IsAudit;
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

        public Task<bool> UpdateLineType(int pkCode, string type)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_TIPO_LINEA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
                        cmd.Parameters.Add("@TIPO", SqlDbType.NVarChar).Value = type;
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

        public Task<bool> UpdatePriceType(int pkCode, string type)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_TIPO_PRECIO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
                        cmd.Parameters.Add("@TIPO", SqlDbType.NVarChar).Value = type;
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

        public Task<bool> UpdateCardType(int pkCode, string type)
        {
            try
            {
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    using (var cmd = new SqlCommand("MODIFICA_TIPO_TARJETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CODE", SqlDbType.Int).Value = pkCode;
                        cmd.Parameters.Add("@TIPO", SqlDbType.NVarChar).Value = type;
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