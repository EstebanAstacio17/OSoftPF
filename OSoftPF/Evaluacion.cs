
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Vml;

namespace OSoftPF
{
    public partial class Evaluacion : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        private List<string> denominaciones = new List<string>();
        private List<string> tiposOrganizacion = new List<string>();

        public Evaluacion()
        {
            InitializeComponent();

            
        }

        private void Evaluacion_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarComboBoxDeHerramientas();

            BuscarYMostrarDatos();
        }

        private void LlenarComboBoxDeHerramientas()
        {
            OpcionesDeDenominacion();

            OpcionesDeTipoOrganizacion();
        }

        private void OpcionesDeDenominacion()
        {
            // Clear any existing items in the ComboBox and list
            cboDenominacion.Items.Clear();
            denominaciones.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoDenominacion FROM Denominacion WHERE EstadoDenominacion = 'Activo'";

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and obtain a SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the rows in the SqlDataReader
                            while (reader.Read())
                            {
                                // Add each value to the ComboBox and the list
                                string codigo = reader["CodigoDenominacion"].ToString();
                                cboDenominacion.Items.Add(codigo);
                                denominaciones.Add(codigo);
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

        private void OpcionesDeTipoOrganizacion()
        {
            // Clear any existing items in the ComboBox and list
            cboTipoOraganizacion.Items.Clear();
            tiposOrganizacion.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoTipoOrganizacion FROM TipoOrganizacion WHERE EstadoTipoOrganizacion = 'Activo'";

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and obtain a SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the rows in the SqlDataReader
                            while (reader.Read())
                            {
                                // Add each value to the ComboBox and the list
                                string codigo = reader["CodigoTipoOrganizacion"].ToString();
                                cboTipoOraganizacion.Items.Add(codigo);
                                tiposOrganizacion.Add(codigo);
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
            cboTipoOraganizacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDenominacion.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void SetDatos(string[] datos)
        {
            if (datos.Length < 21)
            {
                throw new ArgumentException("Se requieren al menos 21 elementos en el array de datos.");
            }

            txtNombreOrganizacion.Text = datos[0];
            txtDocumentoOrganizacion.Text = datos[1];
            txtTelefonoOficina.Text = datos[2];
            txtDireccionOrganizacion.Text = datos[3];
            txtCorreoOrganizacion.Text = datos[4];

            string denominacionSeleccionada = datos[5];
            if (denominaciones.Contains(denominacionSeleccionada))
            {
                cboDenominacion.SelectedItem = denominacionSeleccionada;
            }
            else
            {
                MessageBox.Show($"El valor '{denominacionSeleccionada}' no está disponible en el ComboBox Denominacion.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string tipoOrganizacionSeleccionado = datos[6];
            if (tiposOrganizacion.Contains(tipoOrganizacionSeleccionado))
            {
                cboTipoOraganizacion.SelectedItem = tipoOrganizacionSeleccionado;
            }
            else
            {
                MessageBox.Show($"El valor '{tipoOrganizacionSeleccionado}' no está disponible en el ComboBox TipoOrganizacion.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtNombrePastor.Text = datos[7];
            txtDocumentoPastor.Text = datos[8];
            txtDireccionPastor.Text = datos[9];
            txtTelefonoPastor.Text = datos[10];
            txtCelularPastor.Text = datos[11];
            txtCorreoPastor.Text = datos[12];
            txtNombreLider.Text = datos[13];
            txtDocumentoLider.Text = datos[14];
            txtDireccionLider.Text = datos[15];
            txtTelefonoLider.Text = datos[16];
            txtCelularLider.Text = datos[17];
            txtCorreoLider.Text = datos[18];
            txtBoy.Text = datos[19];
            txtGirl.Text = datos[20];
        }

        private void BuscarYMostrarDatos()
        {
            string nombreOrganizacion = txtNombreOrganizacion.Text;
            string nombrePastor = txtNombrePastor.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            o.IdOrganizacion, o.NombreOrganizacion, o.DocumentoOrganizacion, o.TelefonoOficina, o.DireccionFisica, o.CorreoElectronico, o.TipoOrganizacion, o.Denominacion, 
                            p.NombrecompletoPastor, p.DocumentoPastor, p.DireccionPastor, p.TelefonoPastor, p.CelularPastor, p.CorreoPastor,
                            l.NombrecompletoLider, l.DocumentoLider, l.DireccionLider, l.TelefonoLider, l.CelularLider, l.CorreoLider,
                            op.Boy, op.Girls, op.Total, op.FechaRegistro,
                            ob.Observacion, ob.FechaObservacion,
                            dt.TemporadaCM, dt.ZonaCM, dt.EquipoCM, dt.PaisCM, dt.FechaRegistro
                        FROM Organizacion o
                        LEFT JOIN Pastor p ON o.IdOrganizacion = p.IdOrganizacion
                        LEFT JOIN Lider l ON o.IdOrganizacion = l.IdOrganizacion
                        LEFT JOIN Oportunidades op ON o.IdOrganizacion = op.IdOrganizacion
                        LEFT JOIN Observaciones ob ON o.IdOrganizacion = ob.IdOrganizacion
                        LEFT JOIN DatosTemporadaCM dt ON o.IdOrganizacion = dt.IdOrganizacion
                        WHERE o.NombreOrganizacion LIKE @NombreOrganizacion AND p.NombrecompletoPastor LIKE @NombrePastor";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreOrganizacion", "%" + nombreOrganizacion + "%");
                        command.Parameters.AddWithValue("@NombrePastor", "%" + nombrePastor + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                DataRow row = dataTable.Rows[0];
                                int idOrganizacion = Convert.ToInt32(row["IdOrganizacion"]);
                                MostrarObservaciones(idOrganizacion);
                            }

                            dgvHistorico.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar y mostrar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarObservaciones(int idOrganizacion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            Observacion, 
                            FechaObservacion
                        FROM Observaciones
                        WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgvComentarios.DataSource = dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al mostrar observaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
