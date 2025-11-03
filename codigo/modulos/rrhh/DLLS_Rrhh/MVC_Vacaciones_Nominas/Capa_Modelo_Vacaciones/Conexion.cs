using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Vacaciones
{
    public class Conexion
    {
        private OdbcConnection conexion;

        public OdbcConnection ConexionDB()
        {
            try
            {
                string dsn = "DSN=bd_hoteleria";
                conexion = new OdbcConnection(dsn);
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
            return conexion;
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar conexión: " + ex.Message);
            }
        }
    }
}