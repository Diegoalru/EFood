using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Management;
using System.Web.Services;
using System.Xml.XPath;

namespace EService
{
    /// <summary>
    /// Descripción breve de TransportWS
    /// </summary>
    [WebService(Name = "EFoodService", Description = "Web Service. ULACIT IIC0 2020")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private SqlConnection GetConnection() => new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString); 
        
        /// <summary>
        /// Metodo para validar la conexion con la base de datos.
        /// </summary>
        /// <returns>Retorna un boolean con elestado de la conexion</returns>
        [WebMethod(Description = "Confirma el estado de la conexion con la base de datos.", MessageName = "Valida_Conexion")]
        public bool ValidateConnection()
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();
                return (conn.State & ConnectionState.Open) != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #if DEBUG
        /// <summary>
        /// Metodo de prueba.
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <returns>Retorna un saludo</returns>
        [WebMethod(Description = "Metodo de retorna un saludo.", MessageName = "Saludo")]
        public string Saludo(string nombre) => $"¡Hola {nombre}!";
        #endif
        
        #region Cuenta-Cheque
        
        #if  DEBUG
        [WebMethod]
        public int TestExisteCuenta(int cuenta) => ExisteCuenta(cuenta);

        [WebMethod]
        public int TestExisteCheque(int cuenta, string numero) => ExisteCheque(cuenta, numero);
        
        [WebMethod]
        public int TestRealizaRebajoCheque(int cuenta, decimal monto) => RealizaRebajoCheque(cuenta, monto);
        
        [WebMethod]
        public int TestActualizaSaldoCheque(int cuenta, decimal monto) => ActualizaSaldoCheque(cuenta, monto);

        [WebMethod]
        public int TestInsertaCuenta(int cuenta, decimal saldo) => InsertaCuenta(cuenta, saldo);
        
        [WebMethod]
        public int TestInsertaCheque(int cuenta, string numero) => InsertaCheque(cuenta, numero);
        
        #endif
        
        [WebMethod]
        public int ExisteCuenta(int cuenta)
        {
            int result;
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var query = $"SELECT dbo.EXISTE_CUENTA({cuenta});";
                using var cmd = new SqlCommand(query, conn);
                var response = Task.FromResult((bool) cmd.ExecuteScalar());
                result = response.Result ? 0 : 1;
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }
        
        [WebMethod]
        public int ExisteCheque(int cuenta, string numero)
        {
            int result;
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var query = $"Select dbo.EXISTE_NUMERO_CHEQUE({cuenta}, '{numero}');";
                using var cmd = new SqlCommand(query, conn);
                var response = Task.FromResult((bool) cmd.ExecuteScalar());

                result = response.Result ? 0 : 1;
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }
        
        [WebMethod]
        public int RealizaRebajoCheque(int cuenta, decimal monto)
        {
            int result;
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var query = $"SELECT dbo.VALIDA_REBAJO_CUENTA({cuenta}, {monto});";
                using var cmd = new SqlCommand(query, conn);
                var response = Task.FromResult((bool) cmd.ExecuteScalar());

                result = response.Result ? 0 : 1;
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }

        [WebMethod]
        public int ActualizaSaldoCheque(int cuenta, decimal monto)
        {
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed) conn.Open();

                using var cmd = new SqlCommand("ACTUALIZA_SALDO_CUENTA", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                cmd.Parameters.Add("@CUENTA", SqlDbType.Int).Value = cuenta;
                cmd.Parameters.Add("@MONTO", SqlDbType.Decimal).Value = monto;
                cmd.ExecuteNonQuery();
                conn.Close();

                return 0;
            }
            catch (Exception )
            {
                return 1;
            }
        }

        [WebMethod]
        public int InsertaCuenta(int cuenta, decimal saldo)
        {
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed) conn.Open();

                using var cmd = new SqlCommand("INSERTA_CUENTA_CHEQUE", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@CUENTA", SqlDbType.Int).Value = cuenta;
                cmd.Parameters.Add("@SALDO", SqlDbType.Decimal).Value = saldo;
                cmd.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        [WebMethod]
        public int InsertaCheque(int cuenta, string numero)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    using var cmd = new SqlCommand("INSERTA_CHEQUE", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@CUENTA", SqlDbType.Int).Value = cuenta;
                    cmd.Parameters.Add("@NUMERO", SqlDbType.VarChar).Value = numero;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion

        #region Tarjeta
        #if DEBUG
        [WebMethod]
        public int TestExisteTarjeta(string tarjeta) => ExisteTarjeta(tarjeta);

        [WebMethod]
        public int TestActualizaSaldoTarjeta(string tarjeta, decimal monto) => 
            ActualizaSaldoTarjeta(tarjeta, monto);

        [WebMethod]
        public int TestRealizaRebajoTarjeta(string tarjeta, decimal monto) =>
            RealizaRebajoTarjeta(tarjeta, monto);

        [WebMethod]
        public int TestValidaTarjeta(string tarjeta, string mes, string year, string cvv) =>
            ValidaTarjeta(tarjeta, mes, year, cvv);
        
        [WebMethod]
        public int TestInsertaTarjeta(string nombreAsociado, string tarjeta, string mes, string year, string cvv, int tipo, decimal saldo) =>
            InsertaTarjeta(nombreAsociado, tarjeta, mes, year, cvv, tipo, saldo);
        #endif
        
        [WebMethod]
        public int ExisteTarjeta(string tarjeta)
        {
            int result;
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var query = $"SELECT dbo.EXISTE_TARJETA('{tarjeta}');";
                using var cmd = new SqlCommand(query, conn);
                var response = Task.FromResult((bool) cmd.ExecuteScalar());

                result = response.Result ? 0 : 1;
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }
        
        [WebMethod]
        public int ActualizaSaldoTarjeta(string tarjeta, decimal monto)
        {
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed) conn.Open();

                using var cmd = new SqlCommand("ACTUALIZA_SALDO_TARJETA", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                cmd.Parameters.Add("@TARJETA", SqlDbType.VarChar).Value = tarjeta;
                cmd.Parameters.Add("@MONTO", SqlDbType.Decimal).Value = monto;
                cmd.ExecuteNonQuery();
                conn.Close();

                return 0;
            }
            catch (Exception)
            {
                return  1;
            }
        }
        
        [WebMethod]
        public int RealizaRebajoTarjeta(string tarjeta, decimal monto)
        {
            int result;
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var query = $"SELECT dbo.VALIDA_REBAJO_TARJETA('{tarjeta}', {monto});";
                using var cmd = new SqlCommand(query, conn);
                var response = Task.FromResult((bool) cmd.ExecuteScalar());

                result = response.Result ? 0 : 1;
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }

        [WebMethod]
        public int ValidaTarjeta(string tarjeta, string mes, string year, string cvv)
        {
            try
            {
                using var conn = GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var query = $"SELECT dbo.VALIDA_TARJETA('{tarjeta}', '{mes}', '{year}', '{cvv}');";
                using var cmd = new SqlCommand(query, conn);
                var result = Task.FromResult((int) cmd.ExecuteScalar());

                return result.Result;
            }
            catch (Exception)
            {
                return -6;
            }
        }
        
        [WebMethod]
        public int InsertaTarjeta(string nombreAsociado, string tarjeta, string mes, string year, string cvv, int tipo, decimal saldo)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    using var cmd = new SqlCommand("INSERTA_TARJETA", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@NOMBRE_ASOCIADO", SqlDbType.VarChar).Value = nombreAsociado;
                    cmd.Parameters.Add("@TARJETA", SqlDbType.VarChar).Value = tarjeta;
                    cmd.Parameters.Add("@MES", SqlDbType.NVarChar).Value = mes;
                    cmd.Parameters.Add("@YEAR", SqlDbType.NVarChar).Value = year;
                    cmd.Parameters.Add("@CVV", SqlDbType.NVarChar).Value = cvv;
                    cmd.Parameters.Add("@TIPO", SqlDbType.Int).Value = tipo;
                    cmd.Parameters.Add("@SALDO", SqlDbType.Decimal).Value = saldo;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        #endregion
    }
}
