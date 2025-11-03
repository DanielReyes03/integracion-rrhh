using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using Capa_Controlador_Percepciones_Nomina;
using Capa_Modelo_Percepciones_Nomina;

namespace Capa_Vista_Percepciones_Nomina
{
    public partial class Form_Percep : UserControl
    {
        private readonly CatalogosControlador ctrl = new CatalogosControlador();
        private readonly MovimientosControlador _controlador = new MovimientosControlador();

        public Form_Percep()
        {
            InitializeComponent();
            // No dependas del diseñador para enlazar el Load
            // this.Load += Form_Percep_Load;  // <- ya no
        }

        // OnLoad es más confiable en UserControl
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                try
                {
                    // ✅ NUEVO: suscribir eventos que dependen de controles ya creados
                    Cbo_NoNomina.SelectedIndexChanged += Cbo_NoNomina_SelectedIndexChanged; // ✅ NUEVO

                    CargarCombos(); // llena combos y (si hay nómina) carga el grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en OnLoad: " + ex.Message,
                                    "Percepciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Permite recargar desde el formulario padre si quieres
        public void RefreshData()
        {
            CargarCombos();
        }

        private void CargarCombos()
        {
            // --- Concepto de Nómina ---
            var dtConceptos = ctrl.ListarConceptosNomina();
            Cbo_ConceptoNomina.DisplayMember = "nombre_concepto_nomina";
            Cbo_ConceptoNomina.ValueMember = "id_concepto_nomina";
            Cbo_ConceptoNomina.DataSource = dtConceptos;

            // --- Empleados ---
            var dtEmpleados = ctrl.ListarEmpleados();
            Cbo_Empleado.DisplayMember = "nombre_empleado";
            Cbo_Empleado.ValueMember = "id_empleado";
            Cbo_Empleado.DataSource = dtEmpleados;

            // --- No. Nómina ---
            var dtNominas = ctrl.ListarNumerosNomina();
            Cbo_NoNomina.DisplayMember = "numero_nomina";
            Cbo_NoNomina.ValueMember = "id_nomina";
            Cbo_NoNomina.DataSource = dtNominas;

            Cbo_ConceptoNomina.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbo_Empleado.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbo_NoNomina.DropDownStyle = ComboBoxStyle.DropDownList;

            // ✅ NUEVO: selección inicial y carga del grid
            // Si quieres que NO haya selección en concepto/empleado, déjalos en -1.
            Cbo_ConceptoNomina.SelectedIndex = -1;
            Cbo_Empleado.SelectedIndex = -1;

            if (dtNominas != null && dtNominas.Rows.Count > 0)
            {
                // Selecciona la primera nómina disponible y carga el grid
                Cbo_NoNomina.SelectedIndex = -1; // esto dispara SelectedIndexChanged (y carga el grid)
            }
            else
            {
                // Si no hay nóminas, limpia el grid
                Cbo_NoNomina.SelectedIndex = -1;
                Dvg_Detalle.DataSource = null; // ✅ NUEVO
            }
        }

        // ✅ NUEVO: recargar grid al cambiar la nómina
        private void Cbo_NoNomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbo_NoNomina.SelectedValue == null) return;
            if (Cbo_NoNomina.SelectedValue.ToString() == "System.Data.DataRowView") return;

            int idNomina;
            if (int.TryParse(Cbo_NoNomina.SelectedValue.ToString(), out idNomina))
            {
                CargarDgvDetalle(idNomina);
            }
        }

        // En tu UserControl Form_Percep
        private void CargarDgvDetalle(int idNomina)
        {
            // Trae todos los campos de Tbl_DetallesNomina
            var dt = _controlador.MostrarDetalleNomina_Todo(idNomina, asc: true);

            // Auto generar todas las columnas del DataTable
            Dvg_Detalle.AutoGenerateColumns = true;
            Dvg_Detalle.DataSource = null;
            Dvg_Detalle.Columns.Clear();
            Dvg_Detalle.DataSource = dt;

            // Asigna nombres legibles a las columnas principales
            if (Dvg_Detalle.Columns.Contains("Cmp_iId_DetalleNomina"))
                Dvg_Detalle.Columns["Cmp_iId_DetalleNomina"].HeaderText = "ID Detalle";

            if (Dvg_Detalle.Columns.Contains("Cmp_iId_Nomina"))
                Dvg_Detalle.Columns["Cmp_iId_Nomina"].HeaderText = "ID Nómina";

            if (Dvg_Detalle.Columns.Contains("Cmp_iId_Empleado"))
                Dvg_Detalle.Columns["Cmp_iId_Empleado"].HeaderText = "ID Empleado";

            if (Dvg_Detalle.Columns.Contains("Cmp_iAusencias_DetalleNomina"))
                Dvg_Detalle.Columns["Cmp_iAusencias_DetalleNomina"].HeaderText = "Ausencias";

            if (Dvg_Detalle.Columns.Contains("Cmp_iDiasLaborados_DetalleNomina"))
                Dvg_Detalle.Columns["Cmp_iDiasLaborados_DetalleNomina"].HeaderText = "Días Laborados";

            if (Dvg_Detalle.Columns.Contains("Cmp_dePercepciones_DetalleNomina"))
                Dvg_Detalle.Columns["Cmp_dePercepciones_DetalleNomina"].HeaderText = "Percepciones";

            if (Dvg_Detalle.Columns.Contains("Cmp_deDeducciones_DetalleNomina"))
                Dvg_Detalle.Columns["Cmp_deDeducciones_DetalleNomina"].HeaderText = "Deducciones";

            if (Dvg_Detalle.Columns.Contains("Cmp_deSueldoLiquido_DetalleNomina"))
                Dvg_Detalle.Columns["Cmp_deSueldoLiquido_DetalleNomina"].HeaderText = "Sueldo Líquido";

            // (Opcional) Ajusta ancho de columnas automáticamente
            Dvg_Detalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // (Opcional) Ordenar ascendentemente por ID Detalle
            if (Dvg_Detalle.Columns.Contains("Cmp_iId_DetalleNomina"))
                Dvg_Detalle.Sort(Dvg_Detalle.Columns["Cmp_iId_DetalleNomina"], ListSortDirection.Ascending);
        }


        private void SeleccionarFilaPorEmpleado(int idEmpleado)
        {
            if (Dvg_Detalle.DataSource is DataTable dt)
            {
                string colEmpleado =
                    dt.Columns.Contains("id_empleado") ? "id_empleado" :
                    dt.Columns.Contains("Cmp_iId_Empleado") ? "Cmp_iId_Empleado" : null;

                if (colEmpleado == null) return;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i][colEmpleado]) == idEmpleado)
                    {
                        Dvg_Detalle.ClearSelection();
                        Dvg_Detalle.Rows[i].Selected = true;
                        Dvg_Detalle.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
        }


        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            if (Cbo_NoNomina.SelectedValue == null || Cbo_ConceptoNomina.SelectedValue == null || Cbo_Empleado.SelectedValue == null)
            {
                MessageBox.Show("Selecciona Nómina, Concepto y Empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(Txt_Valor.Text, out var monto) || monto <= 0)
            {
                MessageBox.Show("Ingresa un monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idNomina = Convert.ToInt32(Cbo_NoNomina.SelectedValue);
            int idConcepto = Convert.ToInt32(Cbo_ConceptoNomina.SelectedValue);
            int idEmpleado = Convert.ToInt32(Cbo_Empleado.SelectedValue);

            Conexion cn = new Conexion();
            using (OdbcConnection con = cn.conexionDB())
            using (OdbcTransaction tx = con.BeginTransaction())
            {
                try
                {
                    // 1) Insertar movimiento
                    string sqlMov = @"
                        INSERT INTO `Tbl_MovimientosNomina`
                            (`Cmp_iId_Nomina`, `Cmp_iId_ConceptoNomina`, `Cmp_deMonto_MovimientoNomina`)
                        VALUES (?, ?, ?);";
                    using (OdbcCommand cmd = new OdbcCommand(sqlMov, con, tx))
                    {
                        cmd.Parameters.Add("p1", OdbcType.Int).Value = idNomina;
                        cmd.Parameters.Add("p2", OdbcType.Int).Value = idConcepto;
                        cmd.Parameters.Add("p3", OdbcType.Decimal).Value = monto;
                        cmd.ExecuteNonQuery();
                    }

                    // 2) Insertar detalle (sin cálculos; percepciones/deducciones NULL)
                    string sqlDet = @"
                        INSERT INTO `Tbl_DetallesNomina`
                            (`Cmp_iId_Nomina`, `Cmp_iId_Empleado`,
                             `Cmp_dePercepciones_DetalleNomina`, `Cmp_deDeducciones_DetalleNomina`)
                        VALUES (?, ?, NULL, NULL);";
                    using (OdbcCommand cmd = new OdbcCommand(sqlDet, con, tx))
                    {
                        cmd.Parameters.Add("p1", OdbcType.Int).Value = idNomina;
                        cmd.Parameters.Add("p2", OdbcType.Int).Value = idEmpleado;
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();

                    // 🔄 Recargar grid y resaltar al empleado
                    CargarDgvDetalle(idNomina);
                    SeleccionarFilaPorEmpleado(idEmpleado);

                    MessageBox.Show("Registro insertado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { /* ignore */ }
                    MessageBox.Show("Error al insertar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.cerrarConexion();
                }
            }
        }
        private readonly UtilControlador _util = new UtilControlador();
        private void Btn_Eliminar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Seguro que quieres eliminar todos los registros?\nEsto reiniciará el contador de ID.",
                "Confirmar limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Limpia las tablas
                    _util.TruncarTabla("Tbl_MovimientosNomina");
                    _util.TruncarTabla("Tbl_DetallesNomina");

                    // (opcional, si usas DELETE en lugar de TRUNCATE)
                    _util.ReiniciarSiVacia("Tbl_DetallesNomina", "Cmp_iId_DetalleNomina");

                    MessageBox.Show("Registros eliminados y contador reiniciado.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar grid vacío
                    Dvg_Detalle.DataSource = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al limpiar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
