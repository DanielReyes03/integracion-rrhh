using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Vista_HorasExtra
{
    public partial class EditarHorasExtra : Form
    {
        public EditarHorasExtra()
        {
            InitializeComponent();
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Regresar_Click(object sender, EventArgs e)
        {
            HorasExtra frmregresar = new HorasExtra();
            frmregresar.Show();
            this.Hide();
        }
    }
}
