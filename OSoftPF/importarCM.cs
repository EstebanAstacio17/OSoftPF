using ClosedXML.Excel;
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
using System.Windows.Forms.VisualStyles;
using System.IO;
using ExcelDataReader;  // Necesitarás instalar ExcelDataReader a través de NuGet

namespace OSoftPF
{
    public partial class ImportarCM : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public ImportarCM()
        {
            InitializeComponent();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            SelectorArchivos();
        }

        private void SelectorArchivos()
        {
            // Crear una nueva instancia del cuadro de diálogo de selección de archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Configurar el filtro para mostrar solo archivos de Excel
            openFileDialog.Filter = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog.Title = "Seleccionar archivo de Excel";

            // Mostrar el cuadro de diálogo y verificar si se seleccionó un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta del archivo seleccionado
                string selectedFilePath = openFileDialog.FileName;

                // Cargar el archivo de Excel en el DataGridView
                CargarExcelEnDataGridView(selectedFilePath);
            }
        }

        private void CargarExcelEnDataGridView(string filePath)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    bool firstRow = true;
                    foreach (var row in worksheet.RowsUsed())
                    {
                        if (firstRow)
                        {
                            foreach (var cell in row.CellsUsed())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            var dataRow = dt.NewRow();
                            int i = 0;
                            foreach (var cell in row.CellsUsed())
                            {
                                dataRow[i] = cell.Value.ToString();
                                i++;
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }

                dgvSeleccionados.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el archivo de Excel: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatosEnBaseDeDatos();
        }

        private void GuardarDatosEnBaseDeDatos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (DataGridViewRow row in dgvSeleccionados.Rows)
                    {
                        if (row.IsNewRow) continue;
                        try
                        {
                            GuardarFilaEnBaseDeDatos(connection, row);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al guardar la fila: " + ex.Message);
                        }
                    }
                    MessageBox.Show("Datos guardados exitosamente.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message);
            }
        }

        private void GuardarFilaEnBaseDeDatos(SqlConnection connection, DataGridViewRow row)
        {
            if (!ValidarFila(row)) return;

            string nombreOrganizacion = row.Cells[0].Value?.ToString();
            string documentoOrganizacion = row.Cells[1].Value?.ToString();
            string telefonoOficina = row.Cells[2].Value?.ToString();
            string direccionFisica = row.Cells[3].Value?.ToString();
            string correoElectronico = row.Cells[4].Value?.ToString();
            string tipoOrganizacion = row.Cells[5].Value?.ToString();
            string denominacion = row.Cells[6].Value?.ToString();

            string nombrecompletoPastor = row.Cells[7].Value?.ToString();
            string documentoPastor = row.Cells[8].Value?.ToString();
            string direccionPastor = row.Cells[9].Value?.ToString();
            string telefonoPastor = row.Cells[10].Value?.ToString();
            string celularPastor = row.Cells[11].Value?.ToString();
            string correoPastor = row.Cells[12].Value?.ToString();

            string nombrecompletoLider = row.Cells[13].Value?.ToString();
            string documentoLider = row.Cells[14].Value?.ToString();
            string direccionLider = row.Cells[15].Value?.ToString();
            string telefonoLider = row.Cells[16].Value?.ToString();
            string celularLider = row.Cells[17].Value?.ToString();
            string correoLider = row.Cells[18].Value?.ToString();

            if (!int.TryParse(row.Cells[19].Value?.ToString(), out int boy))
            {
                MessageBox.Show("Error en el formato del campo 'Boy'");
                return;
            }
            if (!int.TryParse(row.Cells[20].Value?.ToString(), out int girl))
            {
                MessageBox.Show("Error en el formato del campo 'Girls'");
                return;
            }

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = @"
                    INSERT INTO Organizacion (NombreOrganizacion, DocumentoOrganizacion, TelefonoOficina, DireccionFisica, CorreoElectronico, TipoOrganizacion, Denominacion)
                    OUTPUT INSERTED.IdOrganizacion
                    VALUES (@NombreOrganizacion, @DocumentoOrganizacion, @TelefonoOficina, @DireccionFisica, @CorreoElectronico, @TipoOrganizacion, @Denominacion)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@NombreOrganizacion", nombreOrganizacion);
                cmd.Parameters.AddWithValue("@DocumentoOrganizacion", documentoOrganizacion);
                cmd.Parameters.AddWithValue("@TelefonoOficina", telefonoOficina);
                cmd.Parameters.AddWithValue("@DireccionFisica", direccionFisica);
                cmd.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                cmd.Parameters.AddWithValue("@TipoOrganizacion", tipoOrganizacion);
                cmd.Parameters.AddWithValue("@Denominacion", denominacion);
                int idOrganizacion = (int)cmd.ExecuteScalar();

                cmd.CommandText = @"
                    INSERT INTO Pastor (IdOrganizacion, NombrecompletoPastor, DocumentoPastor, DireccionPastor, TelefonoPastor, CelularPastor, CorreoPastor)
                    VALUES (@IdOrganizacion, @NombrecompletoPastor, @DocumentoPastor, @DireccionPastor, @TelefonoPastor, @CelularPastor, @CorreoPastor)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@NombrecompletoPastor", nombrecompletoPastor);
                cmd.Parameters.AddWithValue("@DocumentoPastor", documentoPastor);
                cmd.Parameters.AddWithValue("@DireccionPastor", direccionPastor);
                cmd.Parameters.AddWithValue("@TelefonoPastor", telefonoPastor);
                cmd.Parameters.AddWithValue("@CelularPastor", celularPastor);
                cmd.Parameters.AddWithValue("@CorreoPastor", correoPastor);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Lider (IdOrganizacion, NombrecompletoLider, DocumentoLider, DireccionLider, TelefonoLider, CelularLider, CorreoLider)
                    VALUES (@IdOrganizacion, @NombrecompletoLider, @DocumentoLider, @DireccionLider, @TelefonoLider, @CelularLider, @CorreoLider)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@NombrecompletoLider", nombrecompletoLider);
                cmd.Parameters.AddWithValue("@DocumentoLider", documentoLider);
                cmd.Parameters.AddWithValue("@DireccionLider", direccionLider);
                cmd.Parameters.AddWithValue("@TelefonoLider", telefonoLider);
                cmd.Parameters.AddWithValue("@CelularLider", celularLider);
                cmd.Parameters.AddWithValue("@CorreoLider", correoLider);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Oportunidades (IdOrganizacion, Boy, Girls)
                    VALUES (@IdOrganizacion, @Boy, @Girl)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@Boy", boy);
                cmd.Parameters.AddWithValue("@Girl", girl);
                cmd.ExecuteNonQuery();
            }

            this.Close();
        }


        private bool ValidarFila(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    MessageBox.Show("Hay campos vacíos en la fila.");
                    return false;
                }
            }
            return true;
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            ValidarDatos();
        }

        private void ValidarDatos()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow row in dgvSeleccionados.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool rowHighlighted = false;

                    // Validar NombreOrganizacion
                    string nombreOrganizacion = row.Cells["NombreOrganizacion"].Value?.ToString();
                    if (nombreOrganizacion != null && ExisteEnTabla(connection, "Organizacion", "NombreOrganizacion", nombreOrganizacion))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        rowHighlighted = true;
                    }

                    // Validar NombrecompletoPastor
                    string nombreCompletoPastor = row.Cells["NombrecompletoPastor"].Value?.ToString();
                    if (nombreCompletoPastor != null && ExisteEnTabla(connection, "Pastor", "NombrecompletoPastor", nombreCompletoPastor))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        rowHighlighted = true;
                    }

                    // Validar DocumentoPastor
                    string documentoPastor = row.Cells["DocumentoPastor"].Value?.ToString();
                    if (documentoPastor != null && ExisteEnTabla(connection, "Pastor", "DocumentoPastor", documentoPastor))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        rowHighlighted = true;
                    }

                    // Si la fila no ha sido resaltada, cambiar el color de fondo a blanco
                    if (!rowHighlighted)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private bool ExisteEnTabla(SqlConnection connection, string tableName, string columnName, string value)
        {
            string query = $"SELECT COUNT(1) FROM {tableName} WHERE {columnName} = @value";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@value", value);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnEvaluar_Click(object sender, EventArgs e)
        {

        }
    }
}
