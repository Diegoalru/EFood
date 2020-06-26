using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.IntranetModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Intranet
{
    public interface IReturnMethods
    {
        Task<bool?> ReturnUserStatus(string username);
        /// <summary>
        /// Este metodo funciona para retornar la lista de articulos comprados relacionados con a una orden.
        /// </summary>
        /// <param name="pkOrder">Llave primaria de la orden.</param>
        /// <returns>Retorna la lista de compras realizadas con el numero de orden.</returns>
        Task<List<ShoppingCart>> ListShoppingCart(int pkOrder);
        Task<ReturnConsecutive> ReturnConsecutive(int pkConsecutive);
        Task<ReturnDiscount> ReturnDiscount(int pkDiscount);
        Task<ReturnPrice> ReturnPrice(int pkPrice);
        Task<ReturnPaymentProcessor> ReturnPaymentProcessor(int pkPaymentProcessor);
        Task<ReturnProduct> ReturnProduct(int pkProduct);
        Task<ReturnRole> ReturnRole(string username);
        Task<ReturnLineType> ReturnLineType(int pkLineType);
        Task<ReturnCardType> ReturnCardType(int pkCardType);
    }

    public class ReturnMethods : IReturnMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();
        
        public Task<bool?> ReturnUserStatus(string username)
        {
            try
            {
                Task<bool?> result = null;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_ESTADO_ADMINISTRADOR({username});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result = Task.FromResult((bool?) dr.GetBoolean(0));
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<ShoppingCart>> ListShoppingCart(int pkOrder)
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_CARRITOS_DE_PEDIDO({pkOrder});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                        {
                            dataAdapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ConvertDStoList_ShopingCart(ds));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        private List<ShoppingCart> ConvertDStoList_ShopingCart(DataSet dataSet)
        {
            DataSet ds = dataSet;
            List<ShoppingCart > list = new List<ShoppingCart >();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new ShoppingCart { PkCode = (int) (dr["CODE"]), TransactionID = Convert.ToString(dr["TRANSACCION"]), Amount = (decimal) (dr["MONTO"]), Quantity = (int) (dr["CANTIDAD"]) });
            }
            return list;
        }

        public Task<ReturnConsecutive> ReturnConsecutive(int pkConsecutive)
        {
            try
            {
                ReturnConsecutive consecutive = new ReturnConsecutive();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_CONSECUTIVO({pkConsecutive});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            consecutive.PkCode = dr.GetInt32(0);
                            consecutive.Type = dr.GetInt32(1);
                            consecutive.IdConsecutive = dr.GetInt32(2);
                            consecutive.HasPrefix = dr.GetBoolean(3);
                            consecutive.Prefix = (dr.IsDBNull(4) ? null : dr.GetString(4));
                            consecutive.NumConsecutive = dr.GetInt32(5);
                        }
                    }
                }
                return Task.FromResult(consecutive);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public Task<ReturnDiscount> ReturnDiscount(int pkDiscount)
        {
            try
            {
                ReturnDiscount discount = new ReturnDiscount();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_DESCUENTO({pkDiscount});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            discount.PkCode = dr.GetInt32(0);
                            discount.Code = dr.GetString(1);
                            discount.Description = dr.GetString(2);
                            discount.Available = dr.GetInt32(3);
                            discount.Percentage = dr.GetInt32(4);
                        }
                    }
                }
                return Task.FromResult(discount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<ReturnPrice> ReturnPrice(int pkPrice)
        {
            try
            {
                ReturnPrice price = new ReturnPrice();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_PRECIO({pkPrice});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            price.PkCode = dr.GetInt32(0);
                            price.Type = dr.GetInt32(1);
                            price.Product = dr.GetInt32(2);
                            price.Amount = dr.GetDecimal(3);
                        }
                    }
                }

                return Task.FromResult(price);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<ReturnPaymentProcessor> ReturnPaymentProcessor(int pkPaymentProcessor)
        {
            try
            {
                ReturnPaymentProcessor paymentProcessor = new ReturnPaymentProcessor();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_PROCESADOR({pkPaymentProcessor});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            paymentProcessor.PkCode = dr.GetInt32(0);
                            paymentProcessor.Code = (dr.IsDBNull(1) ? null: dr.GetString(1));
                            paymentProcessor.Processor = dr.GetString(2);
                            paymentProcessor.PaymentType = dr.GetInt32(3);
                            paymentProcessor.Status = dr.GetBoolean(4);
                        }
                    }
                }
                return Task.FromResult(paymentProcessor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<ReturnProduct> ReturnProduct(int pkProduct)
        {
            try
            {
                ReturnProduct product = new ReturnProduct();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_PRODUCTO({pkProduct});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            product.PkCode = dr.GetInt32(0);
                            product.Code = dr.GetString(1);
                            product.Description = dr.GetString(2);
                            product.LineTye = dr.GetInt32(3);
                            product.Content = dr.GetString(4);
                        }
                    }
                }
                return Task.FromResult(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<ReturnRole> ReturnRole(string username)
        { 
            try
            {
                ReturnRole role = new ReturnRole();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_ROL_ADMINISTRADOR(N'{username}');";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            role.IsAdministrator = dr.GetBoolean(0);
                            role.IsSecurity = dr.GetBoolean(1);
                            role.IsMaintenance= dr.GetBoolean(2);
                            role.IsAudit = dr.GetBoolean(3);
                        }
                    }
                }
                return Task.FromResult(role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<ReturnLineType> ReturnLineType(int pkLineType)
        {
            try
            {
                ReturnLineType lineType = new ReturnLineType();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_TIPO_LINEA({pkLineType});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lineType.PkCode = dr.GetInt32(0);
                            lineType.Type = dr.GetString(1);
                        }
                    }
                }

                return Task.FromResult(lineType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Task<ReturnCardType> ReturnCardType(int pkCardType)
        {
            try
            {
                ReturnCardType cardType = new ReturnCardType();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $"SELECT * FROM RETORNA_TIPO_TARJETA({pkCardType});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            cardType.PkCode = dr.GetInt32(0);
                            cardType.Type = dr.GetString(1);
                        }
                    }
                }

                return Task.FromResult(cardType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}