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


namespace OSoftPF
{
    public partial class ControlUsuarios : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public ControlUsuarios()
        {
            InitializeComponent();
        }

        private void ControlUsuarios_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarComboBox();

            MostrarUsuarios();

            DisyngDgv();

            LimiteDeTextBoxes();
        }

        private void NoEditarComboBoxes()
        {
            // Configurar ComboBox para ser de solo lectura
            cboZonas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRol.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LlenarComboBox()
        {
            OpcionesDeZonas();
            OpcionesDeEquipos();
            OpcionesDeRoles();
        }

        private void OpcionesDeZonas()
        {
            // Clear any existing items in the ComboBox
            cboZonas.Items.Clear();

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
                                cboZonas.Items.Add(reader["CodigoZona"].ToString());
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            PerfilUsuario perfilUsuario = new PerfilUsuario();

            // Suscribirse al evento FormClosedEvent
            perfilUsuario.FormClosedEvent += PerfilUsuario_FormClosed;


            perfilUsuario.ShowDialog();
        }

        // Método manejador del evento FormClosedEvent
        private void PerfilUsuario_FormClosed()
        {
            MostrarUsuarios();
        }

        public void MostrarUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL query to retrieve specific columns from your table
                    string query = "SELECT IdUsuario, NombreCompleto, ApellidoCompleto, Zona, Equipo, Rol, Usuario, Correo, Estado, Permiso FROM Usuarios";

                    // Create a SqlDataAdapter to execute the query and fill a DataTable
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the result of the SQL query
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dgvUsuarios.DataSource = dataTable;

                    // Optionally, adjust the display properties of the DataGridView columns
                    dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    // Set specific widths for each column
                    dgvUsuarios.Columns["IdUsuario"].Width = 30;
                    dgvUsuarios.Columns["NombreCompleto"].Width = 130;
                    dgvUsuarios.Columns["ApellidoCompleto"].Width = 130;
                    dgvUsuarios.Columns["Zona"].Width = 50;
                    dgvUsuarios.Columns["Equipo"].Width = 55;
                    dgvUsuarios.Columns["Rol"].Width = 35;
                    dgvUsuarios.Columns["Usuario"].Width = 112;
                    dgvUsuarios.Columns["Correo"].Width = 220;
                    dgvUsuarios.Columns["Estado"].Width = 75;
                    dgvUsuarios.Columns["Permiso"].Width = 100;
                }
                catch (SqlException sqlEx)
                {
                    // Handle SQL specific errors
                    MessageBox.Show("Error al conectar con la base de datos: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Handle any other errors that may have occurred
                    MessageBox.Show("Error al llenar el DataGridView: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void DisyngDgv()
        {
            // Establecer el tamaño de la fuente para el DataGridView
            dgvUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10); // Cambia "Arial" por la fuente que desees y "12" por el tamaño deseado
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

            // Llamar al método MostrarUsuarios para actualizar el DataGridView
            MostrarUsuarios();
        }

        private void LimpiarCampos()
        {
            txtBuscarUsuario.Clear();

            cboZonas.SelectedIndex = -1;
            cboEquipo.SelectedIndex = -1;
            cboRol.SelectedIndex = -1;


        }

        private void LimiteDeTextBoxes()
        {
            // Establecer la propiedad MaxLength de cada TextBox a 20 caracteres
            txtBuscarUsuario.MaxLength = 20;
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {           
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dgvUsuarios.Rows[e.RowIndex];

                // Obtener el valor de la columna "IdUsuario"
                string idUsuarioEditar = selectedRow.Cells["IdUsuario"].Value.ToString();

                // Crear una instancia del formulario EditarUsuario y pasarle el valor de IdUsuario
                EditarUsuario editarUsuario = new EditarUsuario(idUsuarioEditar);

                // Mostrar el formulario como un diálogo modal
                editarUsuario.ShowDialog();
            }
        }

        private void btnInactivar_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void CambiarEstado()
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                // Obtener el ID del usuario de la fila seleccionada
                int usuarioID = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["IdUsuario"].Value);

                // Confirmar la acción con un cuadro de diálogo
                DialogResult result = MessageBox.Show("¿Está seguro de que desea inactivar el usuario seleccionado?", "Confirmar Inactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Actualizar el estado del usuario en la base de datos
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            string query = "UPDATE Usuarios SET Estado = 'No Activo' WHERE IdUsuario = @UsuarioID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            conn.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("El estado del usuario ha sido actualizado a 'No Activo'.", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se encontró un usuario con el ID especificado.", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        // Llamar al método MostrarUsuarios para actualizar el DataGridView
                        MostrarUsuarios();
                    }
                    catch (SqlException sqlEx)
                    {
                        // Manejar errores específicos de SQL
                        MessageBox.Show("Error al conectar con la base de datos: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        // Manejar cualquier otro error
                        MessageBox.Show("Error al actualizar el estado del usuario: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuarios();
        }

        private void BuscarUsuarios()
        {
            string busqueda = txtBuscarUsuario.Text.Trim();
            if (!string.IsNullOrEmpty(busqueda))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Define the SQL query to retrieve specific columns from your table
                        string query = @"
                            SELECT IdUsuario, NombreCompleto, ApellidoCompleto, Zona, Equipo, Rol, Usuario, Correo, Estado, Permiso 
                            FROM Usuarios 
                            WHERE NombreCompleto LIKE @Busqueda OR ApellidoCompleto LIKE @Busqueda";

                        // Create a SqlDataAdapter to execute the query and fill a DataTable
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");

                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the result of the SQL query
                        dataAdapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dgvUsuarios.DataSource = dataTable;

                        // Optionally, adjust the display properties of the DataGridView columns
                        dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                        // Set specific widths for each column
                        dgvUsuarios.Columns["IdUsuario"].Width = 30;
                        dgvUsuarios.Columns["NombreCompleto"].Width = 130;
                        dgvUsuarios.Columns["ApellidoCompleto"].Width = 130;
                        dgvUsuarios.Columns["Zona"].Width = 50;
                        dgvUsuarios.Columns["Equipo"].Width = 55;
                        dgvUsuarios.Columns["Rol"].Width = 35;
                        dgvUsuarios.Columns["Usuario"].Width = 112;
                        dgvUsuarios.Columns["Correo"].Width = 220;
                        dgvUsuarios.Columns["Estado"].Width = 75;
                        dgvUsuarios.Columns["Permiso"].Width = 100;
                    }


                    catch (SqlException sqlEx)
                    {
                        // Handle SQL specific errors
                        MessageBox.Show("Error al conectar con la base de datos: " + sqlEx.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle any other errors that may have occurred
                        MessageBox.Show("Error al llenar el DataGridView: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor para buscar.", "Entrada requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZonas.SelectedItem != null)
            {
                string zonaSeleccionada = cboZonas.SelectedItem.ToString();
                FiltrarUsuariosPorZona(zonaSeleccionada);
            }
            else
            {
                // Si no hay ninguna selección, mostrar todos los usuarios
                MostrarUsuarios();
            }
        }

        private void FiltrarUsuariosPorZona(string zona)
        {
            DataTable dataTable = dgvUsuarios.DataSource as DataTable;
            if (dataTable != null)
            {
                dataTable.DefaultView.RowFilter = string.Format("Zona = '{0}'", zona);
            }
        }


        private void cboEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEquipo.SelectedItem != null)
            {
                string equipoSeleccionada = cboEquipo.SelectedItem.ToString();
                FiltrarUsuariosPorEquipo(equipoSeleccionada);
            }
            else
            {
                // Si no hay ninguna selección, mostrar todos los usuarios
                MostrarUsuarios();
            }
        }

        private void FiltrarUsuariosPorEquipo(string equipo)
        {
            DataTable dataTable = dgvUsuarios.DataSource as DataTable;
            if (dataTable != null)
            {
                dataTable.DefaultView.RowFilter = string.Format("Equipo = '{0}'", equipo);
            }
        }

        private void cboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRol.SelectedItem != null)
            {
                string rolSeleccionada = cboRol.SelectedItem.ToString();
                FiltrarUsuariosPorRol(rolSeleccionada);
            }
            else
            {
                // Si no hay ninguna selección, mostrar todos los usuarios
                MostrarUsuarios();
            }
        }

        private void FiltrarUsuariosPorRol(string rol)
        {
            DataTable dataTable = dgvUsuarios.DataSource as DataTable;
            if (dataTable != null)
            {
                dataTable.DefaultView.RowFilter = string.Format("Rol = '{0}'", rol);
            }
        }
    }
}
