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
    public partial class SolicitarVacaciones : Form
    {
        public SolicitarVacaciones()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Btn_Regresar_Click(object sender, EventArgs e)
        {
            Vacaciones frmSoli = new Vacaciones();
            frmSoli.Show();
            this.Hide();
        }
    }
}
