using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EFoodBLL.ClientModels;
using EFoodDB.DBSettings;

namespace EFoodDB.EFood_Client
{
    public interface IAdministrationMethods
    {
        
        /*
         *    Listas:
         *         Productos, precios, y tipos de linea.
         *         Medios de pagos.
         *         Tipos de tarjetas.
         *     Validaciones:
         *         Descuentos.
         *     Inserts:
         *         Pedidos.
         *         Caritos.
         */
        
        Task<List<LineType>> LineTypeList();
        Task<List<Product>> ProductList(int lineType);
        Task<List<Price>> ProductPrices(int product);
        Task<List<PayMethods>> PayMethods();
        Task<List<CardList>> CardTypes();
        Task<bool?> ExistsDiscount(string discount);
        Task<bool> InsertOrder(Order order);
        Task<bool> InsertShoppingCart(ShoppingCart cart);
    }

    public class AdministrationMethods : IAdministrationMethods
    {
        private readonly IDbSettings _settings = new EFoodAdministration();
        
        #region BDMethods

        public Task<List<LineType>> LineTypeList()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_TIPO_LINEA_CLIENTE;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ConvertToList_LineType(ds));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<Product>> ProductList(int lineType)
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    
                    string query = $@"SELECT * FROM V_PRODUCTOS_POR_TIPOLINEA_CLIENTE({lineType});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ConvertToList_Product(ds));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<Price>> ProductPrices(int product)
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $@"SELECT * FROM V_PRECIOS_PRODUCTO_CLIENTE({product});";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }

                return Task.FromResult(ConvertToList_Price(ds));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<PayMethods>>PayMethods()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $@"SELECT * FROM V_TIPO_PAGO_CLIENTE;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ConvertToList_PayMethod(ds));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<List<CardList>> CardTypes()
        {
            try
            {
                DataSet ds = new DataSet();
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = $@"SELECT * FROM V_TIPO_TARJETAS_CLIENTE;";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
                return Task.FromResult(ConvertToList_CardType(ds));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<bool?> ExistsDiscount(string discount)
        {
            try
            {
                Task<bool?> result;
                using (var conn = _settings.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    
                    var query = $"Select dbo.EXISTE_DESCUENTO(N'{discount}');";
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

        public Task<bool> InsertOrder(Order order)
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
                        cmd.Parameters.Add("@TRANSACCION", SqlDbType.NVarChar).Value = order.Transaction;
                        cmd.Parameters.Add("@DESCUENTO", SqlDbType.NVarChar).Value = order.Discount;
                        cmd.Parameters.Add("@PROCESADOR", SqlDbType.Int).Value = order.Processor;
                        cmd.Parameters.Add("@TIPO_TARJETA", SqlDbType.Int).Value = order.CardType;
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

        public Task<bool> InsertShoppingCart(ShoppingCart cart)
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
                        cmd.Parameters.Add("@TRANSACCION", SqlDbType.NVarChar).Value = cart.Transaction;
                        cmd.Parameters.Add("@PRECIO", SqlDbType.Int).Value = cart.ProductPrice;
                        cmd.Parameters.Add("@CANTIDAD", SqlDbType.Int).Value = cart.Quantity;
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

        #endregion

        #region ConvertDataSetToList

        private static List<LineType> ConvertToList_LineType(DataSet dataSet)
        {
            var ds = dataSet;
            var list = new List<LineType>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new LineType()
                {
                    PkCode = (int) (dr["CODE"])
                    ,Type = Convert.ToString(dr["TIPO"])
                });
            }
            return list;
        }

        private static List<Product> ConvertToList_Product(DataSet dataSet)
        {
            var ds = dataSet;
            var list = new List<Product>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Product()
                {
                    PkCode = (int) (dr["CODE"])
                    ,Description = Convert.ToString(dr["DESCRIPCION"])
                    ,Content = Convert.ToString(dr["CONTENIDO"])
                });
            }
            return list;
        }
        
        private static List<Price> ConvertToList_Price(DataSet dataSet)
        {
            var ds = dataSet;
            List<Price> list = new List<Price>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Price()
                {
                    PkCode = (int) (dr["CODE"])
                    ,Type = Convert.ToString(dr["TIPO"])
                    ,Amount = (decimal) (dr["MONTO"])
                });
            }
            return list;
        }
        
        private static List<PayMethods> ConvertToList_PayMethod(DataSet dataSet)
        {
            var ds = dataSet;
            List<PayMethods> list = new List<PayMethods>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new PayMethods()
                {
                    PkCode = (int) (dr["CODE"])
                    ,Type = Convert.ToString(dr["TIPO"])
                });
            }
            return list;
        }
        
        private static List<CardList> ConvertToList_CardType(DataSet dataSet)
        {
            var ds = dataSet;
            List<CardList> list = new List<CardList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new CardList()
                {
                    PkCode = (int) (dr["CODE"])
                    ,Type = Convert.ToString(dr["TIPO"])
                });
            }
            return list;
        }
        
        #endregion
    }
    
}