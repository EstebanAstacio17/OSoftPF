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
            OpcionesDeTemporada();
            OpcionesDeDenominacion();
            OpcionesDeTipoOrganizacion();
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

        private void OpcionesDeTemporada()
        {
            // Clear any existing items in the ComboBox
            cbotemporada.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoTemporada FROM Temporada WHERE EstadoTemporada = 'Activo'";

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
                                cbotemporada.Items.Add(reader["CodigoTemporada"].ToString());
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

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, el carácter de control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            ValidacionCompleta();
        }

        private void ValidacionCompleta()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Validar txtNombreOrganizacion
                if (ExisteEnTabla(connection, "Organizacion", "NombreOrganizacion", txtNombreOrganizacion.Text))
                {
                    txtNombreOrganizacion.BackColor = Color.Red;
                }
                else
                {
                    txtNombreOrganizacion.BackColor = Color.White;
                }

                // Validar txtNombrePastor
                if (ExisteEnTabla(connection, "Pastor", "NombrecompletoPastor", txtNombrePastor.Text))
                {
                    txtNombrePastor.BackColor = Color.Red;
                }
                else
                {
                    txtNombrePastor.BackColor = Color.White;
                }

                // Validar txtDocumentoPastor
                if (ExisteEnTabla(connection, "Pastor", "DocumentoPastor", txtDocumentoPastor.Text))
                {
                    txtDocumentoPastor.BackColor = Color.Red;
                }
                else
                {
                    txtDocumentoPastor.BackColor = Color.White;
                }

                // Validar txtNombreLider
                if (ExisteEnTabla(connection, "Lider", "NombrecompletoLider", txtNombreLider.Text))
                {
                    txtNombreLider.BackColor = Color.Red;
                }
                else
                {
                    txtNombreLider.BackColor = Color.White;
                }

                // Validar txtDocumentoLider
                if (ExisteEnTabla(connection, "Lider", "DocumentoLider", txtDocumentoLider.Text))
                {
                    txtDocumentoLider.BackColor = Color.Red;
                }
                else
                {
                    txtDocumentoLider.BackColor = Color.White;
                }
            }

            // Habilitar el botón btnGuardar si la validación fue exitosa
            btnGuardar.Enabled = true;


        }

        private bool ExisteEnTabla(SqlConnection connection, string tableName, string columnName, string value)
        {
            string query = $"SELECT COUNT(1) FROM {tableName} WHERE {columnName} = @value";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@value", value);
                return (int)command.ExecuteScalar() > 0;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que los TextBox específicos no estén en rojo
                if (txtNombreOrganizacion.BackColor == Color.Red ||
                    txtNombrePastor.BackColor == Color.Red ||
                    txtDocumentoPastor.BackColor == Color.Red ||
                    txtNombreLider.BackColor == Color.Red ||
                    txtDocumentoLider.BackColor == Color.Red)
                {
                    // Mostrar mensaje de error si alguno está en rojo
                    MessageBox.Show("Por favor, corrija los campos en rojo antes de guardar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método sin ejecutar InsertarCM
                }

                // Si todos los TextBox están válidos, ejecutar InsertarCM
                InsertarCM();

                LimiteDeTextBoxes();

                this.Close();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones general
                MessageBox.Show("Ocurrió un error al intentar guardar la información: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InsertarCM()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insertar en la tabla Organizacion y obtener el IdOrganizacion generado
                    int idOrganizacion = InsertarOrganizacion(connection, transaction);

                    // Insertar en la tabla Pastor
                    InsertarPastor(connection, transaction, idOrganizacion);

                    // Insertar en la tabla Lider
                    InsertarLider(connection, transaction, idOrganizacion);

                    // Insertar en la tabla Oportunidades
                    InsertarOportunidades(connection, transaction, idOrganizacion);

                    // Insertar en la tabla Observaciones
                    InsertarObservaciones(connection, transaction, idOrganizacion);

                    // Insertar en la tabla DatosTemporadaCM
                    InsertarDatosTemporadaCM(connection, transaction, idOrganizacion);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al guardar la información: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int InsertarOrganizacion(SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
        INSERT INTO Organizacion 
            (NombreOrganizacion, DocumentoOrganizacion, TelefonoOficina, DireccionFisica, CorreoElectronico, TipoOrganizacion, Denominacion)
        VALUES 
            (@NombreOrganizacion, @DocumentoOrganizacion, @TelefonoOficina, @DireccionOrganizacion, @CorreoOrganizacion, @TipoOrganizacion, @Denominacion );
        SELECT SCOPE_IDENTITY();";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@NombreOrganizacion", txtNombreOrganizacion.Text);
                command.Parameters.AddWithValue("@DocumentoOrganizacion", txtDocumentoOrganizacion.Text);
                command.Parameters.AddWithValue("@TelefonoOficina", txtTelefonoOficina.Text);
                command.Parameters.AddWithValue("@DireccionOrganizacion", txtDireccionOrganizacion.Text);
                command.Parameters.AddWithValue("@CorreoOrganizacion", txtCorreoOrganizacion.Text);
                command.Parameters.AddWithValue("@TipoOrganizacion", cboTipoOraganizacion.Text);
                command.Parameters.AddWithValue("@Denominacion", cboDenominacion.Text);

                // Ejecutar el comando y obtener el IdOrganizacion generado
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void InsertarPastor(SqlConnection connection, SqlTransaction transaction, int idOrganizacion)
        {
            string query = @"
        INSERT INTO Pastor 
            (IdOrganizacion, NombrecompletoPastor, DocumentoPastor, DireccionPastor, TelefonoPastor, CelularPastor, CorreoPastor)
        VALUES 
            (@IdOrganizacion, @NombrePastor, @DocumentoPastor, @DireccionPastor, @TelefonoPastor, @CelularPastor, @CorreoPastor);";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                command.Parameters.AddWithValue("@NombrePastor", txtNombrePastor.Text);
                command.Parameters.AddWithValue("@DocumentoPastor", txtDocumentoPastor.Text);
                command.Parameters.AddWithValue("@DireccionPastor", txtDireccionPastor.Text);
                command.Parameters.AddWithValue("@TelefonoPastor", txtTelefonoPastor.Text);
                command.Parameters.AddWithValue("@CelularPastor", txtCelularPastor.Text);
                command.Parameters.AddWithValue("@CorreoPastor", txtCorreoPastor.Text);

                command.ExecuteNonQuery();
            }
        }

        private void InsertarLider(SqlConnection connection, SqlTransaction transaction, int idOrganizacion)
        {
            string query = @"
        INSERT INTO Lider 
            (IdOrganizacion, NombrecompletoLider, DocumentoLider, DireccionLider, TelefonoLider, CelularLider, CorreoLider)
        VALUES 
            (@IdOrganizacion, @NombreLider, @DocumentoLider, @DireccionLider, @TelefonoLider, @CelularLider, @CorreoLider);";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                command.Parameters.AddWithValue("@NombreLider", txtNombreLider.Text);
                command.Parameters.AddWithValue("@DocumentoLider", txtDocumentoLider.Text);
                command.Parameters.AddWithValue("@DireccionLider", txtDireccionLider.Text);
                command.Parameters.AddWithValue("@TelefonoLider", txtTelefonoLider.Text);
                command.Parameters.AddWithValue("@CelularLider", txtCelularLider.Text);
                command.Parameters.AddWithValue("@CorreoLider", txtCorreoLider.Text);

                command.ExecuteNonQuery();
            }
        }

        private void InsertarOportunidades(SqlConnection connection, SqlTransaction transaction, int idOrganizacion)
        {
            string query = @"
        INSERT INTO Oportunidades 
            (IdOrganizacion, Boy, Girls)
        VALUES 
            (@IdOrganizacion, @Boy, @Girls);";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                command.Parameters.AddWithValue("@Boy", txtBoy.Text);
                command.Parameters.AddWithValue("@Girls", txtGirl.Text);

                command.ExecuteNonQuery();
            }
        }

        private void InsertarObservaciones(SqlConnection connection, SqlTransaction transaction, int idOrganizacion)
        {
            string query = @"
        INSERT INTO Observaciones 
            (IdOrganizacion, Observacion)
        VALUES 
            (@IdOrganizacion, @Observacion);";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                command.Parameters.AddWithValue("@Observacion", rtbComentarios.Text);

                command.ExecuteNonQuery();
            }
        }

        private void InsertarDatosTemporadaCM(SqlConnection connection, SqlTransaction transaction, int idOrganizacion)
        {
            string query = @"
        INSERT INTO DatosTemporadaCM 
            (IdOrganizacion, TemporadaCM, ZonaCM, EquipoCM, PaisCM)
        VALUES 
            (@IdOrganizacion, @TemporadaCM, @ZonaCM, @EquipoCM, @PaisCM);";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                command.Parameters.AddWithValue("@TemporadaCM", cbotemporada.Text);
                command.Parameters.AddWithValue("@ZonaCM", cboZona.Text);
                command.Parameters.AddWithValue("@EquipoCM", cboEquipo.Text);
                command.Parameters.AddWithValue("@PaisCM", cboPais.Text);

                command.ExecuteNonQuery();
            }
        }



    }
}
