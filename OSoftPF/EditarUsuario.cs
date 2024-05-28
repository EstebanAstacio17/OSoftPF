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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.IO;

namespace OSoftPF
{
    public partial class EditarUsuario : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        private string idUsuarioEditar;
        public EditarUsuario(string idUsuarioEditar)
        {
            InitializeComponent();

            this.idUsuarioEditar = idUsuarioEditar;
        }

        private void EditarUsuario_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarCboPErmiso();

            LlenarComboBoxDeHerramientas();

            CargarDatosUsuario();

            txtUsuario.ReadOnly = true;

        }

        private void EditarUsuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Limpiar la variable idUsuario cuando el formulario se cierre
            idUsuarioEditar = null;
        }

        private void CargarDatosUsuario()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuarioEditar);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Llenar los TextBox
                            txtNombreCompleto.Text = reader["NombreCompleto"].ToString();
                            txtApellidoCompleto.Text = reader["ApellidoCompleto"].ToString();
                            txtCorreo.Text = reader["Correo"].ToString();
                            txtCedula.Text = reader["Cedula"].ToString();
                            txtPasaporte.Text = reader["Pasaporte"].ToString();
                            txtCelular.Text = reader["Celular"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            txtUsuario.Text = reader["Usuario"].ToString();
                            // Otros TextBox según corresponda

                            // Llenar los ComboBox
                            cboZona.SelectedItem = reader["Zona"].ToString();
                            cboEquipo.SelectedItem = reader["Equipo"].ToString();
                            cboRol.SelectedItem = reader["Rol"].ToString();
                            cboPais.SelectedItem = reader["Pais"].ToString() ;
                            cboEstado.SelectedItem = reader["Estado"].ToString();
                            cboPermiso.SelectedItem = reader["Permiso"].ToString();
                            // Otros ComboBox según corresponda

                            // Llenar el PictureBox (asumiendo que almacenas la imagen como binario en la base de datos)
                            if (reader["Foto"] != DBNull.Value)
                            {
                                byte[] foto = (byte[])reader["Foto"];
                                using (MemoryStream ms = new MemoryStream(foto))
                                {
                                    pbFoto.Image = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NoEditarComboBoxes()
        {
            cboZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPais.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPermiso.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LlenarCboPErmiso()
        {
            // Llenar cboHerramienta con opciones válidas
            cboPermiso.Items.Add("Administrador");
            cboPermiso.Items.Add("Coordinador");
            cboPermiso.Items.Add("Editor");
            cboPermiso.Items.Add("Almacen");
        }

        private void LlenarComboBoxDeHerramientas()
        {
            OpcionesDeZonas();
            OpcionesDeEquipos();
            OpcionesDeRoles();
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

        private void OpcionesDeRoles()
        {
            // Clear any existing items in the ComboBox
            cboRol.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database
                    connection.Open();

                    // Define the SQL query to retrieve the desired column from your table
                    string query = "SELECT CodigoRol FROM Roles WHERE EstadoRol = 'Activo'";

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
                                cboRol.Items.Add(reader["CodigoRol"].ToString());
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

        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Crear y configurar el OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            openFileDialog.Title = "Select an Image";

            // Mostrar el OpenFileDialog y verificar si se seleccionó una imagen
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta de la imagen seleccionada
                string imagePath = openFileDialog.FileName;

                // Cargar la imagen en el PictureBox
                pbFoto.Image = Image.FromFile(imagePath);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            //pbFoto.Image = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE Usuarios SET 
                             NombreCompleto = @NombreCompleto,
                             ApellidoCompleto = @ApellidoCompleto,
                             Correo = @Correo,
                             Cedula = @Cedula,
                             Pasaporte = @Pasaporte,
                             Celular = @Celular,
                             Password = @Password,
                             Usuario = @Usuario,
                             Zona = @Zona,
                             Equipo = @Equipo,
                             Rol = @Rol,
                             Pais = @Pais,
                             Estado = @Estado,
                             Permiso = @Permiso
                             WHERE IdUsuario = @IdUsuario";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuarioEditar);
                    command.Parameters.AddWithValue("@NombreCompleto", txtNombreCompleto.Text);
                    command.Parameters.AddWithValue("@ApellidoCompleto", txtApellidoCompleto.Text);
                    command.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    command.Parameters.AddWithValue("@Cedula", txtCedula.Text);
                    command.Parameters.AddWithValue("@Pasaporte", txtPasaporte.Text);
                    command.Parameters.AddWithValue("@Celular", txtCelular.Text);
                    command.Parameters.AddWithValue("@Password", txtPassword.Text);
                    command.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                    command.Parameters.AddWithValue("@Zona", cboZona.SelectedItem?.ToString());
                    command.Parameters.AddWithValue("@Equipo", cboEquipo.SelectedItem?.ToString());
                    command.Parameters.AddWithValue("@Rol", cboRol.SelectedItem?.ToString());
                    command.Parameters.AddWithValue("@Pais", cboPais.SelectedItem?.ToString());
                    command.Parameters.AddWithValue("@Estado", cboEstado.SelectedItem?.ToString());
                    command.Parameters.AddWithValue("@Permiso", cboPermiso.SelectedItem?.ToString());

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Cerrar el formulario después de la actualización
                                      // Ejecutar el método MostrarUsuarios en el formulario Usuarios
                        ControlUsuarios formUsuarios = Application.OpenForms["ControlUsuarios"] as ControlUsuarios;
                        if (formUsuarios != null)
                        {
                            formUsuarios.MostrarUsuarios();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario a actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar los datos del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}