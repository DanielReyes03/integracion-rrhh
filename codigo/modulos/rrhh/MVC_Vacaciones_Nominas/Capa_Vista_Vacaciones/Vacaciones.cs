using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Vista_Vacaciones
{
    public partial class Vacaciones : Form
    {
        public Vacaciones()
        {
            InitializeComponent();
        }

        private void Gpb_Vacaciones_Enter(object sender, EventArgs e)
        {

        }

        private void Lbl_NomE_Click(object sender, EventArgs e)
        {

        }

        private void Btn_buscar_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Soli_Click(object sender, EventArgs e)
        {
            SolicitarVacaciones frmSoli = new SolicitarVacaciones();
            frmSoli.Show();
            this.Hide(); 
        }

        private void Btn_Modificar_Click(object sender, EventArgs e)
        {
            EditarVacaciones frmSoli = new EditarVacaciones();
            frmSoli.Show();
            this.Hide();
        }

        private void Lbl_Nomina_Click(object sender, EventArgs e)
        {

        }
    }
}
