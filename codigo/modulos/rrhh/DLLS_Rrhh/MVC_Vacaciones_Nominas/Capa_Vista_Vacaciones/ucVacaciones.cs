using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using Capa_Controlador_Vacaciones;
using Capa_Modelo_Vacaciones;

namespace Capa_Vista_Vacaciones
{
    public partial class ucVacaciones : UserControl
    {
        private Controlador controlador = new Controlador();

        public ucVacaciones()
        {
            InitializeComponent();
            CargarEmpleados();
        }

        public void CargarEmpleados()
        {
            try
            {
                var con = new Conexion().ConexionDB();
                if (con != null)
                {
                    string query = "SELECT pk_idEmpleado, nombre FROM tbl_empleado";
                    OdbcCommand cmd = new OdbcCommand(query, con);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    Cbo_NombreE.DataSource = dt;
                    Cbo_NombreE.DisplayMember = "nombre";
                    Cbo_NombreE.ValueMember = "pk_idEmpleado";
                    con.Close();
                }
            }
            catch { }
        }

        private void Btn_buscar_Click(object sender, EventArgs e)
        {
            if (Cbo_NombreE.SelectedValue != null && int.TryParse(Cbo_NombreE.SelectedValue.ToString(), out int id))
            {
                Dvg_HoraE.DataSource = controlador.BuscarVacaciones(id);
            }
            else
            {
                MessageBox.Show("Seleccione un empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_Soli_Click(object sender, EventArgs e)
        {
            SolicitarVacaciones formSolicitar = new SolicitarVacaciones();
            formSolicitar.FormClosed += (s, args) =>
            {
                if (Cbo_NombreE.SelectedValue != null)
                {
                    int id = Convert.ToInt32(Cbo_NombreE.SelectedValue);
                    Dvg_HoraE.DataSource = controlador.BuscarVacaciones(id);
                }
            };
            formSolicitar.Show();
        }

        private void Btn_Modificar_Click(object sender, EventArgs e)
        {
            if (Dvg_HoraE.SelectedRows.Count > 0)
            {
                int idVacacion = Convert.ToInt32(Dvg_HoraE.SelectedRows[0].Cells["Cmp_iId_Vacacion"].Value);
                EditarVacaciones formEditar = new EditarVacaciones(idVacacion);
                formEditar.FormClosed += (s, args) =>
                {
                    if (Cbo_NombreE.SelectedValue != null)
                    {
                        int id = Convert.ToInt32(Cbo_NombreE.SelectedValue);
                        Dvg_HoraE.DataSource = controlador.BuscarVacaciones(id);
                    }
                };
                formEditar.Show();
            }
            else
            {
                MessageBox.Show("Seleccione una vacación para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (Dvg_HoraE.SelectedRows.Count > 0)
            {
                int idVacacion = Convert.ToInt32(Dvg_HoraE.SelectedRows[0].Cells["Cmp_iId_Vacacion"].Value);
                if (MessageBox.Show("¿Eliminar esta vacación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string resultado = controlador.EliminarVacacion(idVacacion);
                    MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, resultado.Contains("correctamente") ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    if (resultado.Contains("correctamente") && Cbo_NombreE.SelectedValue != null)
                    {
                        int idEmpleado = Convert.ToInt32(Cbo_NombreE.SelectedValue);
                        Dvg_HoraE.DataSource = controlador.BuscarVacaciones(idEmpleado);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una vacación para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ucVacaciones_Load(object sender, EventArgs e)
        {

        }
    }
}