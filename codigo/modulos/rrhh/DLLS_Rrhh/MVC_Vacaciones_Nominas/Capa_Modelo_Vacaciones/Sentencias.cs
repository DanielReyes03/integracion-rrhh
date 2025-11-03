using System;
using System.Data;
using System.Data.Odbc;

namespace Capa_Modelo_Vacaciones
{
    public class Sentencias
    {
        private Conexion con = new Conexion();

        // Obtener vacaciones por empleado
        public DataTable ObtenerVacaciones(int idEmpleado)
        {
            DataTable dt = new DataTable();
            string query = "SELECT Cmp_iId_Vacacion, Cmp_iId_Empleado, Cmp_dFechaInicio_Vacacion, Cmp_dFechaFin_Vacacion, Cmp_iDias_Vacacion, Cmp_bAprobada_Vacacion FROM tbl_vacacion WHERE Cmp_iId_Empleado = ?";

            try
            {
                OdbcConnection conexion = con.ConexionDB();
                if (conexion != null)
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idEmpleado);
                        OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    con.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerVacaciones: " + ex.Message);
            }
            return dt;
        }

        // Insertar nueva solicitud
        public bool InsertarSolicitud(int idEmpleado, DateTime fechaInicio, DateTime fechaFin, int dias)
        {
            string query = "INSERT INTO tbl_vacacion (Cmp_iId_Empleado, Cmp_dFechaInicio_Vacacion, Cmp_dFechaFin_Vacacion, Cmp_iDias_Vacacion, Cmp_bAprobada_Vacacion) VALUES (?, ?, ?, ?, 0)";

            try
            {
                OdbcConnection conexion = con.ConexionDB();
                if (conexion != null)
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@emp", idEmpleado);
                        cmd.Parameters.AddWithValue("@ini", fechaInicio);
                        cmd.Parameters.AddWithValue("@fin", fechaFin);
                        cmd.Parameters.AddWithValue("@dias", dias);
                        cmd.Parameters.AddWithValue("@aprob", 0); // 0 = No aprobada
                        cmd.ExecuteNonQuery();
                    }
                    con.CerrarConexion();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar: " + ex.Message);
            }
            return false;
        }

        // Actualizar vacación
        public bool ActualizarVacacion(int idVacacion, DateTime fechaInicio, DateTime fechaFin, int dias)
        {
            string query = "UPDATE tbl_vacacion SET Cmp_dFechaInicio_Vacacion = ?, Cmp_dFechaFin_Vacacion = ?, Cmp_iDias_Vacacion = ? WHERE Cmp_iId_Vacacion = ?";

            try
            {
                OdbcConnection conexion = con.ConexionDB();
                if (conexion != null)
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ini", fechaInicio);
                        cmd.Parameters.AddWithValue("@fin", fechaFin);
                        cmd.Parameters.AddWithValue("@dias", dias);
                        cmd.Parameters.AddWithValue("@id", idVacacion);
                        cmd.ExecuteNonQuery();
                    }
                    con.CerrarConexion();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar: " + ex.Message);
            }
            return false;
        }

        // Obtener una vacación por ID
        public DataRow ObtenerVacacionPorId(int idVacacion)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM tbl_vacacion WHERE Cmp_iId_Vacacion = ?";

            try
            {
                OdbcConnection conexion = con.ConexionDB();
                if (conexion != null)
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idVacacion);
                        OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    con.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener vacación: " + ex.Message);
            }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // Eliminar vacación
        public bool EliminarVacacion(int idVacacion)
        {
            string query = "DELETE FROM tbl_vacacion WHERE Cmp_iId_Vacacion = ?";

            try
            {
                OdbcConnection conexion = con.ConexionDB();
                if (conexion != null)
                {
                    using (OdbcCommand cmd = new OdbcCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idVacacion);
                        cmd.ExecuteNonQuery();
                    }
                    con.CerrarConexion();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar: " + ex.Message);
            }
            return false;
        }
    }
}