using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSoftPF
{
    public partial class Configuracion : Form
    {
        public Configuracion()
        {
            InitializeComponent();
        }

        private void btnHerramientas_Click(object sender, EventArgs e)
        {
            Herramientas herramientas = new Herramientas();
            herramientas.ShowDialog();
        }
    }
}
