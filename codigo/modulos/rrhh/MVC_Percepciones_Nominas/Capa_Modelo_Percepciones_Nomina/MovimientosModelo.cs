using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace Capa_Modelo_Percepciones_Nomina
{
    // 📂 Capa_Modelo_Percepciones_Nomina / MovimientosModelo.cs
    public class MovimientosModelo
    {
        public void InsertarMovimiento(int idNomina, int idConcepto, decimal monto)
        {
            Conexion cn = new Conexion();
            using (OdbcConnection con = cn.conexionDB())
            using (OdbcCommand cmd = new OdbcCommand(@"
            INSERT INTO `Tbl_MovimientosNomina`
                (`Cmp_iId_Nomina`, `Cmp_iId_ConceptoNomina`, `Cmp_deMonto_MovimientoNomina`)
            VALUES (?, ?, ?);", con))
            {
                cmd.Parameters.Add("p1", OdbcType.Int).Value = idNomina;
                cmd.Parameters.Add("p2", OdbcType.Int).Value = idConcepto;
                cmd.Parameters.Add("p3", OdbcType.Decimal).Value = monto;
                cmd.ExecuteNonQuery();
            }
            cn.cerrarConexion();
        }

        public void InsertarDetalleNomina(int idNomina, int idEmpleado)
        {
            Conexion cn = new Conexion();
            using (OdbcConnection con = cn.conexionDB())
            using (OdbcCommand cmd = new OdbcCommand(@"
            INSERT INTO `Tbl_DetallesNomina`
                (`Cmp_iId_Nomina`, `Cmp_iId_Empleado`,
                 `Cmp_dePercepciones_DetalleNomina`, `Cmp_deDeducciones_DetalleNomina`)
            VALUES (?, ?, NULL, NULL);", con))
            {
                cmd.Parameters.Add("p1", OdbcType.Int).Value = idNomina;
                cmd.Parameters.Add("p2", OdbcType.Int).Value = idEmpleado;
                cmd.ExecuteNonQuery();
            }
            cn.cerrarConexion();
        }

        // Capa_Modelo_Percepciones_Nomina / MovimientosModelo.cs
        public DataTable ObtenerDetalleNomina_Todo(int idNomina, bool asc = true)
        {
            DataTable dt = new DataTable();
            Conexion cn = new Conexion();

            string orden = asc ? "ASC" : "DESC";
            using (OdbcConnection con = cn.conexionDB())
            using (OdbcCommand cmd = new OdbcCommand($@"
        SELECT d.*                       -- TODAS las columnas de Tbl_DetallesNomina
        FROM `Tbl_DetallesNomina` d
        WHERE d.`Cmp_iId_Nomina` = ?
        ORDER BY d.`Cmp_iId_DetalleNomina` {orden};", con))
            {
                cmd.Parameters.Add("p1", OdbcType.Int).Value = idNomina;
                using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            cn.cerrarConexion();
            return dt;
        }

    }

}

