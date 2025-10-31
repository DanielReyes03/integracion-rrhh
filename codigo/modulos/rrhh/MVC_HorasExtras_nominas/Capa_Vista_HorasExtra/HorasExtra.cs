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
    public partial class HorasExtra : Form
    {
        public HorasExtra()
        {
            InitializeComponent();
        }

        private void Btn_Soli_Click(object sender, EventArgs e)
        {
            SolicitarHorasE frmsoli = new SolicitarHorasE();
            frmsoli.Show();
            this.Hide();
        }

        private void Btn_Modificar_Click(object sender, EventArgs e)
        {
            EditarHorasExtra frmeditar = new EditarHorasExtra();
            frmeditar.Show();
            this.Hide();
        }


    }
}
