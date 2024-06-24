using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace OSoftPF
{
    public partial class AddObservacion : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        private int idOrganizacion;
        public AddObservacion(int idOrganizacion)
        {
            InitializeComponent();

            this.idOrganizacion = idOrganizacion;
        }

        private void AddObservacion_Load(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string observacion = rtbComentario.Text.Trim();

            if (string.IsNullOrEmpty(observacion))
            {
                MessageBox.Show("Por favor, ingrese una observación.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Observaciones (IdOrganizacion, Observacion) VALUES (@IdOrganizacion, @Observacion)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                        command.Parameters.AddWithValue("@Observacion", observacion);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Observación agregada exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la observación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
