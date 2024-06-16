using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;
using System.IO;
using ExcelDataReader;  // Necesitarás instalar ExcelDataReader a través de NuGet

namespace OSoftPF
{
    public partial class GestionCM : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public GestionCM( )
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            ImportarCM irImportarCM = new ImportarCM();
            irImportarCM.ShowDialog();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarIglesia agregarIglesia = new AgregarIglesia();
            agregarIglesia.ShowDialog();
        }
    }
}