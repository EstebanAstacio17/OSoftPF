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
    public partial class PerfilUsuario : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        // Define el delegado para el evento
        public delegate void FormClosedEventHandler();
        // Define el evento basado en el delegado
        public event FormClosedEventHandler FormClosedEvent;
        public PerfilUsuario()
        {
            InitializeComponent();

            ValidarCamposAutomaticamente();

            // Inicialmente deshabilitar el botón de grabar
            btnGrabar.Enabled = false;
        }

        private void PerfilUsuario_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarCboPErmiso();

            LlenarComboBoxDeHerramientas();

            FormatoDeCampos();
        }

        private void ValidarCamposAutomaticamente()
        {
            // Suscribir los eventos de cambio de texto para la validación automática
            txtNombreCompleto.TextChanged += ValidarCampos_TextChanged;
            txtApellidoCompleto.TextChanged += ValidarCampos_TextChanged;
            txtCorreo.TextChanged += ValidarCampos_TextChanged;
            txtCedula.TextChanged += ValidarCampos_TextChanged;
            txtPasaporte.TextChanged += ValidarCampos_TextChanged;
            txtCelular.TextChanged += ValidarCampos_TextChanged;
            txtPassword.TextChanged += ValidarCampos_TextChanged;
            txtUsuario.TextChanged += ValidarCampos_TextChanged;
            cboZona.SelectedIndexChanged += ValidarCampos_TextChanged;
            cboEquipo.SelectedIndexChanged += ValidarCampos_TextChanged;
            cboRol.SelectedIndexChanged += ValidarCampos_TextChanged;
            cboPais.SelectedIndexChanged += ValidarCampos_TextChanged;
            cboPermiso.SelectedIndexChanged += ValidarCampos_TextChanged;
            cboEstado.SelectedIndexChanged += ValidarCampos_TextChanged;
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números (0-9) y la tecla de retroceso
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar el evento si el carácter ingresado no es un número ni una tecla de control
                e.Handled = true;
            }
        }

        // Sobrescribir el método OnFormClosed para lanzar el evento
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // Lanzar el evento si hay suscriptores
            FormClosedEvent?.Invoke();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            agregarUsuario();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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

        private void LimpiarCampos()
        {
            pbFoto.Image = null;

            txtNombreCompleto.Clear();
            txtApellidoCompleto.Clear();
            txtCorreo.Clear();
            txtCedula.Clear();
            txtPasaporte.Clear();
            txtCelular.Clear();
            txtPassword.Clear();

            cboZona.SelectedIndex = -1;
            cboEquipo.SelectedIndex = -1;
            cboRol.SelectedIndex = -1;
            cboPais.SelectedIndex = -1;
            cboPermiso.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
        }

        private void FormatoDeCampos()
        {
            // Inicialmente establecer el TextBox como solo lectura
            txtUsuario.ReadOnly = true;

            LimiteDeTextBoxes();
        }

        private void LimiteDeTextBoxes()
        {
            // Establecer la propiedad MaxLength de cada TextBox a 20 caracteres
            txtNombreCompleto.MaxLength = 20;
            txtApellidoCompleto.MaxLength = 20;
            txtCorreo.MaxLength = 35;
            txtCedula.MaxLength = 11;
            txtPasaporte.MaxLength = 10;
            txtCelular.MaxLength = 10;
            txtPassword.MaxLength = 20;
        }

        private void TxtPasaporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números (0-9), letras (a-z y A-Z) y la tecla de retroceso
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar el evento
                e.Handled = true;
            }
        }

        private void TxtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números (0-9) y la tecla de retroceso
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancelar el evento
                e.Handled = true;
            }
        }
        
        private void TextBoxLettersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter presionado es una letra, una tecla de control o un espacio
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                // Si no es una letra, una tecla de control ni un espacio, cancelar el evento
                e.Handled = true;
            }
        }

        // Método para actualizar Usuario basado en textBox1 y textBox2
        private void CreateUsuario(object sender, EventArgs e)
        {
            // Obtener los dos primeros caracteres de txtNombreCompleto
            string partNombre = txtNombreCompleto.Text.Length >= 2 ? txtNombreCompleto.Text.Substring(0, 2) : txtNombreCompleto.Text;

            // Obtener la primera palabra de txtApellidoCompleto
            string partApellido = txtApellidoCompleto.Text;
            int spaceIndex = partApellido.IndexOf(' ');
            string firstWord = spaceIndex > 0 ? partApellido.Substring(0, spaceIndex) : partApellido;

            // Combinar los textos, convertir a mayúsculas y asignarlos a txtUsuario
            txtUsuario.Text = (partNombre + firstWord).ToUpper();
        }


        private void btnCargarFoto_Click(object sender, EventArgs e)
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

        private void btnQuitarFoto_Click(object sender, EventArgs e)
        {
            pbFoto.Image = null;
        }

        private void agregarUsuario()
        {
            try
            {
                if (!CamposCompletos())
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convertir la imagen del PictureBox a un arreglo de bytes
                byte[] imagenBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    pbFoto.Image.Save(ms, pbFoto.Image.RawFormat);
                    imagenBytes = ms.ToArray();
                }

                // Insertar el registro en la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Usuarios (NombreCompleto, ApellidoCompleto, Correo, Cedula, Pasaporte, Celular, Password, Usuario, Zona, Equipo, Rol, Pais, Permiso, Estado, Foto) " +
                                   "VALUES (@NombreCompleto, @ApellidoCompleto, @Correo, @Cedula, @Pasaporte, @Celular, @Password, @Usuario, @Zona, @Equipo, @Rol, @Pais, @Permiso, @Estado, @Foto)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Asignar los parámetros
                        command.Parameters.AddWithValue("@NombreCompleto", txtNombreCompleto.Text);
                        command.Parameters.AddWithValue("@ApellidoCompleto", txtApellidoCompleto.Text);
                        command.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                        command.Parameters.AddWithValue("@Cedula", txtCedula.Text);
                        command.Parameters.AddWithValue("@Pasaporte", txtPasaporte.Text);
                        command.Parameters.AddWithValue("@Celular", txtCelular.Text);
                        command.Parameters.AddWithValue("@Password", txtPassword.Text);
                        command.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                        command.Parameters.AddWithValue("@Zona", cboZona.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Equipo", cboEquipo.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Rol", cboRol.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Pais", cboPais.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Permiso", cboPermiso.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Estado", cboEstado.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Foto", imagenBytes);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Mostrar mensaje de éxito
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("¡Registro agregado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LimpiarCampos();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de SQL al agregar el registro: " + ex.Message, "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Método para verificar si todos los campos tienen valor
        private bool CamposCompletos()
        {
            return !string.IsNullOrWhiteSpace(txtNombreCompleto.Text) &&
                   !string.IsNullOrWhiteSpace(txtApellidoCompleto.Text) &&
                   !string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                   !string.IsNullOrWhiteSpace(txtCedula.Text) &&
                   !string.IsNullOrWhiteSpace(txtPasaporte.Text) &&
                   !string.IsNullOrWhiteSpace(txtCelular.Text) &&
                   !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                   !string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                   cboZona.SelectedItem != null &&
                   cboEquipo.SelectedItem != null &&
                   cboRol.SelectedItem != null &&
                   cboPais.SelectedItem != null &&
                   cboPermiso.SelectedItem != null &&
                   cboEstado.SelectedItem != null &&
                   pbFoto.Image != null;
        }

        
        private bool ContieneArroba(string texto)
        {
            return texto.Contains("@");
        }

        private bool CumpleRequisitos(string texto)
        {
            // Verificar si el texto tiene al menos un carácter en mayúscula, letras, números y un carácter especial
            bool tieneMayuscula = texto.Any(char.IsUpper);
            bool tieneLetrasYNumeros = texto.Any(c => char.IsLetterOrDigit(c));
            bool tieneEspecial = texto.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));

            return tieneMayuscula && tieneLetrasYNumeros && tieneEspecial;
        }

        private void ValidarCampos_TextChanged(object sender, EventArgs e)
        {
            ValidarCampos();
        }

        // Método para verificar si todos los campos tienen valor y establecer la habilitación del botón de grabar
        private void ValidarCampos()
        {
            bool contieneArroba = ContieneArroba(txtCorreo.Text);
            bool cumpleRequisitos = CumpleRequisitos(txtPassword.Text);

            if (contieneArroba && cumpleRequisitos &&
                !string.IsNullOrWhiteSpace(txtNombreCompleto.Text) &&
                !string.IsNullOrWhiteSpace(txtApellidoCompleto.Text) &&
                !string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                !string.IsNullOrWhiteSpace(txtCedula.Text) &&
                !string.IsNullOrWhiteSpace(txtPasaporte.Text) &&
                !string.IsNullOrWhiteSpace(txtCelular.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                cboZona.SelectedItem != null &&
                cboEquipo.SelectedItem != null &&
                cboRol.SelectedItem != null &&
                cboPais.SelectedItem != null &&
                cboPermiso.SelectedItem != null &&
                cboEstado.SelectedItem != null &&
                pbFoto.Image != null)
            {
                btnGrabar.Enabled = true;
                btnGrabar.BackColor = Color.Green;
            }
            else
            {
                btnGrabar.Enabled = false;
                btnGrabar.BackColor = DefaultBackColor;
            }
        }


        


    }
}