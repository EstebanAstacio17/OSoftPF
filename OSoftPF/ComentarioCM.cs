using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace OSoftPF
{
    public partial class ComentarioCM : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        // Bandera para controlar el estado de edición
        private bool editMode = false;
        public ComentarioCM()
        {
            InitializeComponent();

            btnAplicar.Visible = false;
        }

        private void ComentarioCM_Load(object sender, EventArgs e)
        {
            LlenarComboBoxDeHerramientas();

            NoEditarComboBoxes();

            // Al cargar el formulario, deshabilitamos los controles que queremos proteger
            DisableControls();

            // Obtener el IdOrganizacion desde lblIds
            if (int.TryParse(lblIds.Text, out int idOrganizacion))
            {
                CargarDatos(idOrganizacion);
            }

            BuscarYMostrarDatos();
        }

        private void LlenarComboBoxDeHerramientas()
        {
            OpcionesDeDenominacion();
            OpcionesDeTipoOrganizacion();
        }

        public void SetIdOrganizacion(int idOrganizacion)
        {
            lblIds.Text = idOrganizacion.ToString();
        }
        private void btnAgregarObservacion_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lblIds.Text, out int idOrganizacion))
            {
                AddObservacion agregarObservacion = new AddObservacion(idOrganizacion);
                agregarObservacion.FormClosed += (s, args) => CargarComentarios(idOrganizacion); // Actualizar DataGridView al cerrar el formulario
                agregarObservacion.ShowDialog();
            }
            else
            {
                MessageBox.Show("ID de organización no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Cambiar el estado de edición
            editMode = !editMode;

            if (editMode)
            {
                // Habilitar controles para edición
                EnableControls();

                // Ocultar el botón "Editar" y mostrar el botón "Aplicar"
                btnEditar.Visible = false;
                btnAplicar.Visible = true;
            }
            else
            {
                // Deshabilitar controles después de guardar
                DisableControls();

                // Mostrar el botón "Editar" y ocultar el botón "Aplicar"
                btnEditar.Visible = true;
                btnAplicar.Visible = false;
            }



        }

        // Método para cargar y actualizar el DataGridView con los comentarios
        private void CargarComentarios(int idOrganizacion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Observaciones WHERE IdOrganizacion = @IdOrganizacion";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los comentarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para deshabilitar los controles que no deben ser editables
        private void DisableControls()
        {
            txtNombreOrganizacion.Enabled = false;
            txtNombrePastor.Enabled = false;
            txtNombreLider.Enabled = false;
            txtDocumentoOrganizacion.Enabled = false;
            txtDocumentoPastor.Enabled = false;
            txtDocumentoLider.Enabled = false;
            txtTelefonoOficina.Enabled = false;
            txtDireccionPastor.Enabled = false;
            txtDireccionLider.Enabled = false;
            txtDireccionOrganizacion.Enabled = false;
            txtTelefonoPastor.Enabled = false;
            txtTelefonoLider.Enabled = false;
            txtCorreoOrganizacion.Enabled = false;
            txtCelularPastor.Enabled = false;
            txtCelularLider.Enabled = false;
            txtCorreoPastor.Enabled = false;
            txtCorreoLider.Enabled = false;
            txtBoy.Enabled = false;
            txtGirl.Enabled = false;

            cboDenominacion.Enabled = false;
            cboTipoOraganizacion.Enabled = false;
        }

        // Método para habilitar los controles cuando se activa el modo de edición
        private void EnableControls()
        {
            txtNombreOrganizacion.Enabled = true;
            txtNombrePastor.Enabled = true;
            txtNombreLider.Enabled = true;
            txtDocumentoOrganizacion.Enabled = true;
            txtDocumentoPastor.Enabled = true;
            txtDocumentoLider.Enabled = true;
            txtTelefonoOficina.Enabled = true;
            txtDireccionPastor.Enabled = true;
            txtDireccionLider.Enabled = true;
            txtDireccionOrganizacion.Enabled = true;
            txtTelefonoPastor.Enabled = true;
            txtTelefonoLider.Enabled = true;
            txtCorreoOrganizacion.Enabled = true;
            txtCelularPastor.Enabled = true;
            txtCelularLider.Enabled = true;
            txtCorreoPastor.Enabled = true;
            txtCorreoLider.Enabled = true;
            txtBoy.Enabled = true;
            txtGirl.Enabled = true;

            cboDenominacion.Enabled = true;
            cboTipoOraganizacion.Enabled = true;
        }

        private void CargarDatos(int idOrganizacion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta para obtener datos de la tabla Organizacion
                    string queryOrganizacion = @"
                SELECT 
                    NombreOrganizacion, 
                    DocumentoOrganizacion, 
                    TelefonoOficina, 
                    DireccionFisica, 
                    CorreoElectronico, 
                    TipoOrganizacion, 
                    Denominacion 
                FROM Organizacion 
                WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand commandOrganizacion = new SqlCommand(queryOrganizacion, connection))
                    {
                        commandOrganizacion.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        using (SqlDataReader readerOrganizacion = commandOrganizacion.ExecuteReader())
                        {
                            if (readerOrganizacion.Read())
                            {
                                txtNombreOrganizacion.Text = readerOrganizacion["NombreOrganizacion"].ToString();
                                txtDocumentoOrganizacion.Text = readerOrganizacion["DocumentoOrganizacion"].ToString();
                                txtTelefonoOficina.Text = readerOrganizacion["TelefonoOficina"].ToString();
                                txtDireccionOrganizacion.Text = readerOrganizacion["DireccionFisica"].ToString();
                                txtCorreoOrganizacion.Text = readerOrganizacion["CorreoElectronico"].ToString();
                                cboTipoOraganizacion.Text = readerOrganizacion["TipoOrganizacion"].ToString();
                                cboDenominacion.Text = readerOrganizacion["Denominacion"].ToString();
                            }
                        }
                    }

                    // Consulta para obtener datos de la tabla Pastor
                    string queryPastor = @"
                SELECT 
                    NombrecompletoPastor, 
                    DocumentoPastor, 
                    DireccionPastor, 
                    TelefonoPastor, 
                    CelularPastor, 
                    CorreoPastor 
                FROM Pastor 
                WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand commandPastor = new SqlCommand(queryPastor, connection))
                    {
                        commandPastor.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        using (SqlDataReader readerPastor = commandPastor.ExecuteReader())
                        {
                            if (readerPastor.Read())
                            {
                                txtNombrePastor.Text = readerPastor["NombrecompletoPastor"].ToString();
                                txtDocumentoPastor.Text = readerPastor["DocumentoPastor"].ToString();
                                txtDireccionPastor.Text = readerPastor["DireccionPastor"].ToString();
                                txtTelefonoPastor.Text = readerPastor["TelefonoPastor"].ToString();
                                txtCelularPastor.Text = readerPastor["CelularPastor"].ToString();
                                txtCorreoPastor.Text = readerPastor["CorreoPastor"].ToString();
                            }
                        }
                    }

                    // Consulta para obtener datos de la tabla Lider
                    string queryLider = @"
                SELECT 
                    NombrecompletoLider, 
                    DocumentoLider, 
                    DireccionLider, 
                    TelefonoLider, 
                    CelularLider, 
                    CorreoLider 
                FROM Lider 
                WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand commandLider = new SqlCommand(queryLider, connection))
                    {
                        commandLider.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        using (SqlDataReader readerLider = commandLider.ExecuteReader())
                        {
                            if (readerLider.Read())
                            {
                                txtNombreLider.Text = readerLider["NombrecompletoLider"].ToString();
                                txtDocumentoLider.Text = readerLider["DocumentoLider"].ToString();
                                txtDireccionLider.Text = readerLider["DireccionLider"].ToString();
                                txtTelefonoLider.Text = readerLider["TelefonoLider"].ToString();
                                txtCelularLider.Text = readerLider["CelularLider"].ToString();
                                txtCorreoLider.Text = readerLider["CorreoLider"].ToString();
                            }
                        }
                    }

                    // Consulta para obtener datos de la tabla Oportunidades
                    string queryOportunidades = @"
                SELECT Boy, Girls 
                FROM Oportunidades 
                WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand commandOportunidades = new SqlCommand(queryOportunidades, connection))
                    {
                        commandOportunidades.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        using (SqlDataReader readerOportunidades = commandOportunidades.ExecuteReader())
                        {
                            if (readerOportunidades.Read())
                            {
                                txtBoy.Text = readerOportunidades["Boy"].ToString();
                                txtGirl.Text = readerOportunidades["Girls"].ToString();
                            }
                        }
                    }

                    // Consulta para obtener observaciones y cargar el DataGridView
                    string queryObservaciones = @"
                SELECT IdObservaciones, Observacion, FechaObservacion 
                FROM Observaciones 
                WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand commandObservaciones = new SqlCommand(queryObservaciones, connection))
                    {
                        commandObservaciones.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        SqlDataAdapter adapter = new SqlDataAdapter(commandObservaciones);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvComentarios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpcionesDeDenominacion()
        {
            // Clear any existing items in the ComboBox
            cboDenominacion.Items.Clear();

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
                                // Add each value to the ComboBox
                                cboDenominacion.Items.Add(reader["CodigoDenominacion"].ToString());
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
            // Clear any existing items in the ComboBox
            cboTipoOraganizacion.Items.Clear();

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
                                // Add each value to the ComboBox
                                cboTipoOraganizacion.Items.Add(reader["CodigoTipoOrganizacion"].ToString());
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
                            op.Total,
                            dt.TemporadaCM, dt.ZonaCM, dt.EquipoCM, dt.PaisCM
                        FROM Organizacion o
                        LEFT JOIN Pastor p ON o.IdOrganizacion = p.IdOrganizacion
                        LEFT JOIN Lider l ON o.IdOrganizacion = l.IdOrganizacion
                        LEFT JOIN Oportunidades op ON o.IdOrganizacion = op.IdOrganizacion
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

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lblIds.Text, out int idOrganizacion))
            {
                ActualizarDatos(idOrganizacion);
                DisableControls();

                // Mostrar el botón "Editar" y ocultar el botón "Aplicar"
                btnEditar.Visible = true;
                btnAplicar.Visible = false;
                editMode = false;
            }
        }

        // Método para actualizar los datos en la base de datos
        private void ActualizarDatos(int idOrganizacion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string queryOrganizacion = @"
                        UPDATE Organizacion
                        SET 
                            NombreOrganizacion = @NombreOrganizacion,
                            DocumentoOrganizacion = @DocumentoOrganizacion,
                            TelefonoOficina = @TelefonoOficina,
                            DireccionFisica = @DireccionFisica,
                            CorreoElectronico = @CorreoElectronico,
                            TipoOrganizacion = @TipoOrganizacion,
                            Denominacion = @Denominacion
                        WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand command = new SqlCommand(queryOrganizacion, connection))
                    {
                        command.Parameters.AddWithValue("@NombreOrganizacion", txtNombreOrganizacion.Text);
                        command.Parameters.AddWithValue("@DocumentoOrganizacion", txtDocumentoOrganizacion.Text);
                        command.Parameters.AddWithValue("@TelefonoOficina", txtTelefonoOficina.Text);
                        command.Parameters.AddWithValue("@DireccionFisica", txtDireccionOrganizacion.Text);
                        command.Parameters.AddWithValue("@CorreoElectronico", txtCorreoOrganizacion.Text);
                        command.Parameters.AddWithValue("@TipoOrganizacion", cboTipoOraganizacion.Text);
                        command.Parameters.AddWithValue("@Denominacion", cboDenominacion.Text);
                        command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        command.ExecuteNonQuery();
                    }

                    string queryPastor = @"
                        UPDATE Pastor
                        SET 
                            NombrecompletoPastor = @NombrecompletoPastor,
                            DocumentoPastor = @DocumentoPastor,
                            DireccionPastor = @DireccionPastor,
                            TelefonoPastor = @TelefonoPastor,
                            CelularPastor = @CelularPastor,
                            CorreoPastor = @CorreoPastor
                        WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand command = new SqlCommand(queryPastor, connection))
                    {
                        command.Parameters.AddWithValue("@NombrecompletoPastor", txtNombrePastor.Text);
                        command.Parameters.AddWithValue("@DocumentoPastor", txtDocumentoPastor.Text);
                        command.Parameters.AddWithValue("@DireccionPastor", txtDireccionPastor.Text);
                        command.Parameters.AddWithValue("@TelefonoPastor", txtTelefonoPastor.Text);
                        command.Parameters.AddWithValue("@CelularPastor", txtCelularPastor.Text);
                        command.Parameters.AddWithValue("@CorreoPastor", txtCorreoPastor.Text);
                        command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        command.ExecuteNonQuery();
                    }

                    string queryLider = @"
                        UPDATE Lider
                        SET 
                            NombrecompletoLider = @NombrecompletoLider,
                            DocumentoLider = @DocumentoLider,
                            DireccionLider = @DireccionLider,
                            TelefonoLider = @TelefonoLider,
                            CelularLider = @CelularLider,
                            CorreoLider = @CorreoLider
                        WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand command = new SqlCommand(queryLider, connection))
                    {
                        command.Parameters.AddWithValue("@NombrecompletoLider", txtNombreLider.Text);
                        command.Parameters.AddWithValue("@DocumentoLider", txtDocumentoLider.Text);
                        command.Parameters.AddWithValue("@DireccionLider", txtDireccionLider.Text);
                        command.Parameters.AddWithValue("@TelefonoLider", txtTelefonoLider.Text);
                        command.Parameters.AddWithValue("@CelularLider", txtCelularLider.Text);
                        command.Parameters.AddWithValue("@CorreoLider", txtCorreoLider.Text);
                        command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        command.ExecuteNonQuery();
                    }

                    string queryOportunidades = @"
                        UPDATE Oportunidades
                        SET 
                            Boy = @Boy,
                            Girls = @Girls
                        WHERE IdOrganizacion = @IdOrganizacion";

                    using (SqlCommand command = new SqlCommand(queryOportunidades, connection))
                    {
                        command.Parameters.AddWithValue("@Boy", txtBoy.Text);
                        command.Parameters.AddWithValue("@Girls", txtGirl.Text);
                        command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Datos actualizados exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
