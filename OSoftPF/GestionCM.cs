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
using ExcelDataReader;
using ClosedXML.Excel;

namespace OSoftPF
{
    public partial class GestionCM : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public GestionCM()
        {
            InitializeComponent();
        }

        private void GestionCM_Load(object sender, EventArgs e)
        {
            NoEditarComboBoxes();

            LlenarComboBoxDeHerramientas();
        }

        private void LlenarComboBoxDeHerramientas()
        {
            OpcionesDeEquipos();
            OpcionesDeZonas();
            OpcionesDeTemporada();
        }

        private void NoEditarComboBoxes()
        {
            cboZona.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTemporada.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void Busqueda()
        {
            string valorBusqueda = txtBusqueda.Text.Trim();
            string equipoSeleccionado = cboEquipo.SelectedItem?.ToString();
            string zonaSeleccionada = cboZona.SelectedItem?.ToString();
            string temporadaSeleccionada = cboTemporada.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(valorBusqueda) && string.IsNullOrEmpty(equipoSeleccionado) && string.IsNullOrEmpty(zonaSeleccionada) && string.IsNullOrEmpty(temporadaSeleccionada))
            {
                MessageBox.Show("Por favor, ingrese un valor de búsqueda o seleccione filtros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT 
                        o.*,
                        p.NombrecompletoPastor,
                        p.DocumentoPastor,
                        p.DireccionPastor,
                        p.TelefonoPastor,
                        p.CelularPastor,
                        p.CorreoPastor,
                        l.NombrecompletoLider,
                        l.DocumentoLider,
                        l.DireccionLider,
                        l.TelefonoLider,
                        l.CelularLider,
                        l.CorreoLider,
                        d.TemporadaCM,
                        d.EquipoCM,
                        d.ZonaCM,
                        d.PaisCM
                    FROM Organizacion o
                    LEFT JOIN Pastor p ON o.IdOrganizacion = p.IdOrganizacion
                    LEFT JOIN Lider l ON o.IdOrganizacion = l.IdOrganizacion
                    LEFT JOIN DatosTemporadaCM d ON o.IdOrganizacion = d.IdOrganizacion
                    WHERE 1=1";

                    if (!string.IsNullOrEmpty(valorBusqueda))
                    {
                        query += " AND (o.NombreOrganizacion LIKE @valorBusqueda OR p.NombrecompletoPastor LIKE @valorBusqueda OR l.NombrecompletoLider LIKE @valorBusqueda)";
                    }

                    if (!string.IsNullOrEmpty(equipoSeleccionado))
                    {
                        query += " AND d.EquipoCM = @equipoSeleccionado";
                    }

                    if (!string.IsNullOrEmpty(zonaSeleccionada))
                    {
                        query += " AND d.ZonaCM = @zonaSeleccionada";
                    }

                    if (!string.IsNullOrEmpty(temporadaSeleccionada))
                    {
                        query += " AND d.TemporadaCM = @temporadaSeleccionada";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(valorBusqueda))
                        {
                            command.Parameters.AddWithValue("@valorBusqueda", "%" + valorBusqueda + "%");
                        }

                        if (!string.IsNullOrEmpty(equipoSeleccionado))
                        {
                            command.Parameters.AddWithValue("@equipoSeleccionado", equipoSeleccionado);
                        }

                        if (!string.IsNullOrEmpty(zonaSeleccionada))
                        {
                            command.Parameters.AddWithValue("@zonaSeleccionada", zonaSeleccionada);
                        }

                        if (!string.IsNullOrEmpty(temporadaSeleccionada))
                        {
                            command.Parameters.AddWithValue("@temporadaSeleccionada", temporadaSeleccionada);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvGestionCM.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar los registros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtBusqueda.Clear();

            cboEquipo.SelectedIndex = -1;
            cboTemporada.SelectedIndex = -1;
            cboZona.SelectedIndex = -1;

            dgvGestionCM.DataSource = null;
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

        private void OpcionesDeTemporada()
        {
            // Clear any existing items in the ComboBox
            cboTemporada.Items.Clear();

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
                                cboTemporada.Items.Add(reader["CodigoTemporada"].ToString());
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            // ExportarDatosAExcel(dgvGestionCM);
            // Validar si hay datos en el DataGridView
            if (dgvGestionCM.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar si se ha seleccionado un archivo para guardar
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Exportación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    ExportarDatosAExcel(dgvGestionCM, sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportarDatosAExcel(DataGridView dgv, string fileName)
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    DataTable dt = new DataTable();

                    // Agregar las columnas del DataGridView al DataTable
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        dt.Columns.Add(column.HeaderText);
                    }

                    // Agregar las filas del DataGridView al DataTable
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;
                        DataRow dataRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dataRow[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                        }
                        dt.Rows.Add(dataRow);
                    }

                    // Agregar DataTable al workbook
                    workbook.Worksheets.Add(dt, "Sheet1");

                    // Guardar el archivo
                    workbook.SaveAs(fileName);

                    MessageBox.Show("Datos exportados exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Error de E/S: " + ioEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                MessageBox.Show("Error de acceso: " + uaEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvGestionCM_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGestionCM.Rows[e.RowIndex];

                int idOrganizacion;
                if (int.TryParse(row.Cells["IdOrganizacion"].Value?.ToString(), out idOrganizacion))
                {
                    ComentarioCM abrirenComentarioCM = new ComentarioCM();
                    abrirenComentarioCM.SetIdOrganizacion(idOrganizacion);
                    abrirenComentarioCM.Show();
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el IdOrganizacion de la fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        
    }
}