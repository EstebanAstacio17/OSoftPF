using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;
using System.Data.SqlClient;

namespace OSoftPF
{
    public partial class Herramientas : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public Herramientas()
        {
            InitializeComponent();
        }

        private void Herramientas_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarCboHerramientas();
        }

        private void NoEditarComboBoxes()
        {
            // Configurar ComboBox para ser de solo lectura
            cboHerramienta.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LlenarCboHerramientas()
        {
            // Llenar cboHerramienta con opciones válidas
            cboHerramienta.Items.Add("Zonas");
            cboHerramienta.Items.Add("Equipos");
            cboHerramienta.Items.Add("Roles");
            cboHerramienta.Items.Add("Paises");
        }

        private void cboHerramienta_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCampos(); // Limpiar campos antes de cargar nuevos datos

            if (cboHerramienta.SelectedItem != null)
            {
                string selectedOption = cboHerramienta.SelectedItem.ToString();

                switch (selectedOption)
                {
                    case "Zonas":
                        LoadZonasData();
                        break;
                    case "Equipos":
                        LoadEquiposData();
                        break;
                    case "Roles":
                        LoadRolesData();
                        break;
                    case "Paises":
                        LoadPaisesData();
                        break;
                    default:
                        MessageBox.Show("Opción no válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void LoadZonasData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Zonas"; // Cambia 'Zonas' por el nombre de tu tabla
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvHerramienta.DataSource = dataTable;

                    AdjustColumnSizes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        private void LoadEquiposData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Equipos";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvHerramienta.DataSource = dataTable;

                    AdjustColumnSizes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        private void LoadRolesData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Roles";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvHerramienta.DataSource = dataTable;

                    AdjustColumnSizes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        private void LoadPaisesData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Paises";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvHerramienta.DataSource = dataTable;

                    AdjustColumnSizes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private bool ValidarCasillasLlenas()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
            string.IsNullOrWhiteSpace(txtNombre.Text) ||
            cboHerramienta.SelectedIndex == -1 ||
            cboEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar que los TextBox y ComboBox no estén vacíos
            if (!ValidarCasillasLlenas())
            {
                return;
            }

            // Identificar la opción seleccionada en cboHerramienta y llamar al método correspondiente
            string selectedOption = cboHerramienta.SelectedItem.ToString();
            switch (selectedOption)
            {
                case "Zonas":
                    InsertarEnZonas();
                    break;
                case "Equipos":
                    InsertarEnEquipos();
                    break;
                case "Roles":
                    InsertarEnRoles();
                    break;
                case "Paises":
                    InsertarEnPaises();
                    break;
                default:
                    MessageBox.Show("Opción no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private void InsertarEnZonas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar si el código ya existe en la base de datos
                    string checkQuery = "SELECT COUNT(*) FROM Zonas WHERE CodigoZona = @Codigo";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("El código ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LimpiarCampos();
                            return;
                        }
                    }

                    // Si el código no existe, proceder con la inserción
                    string query = "INSERT INTO Zonas (CodigoZona, NombreZona, EstadoZona) VALUES (@Codigo, @Nombre, @Estado)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Estado", cboEstado.SelectedItem.ToString());

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadZonasData();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Error de SQL al agregar el registro: " + sqlEx.Message, "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el registro: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertarEnEquipos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar si el código ya existe en la base de datos
                    string checkQuery = "SELECT COUNT(*) FROM Equipos WHERE CodigoEquipo = @Codigo";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("El código ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LimpiarCampos();
                            return;
                        }
                    }

                    // Si el código no existe, proceder con la inserción
                    string query = "INSERT INTO Equipos (CodigoEquipo, NombreEquipo, EstadoEquipo) VALUES (@Codigo, @Nombre, @Estado)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Estado", cboEstado.SelectedItem.ToString());

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadEquiposData();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Error de SQL al agregar el registro: " + sqlEx.Message, "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el registro: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertarEnRoles()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar si el código ya existe en la base de datos
                    string checkQuery = "SELECT COUNT(*) FROM Roles WHERE CodigoRol = @Codigo";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("El código ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LimpiarCampos();
                            return;
                        }
                    }

                    // Si el código no existe, proceder con la inserción
                    string query = "INSERT INTO Roles (CodigoRol, NombreRol, EstadoRol) VALUES (@Codigo, @Nombre, @Estado)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Estado", cboEstado.SelectedItem.ToString());

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadRolesData();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Error de SQL al agregar el registro: " + sqlEx.Message, "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el registro: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertarEnPaises()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar si el código ya existe en la base de datos
                    string checkQuery = "SELECT COUNT(*) FROM Paises WHERE CodigoPais = @Codigo";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("El código ya existe en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LimpiarCampos();
                            return;
                        }
                    }

                    // Si el código no existe, proceder con la inserción
                    string query = "INSERT INTO Paises (CodigoPais, NombrePais, EstadoPais) VALUES (@Codigo, @Nombre, @Estado)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Codigo", txtCodigo.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Estado", cboEstado.SelectedItem.ToString());

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadPaisesData();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Error de SQL al agregar el registro: " + sqlEx.Message, "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el registro: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();

            cboEstado.SelectedIndex = -1;
        }

        private void AdjustColumnSizes()
        {
            Dictionary<string, int> columnSizes = new Dictionary<string, int>
            {
                { "IdZona", 30 },
                { "CodigoZona", 50 },
                { "NombreZona", 218 },
                { "EstadoZona", 55 },
                { "IdEquipo", 30 },
                { "CodigoEquipo", 50 },
                { "NombreEquipo", 218 },
                { "EstadoEquipo", 55 },
                { "IdRol", 30 },
                { "CodigoRol", 50 },
                { "NombreRol", 218 },
                { "EstadoRol", 55 },
                { "IdPais", 30 },
                { "CodigoPais", 50 },
                { "NombrePais", 218 },
                { "EstadoPais", 55 }
            };

            foreach (DataGridViewColumn column in dgvHerramienta.Columns)
            {
                if (columnSizes.ContainsKey(column.Name))
                {
                    column.Width = columnSizes[column.Name];
                }
            }
        }

        private void dgvHerramienta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se hizo clic en una fila válida
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHerramienta.Rows[e.RowIndex];

                // Asignar los valores de las celdas a los TextBox y ComboBox
                txtCodigo.Text = row.Cells[1].Value.ToString();
                txtNombre.Text = row.Cells[2].Value.ToString();

                string estado = row.Cells[3].Value.ToString();
                cboEstado.SelectedIndex = cboEstado.FindStringExact(estado);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Verificar qué opción está seleccionada en el ComboBox
            string opcionSeleccionada = cboHerramienta.SelectedItem.ToString();

            // Según la opción seleccionada, llama al método correspondiente
            switch (opcionSeleccionada)
            {
                case "Zonas":
                    ActualizarEnZonas();
                    break;
                case "Equipos":
                    ActualizarEnEquipos();
                    break;
                case "Roles":
                    ActualizarEnRoles();
                    break;
                case "Paises":
                    ActualizarEnPaises();
                    break;
                default:
                    MessageBox.Show("Opción no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private bool DatosValidos()
        {
            return !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                   !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                   cboEstado.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(cboEstado.SelectedItem.ToString());
        }

        private void ActualizarEnZonas()
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvHerramienta.SelectedRows.Count > 0)
            {
                // Verificar si los datos son válidos
                if (!DatosValidos())
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener los valores de los controles
                string nuevoValorCodigo = txtCodigo.Text;
                string nuevoValorNombre = txtNombre.Text;
                string nuevoValorEstado = cboEstado.SelectedItem.ToString();

                // Obtener el ID del registro seleccionado en el DataGridView
                int IdZona = Convert.ToInt32(dgvHerramienta.SelectedRows[0].Cells["IdZona"].Value);

                // Construir la consulta SQL para actualizar el registro
                string query = "UPDATE Zonas SET CodigoZona = @ValorCodigo, NombreZona = @ValorNombre, EstadoZona = @ValorEstado WHERE IdZona = @IdZona";

                // Confirmar la actualización con el usuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Ejecutar la consulta SQL para actualizar el registro
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Asignar los parámetros
                                command.Parameters.AddWithValue("@ValorCodigo", nuevoValorCodigo);
                                command.Parameters.AddWithValue("@ValorNombre", nuevoValorNombre);
                                command.Parameters.AddWithValue("@ValorEstado", nuevoValorEstado);
                                command.Parameters.AddWithValue("@IdZona", IdZona);

                                // Ejecutar la consulta
                                int rowsAffected = command.ExecuteNonQuery();

                                // Mostrar mensaje de éxito
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("¡Registro actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoadZonasData();
                                    LimpiarCampos();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un registro para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEnEquipos()
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvHerramienta.SelectedRows.Count > 0)
            {
                // Verificar si los datos son válidos usando un método externo
                if (!DatosValidos())
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener los valores de los controles
                string nuevoValorCodigo = txtCodigo.Text;
                string nuevoValorNombre = txtNombre.Text;

                // Verificar si se ha seleccionado un estado en el ComboBox
                string nuevoValorEstado = cboEstado.SelectedItem != null ? cboEstado.SelectedItem.ToString() : "";

                // Verificar si se seleccionó un estado válido
                if (string.IsNullOrEmpty(nuevoValorEstado))
                {
                    MessageBox.Show("Por favor, selecciona un estado válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el ID del registro seleccionado en el DataGridView
                int IdEquipo = Convert.ToInt32(dgvHerramienta.SelectedRows[0].Cells["IdEquipo"].Value);

                // Construir la consulta SQL para actualizar el registro
                string query = "UPDATE Equipos SET CodigoEquipo = @ValorCodigo, NombreEquipo = @ValorNombre, EstadoEquipo = @ValorEstado WHERE IdEquipo = @IdEquipo";

                // Confirmar la actualización con el usuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Ejecutar la consulta SQL para actualizar el registro
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Asignar los parámetros
                                command.Parameters.AddWithValue("@ValorCodigo", nuevoValorCodigo);
                                command.Parameters.AddWithValue("@ValorNombre", nuevoValorNombre);
                                command.Parameters.AddWithValue("@ValorEstado", nuevoValorEstado);
                                command.Parameters.AddWithValue("@IdEquipo", IdEquipo);

                                // Ejecutar la consulta
                                int rowsAffected = command.ExecuteNonQuery();

                                // Mostrar mensaje de éxito
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("¡Registro actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoadEquiposData();
                                    LimpiarCampos();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un registro para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEnPaises()
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvHerramienta.SelectedRows.Count > 0)
            {
                // Verificar si los datos son válidos usando un método externo
                if (!DatosValidos())
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener los valores de los controles
                string nuevoValorCodigo = txtCodigo.Text;
                string nuevoValorNombre = txtNombre.Text;

                // Verificar si se ha seleccionado un estado en el ComboBox
                string nuevoValorEstado = cboEstado.SelectedItem != null ? cboEstado.SelectedItem.ToString() : "";

                // Verificar si se seleccionó un estado válido
                if (string.IsNullOrEmpty(nuevoValorEstado))
                {
                    MessageBox.Show("Por favor, selecciona un estado válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el ID del registro seleccionado en el DataGridView
                int IdPais = Convert.ToInt32(dgvHerramienta.SelectedRows[0].Cells["IdPais"].Value);

                // Construir la consulta SQL para actualizar el registro
                string query = "UPDATE Paises SET CodigoPais = @ValorCodigo, NombrePais = @ValorNombre, EstadoPais = @ValorEstado WHERE IdPais = @IdPais";

                // Confirmar la actualización con el usuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Ejecutar la consulta SQL para actualizar el registro
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Asignar los parámetros
                                command.Parameters.AddWithValue("@ValorCodigo", nuevoValorCodigo);
                                command.Parameters.AddWithValue("@ValorNombre", nuevoValorNombre);
                                command.Parameters.AddWithValue("@ValorEstado", nuevoValorEstado);
                                command.Parameters.AddWithValue("@IdPais", IdPais);

                                // Ejecutar la consulta
                                int rowsAffected = command.ExecuteNonQuery();

                                // Mostrar mensaje de éxito
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("¡Registro actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoadPaisesData();
                                    LimpiarCampos();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un registro para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEnRoles()
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dgvHerramienta.SelectedRows.Count > 0)
            {
                // Verificar si los datos son válidos usando un método externo
                if (!DatosValidos())
                {
                    MessageBox.Show("Por favor, completa todos los campos antes de actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener los valores de los controles
                string nuevoValorCodigo = txtCodigo.Text;
                string nuevoValorNombre = txtNombre.Text;

                // Verificar si se ha seleccionado un estado en el ComboBox
                string nuevoValorEstado = cboEstado.SelectedItem != null ? cboEstado.SelectedItem.ToString() : "";

                // Verificar si se seleccionó un estado válido
                if (string.IsNullOrEmpty(nuevoValorEstado))
                {
                    MessageBox.Show("Por favor, selecciona un estado válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el ID del registro seleccionado en el DataGridView
                int IdRol = Convert.ToInt32(dgvHerramienta.SelectedRows[0].Cells["IdRol"].Value);

                // Construir la consulta SQL para actualizar el registro
                string query = "UPDATE Roles SET CodigoRol = @ValorCodigo, NombreRol = @ValorNombre, EstadoRol = @ValorEstado WHERE IdRol = @IdRol";

                // Confirmar la actualización con el usuario
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar el registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Ejecutar la consulta SQL para actualizar el registro
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Asignar los parámetros
                                command.Parameters.AddWithValue("@ValorCodigo", nuevoValorCodigo);
                                command.Parameters.AddWithValue("@ValorNombre", nuevoValorNombre);
                                command.Parameters.AddWithValue("@ValorEstado", nuevoValorEstado);
                                command.Parameters.AddWithValue("@IdRol", IdRol);

                                // Ejecutar la consulta
                                int rowsAffected = command.ExecuteNonQuery();

                                // Mostrar mensaje de éxito
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("¡Registro actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoadRolesData();
                                    LimpiarCampos();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo actualizar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un registro para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}