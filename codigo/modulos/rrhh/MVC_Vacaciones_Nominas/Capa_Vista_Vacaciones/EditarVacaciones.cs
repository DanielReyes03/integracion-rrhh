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
    public partial class EditarVacaciones : Form
    {
        public EditarVacaciones()
        {
            InitializeComponent();
        }

        private void Gpb_EditarV_Enter(object sender, EventArgs e)
        {

        }

        private void Btn_Regresar_Click(object sender, EventArgs e)
        {
            Vacaciones frmSoli = new Vacaciones();
            frmSoli.Show();
            this.Hide(); 
        }

        private void Gpb_EditarV_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Lbl_Nomina_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {

        }
    }
}
