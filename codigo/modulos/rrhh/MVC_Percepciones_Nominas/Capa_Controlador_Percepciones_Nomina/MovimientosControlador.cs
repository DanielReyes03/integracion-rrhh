using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_Modelo_Percepciones_Nomina;
namespace Capa_Controlador_Percepciones_Nomina
{
    // 📂 Capa_Controlador_Percepciones_Nomina / MovimientosControlador.cs
    public class MovimientosControlador
    {
        MovimientosModelo modelo = new MovimientosModelo();

        public void GuardarMovimientoYDetalle(int idNomina, int idConcepto, int idEmpleado, decimal monto)
        {
            modelo.InsertarMovimiento(idNomina, idConcepto, monto);
            modelo.InsertarDetalleNomina(idNomina, idEmpleado);
        }

        // Capa_Controlador_Percepciones_Nomina / MovimientosControlador.cs
        public DataTable MostrarDetalleNomina_Todo(int idNomina, bool asc = true)
        {
            return modelo.ObtenerDetalleNomina_Todo(idNomina, asc);
        }

    }


}
