using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace OSoftPF
{
    public partial class AgregarIglesia : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        public AgregarIglesia()
        {
            InitializeComponent();

            // Inicialmente deshabilitar el botón de grabar
            btnGuardar.Enabled = false;
        }

        private void AgregarIglesia_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarComboBoxDeHerramientas();

            LimiteDeTextBoxes();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                MessageBox.Show("Los campos han sido limpiados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar los campos: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtNombreOrganizacion.Clear();
            txtDocumentoOrganizacion.Clear();
            txtTelefonoOficina.Clear();
            txtDireccionOrganizacion.Clear();
            txtCorreoOrganizacion.Clear();
            cboTipoOraganizacion.SelectedIndex = -1;
            cboDenominacion.SelectedIndex = -1;

            txtNombrePastor.Clear();
            txtDocumentoPastor.Clear();
            txtDireccionPastor.Clear();
            txtTelefonoPastor.Clear();
            txtCelularPastor.Clear();
            txtCorreoPastor.Clear();

            txtBoy.Clear();
            txtGirl.Clear();

            txtNombreLider.Clear();
            txtDocumentoLider.Clear();
            txtDireccionLider.Clear();
            txtTelefonoLider.Clear();
            txtCelularLider.Clear();
            txtCorreoLider.Clear();

            cbotemporada.SelectedIndex = -1;
            cboEquipo.SelectedIndex = -1;

            rtbComentarios.Clear();
        }

        private void LlenarComboBoxDeHerramientas()
        {
            OpcionesDeZonas();
            OpcionesDeEquipos();
            OpcionesDePais();
        }

        private void OpcionesDeZonas()
        {
            // Clear any existing items in the ComboBox
            cboZona.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoZona FROM Zonas WHERE EstadoZona = 'Activo'";

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and obtain a SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the rows in the SqlDataReader
                            while (reader.Read())
                            {
                                // Add each value to the ComboBox
                                cboZona.Items.Add(reader["CodigoZona"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may have occurred
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpcionesDeEquipos()
        {
            // Clear any existing items in the ComboBox
            cboEquipo.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoEquipo FROM Equipos WHERE EstadoEquipo = 'Activo'";

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and obtain a SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the rows in the SqlDataReader
                            while (reader.Read())
                            {
                                // Add each value to the ComboBox
                                cboEquipo.Items.Add(reader["CodigoEquipo"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may have occurred
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpcionesDePais()
        {
            // Clear any existing items in the ComboBox
            cboPais.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoPais FROM Paises WHERE EstadoPais = 'Activo'";

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and obtain a SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the rows in the SqlDataReader
                            while (reader.Read())
                            {
                                // Add each value to the ComboBox
                                cboPais.Items.Add(reader["CodigoPais"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may have occurred
                    MessageBox.Show("Error al llenar el ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void NoEditarComboBoxes()
        {
            cboZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPais.DropDownStyle = ComboBoxStyle.DropDownList;
            cbotemporada.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoOraganizacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDenominacion.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LimiteDeTextBoxes()
        {
            txtNombreOrganizacion.MaxLength = 20;
            txtDocumentoOrganizacion.MaxLength = 11;
            txtTelefonoOficina.MaxLength = 10;
            txtDireccionOrganizacion.MaxLength = 50;
            txtCorreoOrganizacion.MaxLength = 30;

            txtNombrePastor.MaxLength = 20;
            txtDocumentoPastor.MaxLength = 11;
            txtDireccionPastor.MaxLength = 50;
            txtTelefonoPastor.MaxLength = 10;
            txtCelularPastor.MaxLength = 10;
            txtCorreoPastor.MaxLength = 30;

            txtBoy.MaxLength = 3;
            txtGirl.MaxLength = 3;

            txtNombreLider.MaxLength = 20;
            txtDocumentoLider.MaxLength = 11;
            txtDireccionLider.MaxLength = 50;
            txtTelefonoLider.MaxLength = 10;
            txtCelularLider.MaxLength = 10;
            txtCorreoLider.MaxLength = 30;

            rtbComentarios.MaxLength = 250;

        }
    }
}
