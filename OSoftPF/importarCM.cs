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
using ExcelDataReader;

namespace OSoftPF
{
    public partial class ImportarCM : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public ImportarCM()
        {
            InitializeComponent();

            btnGuardar.Enabled = false;
        }

        private void ImportarCM_Load(object sender, EventArgs e)
        {
            TemporadasCM();

            cboTemporada.DropDownStyle = ComboBoxStyle.DropDownList;
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

                    Limpieza();

                    // Cerrar el formulario
                    this.Close();
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

            int.TryParse(row.Cells[19].Value?.ToString(), out int boy);
            int.TryParse(row.Cells[20].Value?.ToString(), out int girl);

            // Obtener el valor seleccionado en cboTemporada
            string temporada = cboTemporada.SelectedItem?.ToString();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = connection;

                cmd.CommandText = @"
                    INSERT INTO Organizacion (NombreOrganizacion, DocumentoOrganizacion, TelefonoOficina, DireccionFisica, CorreoElectronico, TipoOrganizacion, Denominacion)
                    OUTPUT INSERTED.IdOrganizacion
                    VALUES (@NombreOrganizacion, @DocumentoOrganizacion, @TelefonoOficina, @DireccionFisica, @CorreoElectronico, @TipoOrganizacion, @Denominacion)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@NombreOrganizacion", nombreOrganizacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DocumentoOrganizacion", documentoOrganizacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TelefonoOficina", telefonoOficina ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DireccionFisica", direccionFisica ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CorreoElectronico", correoElectronico ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TipoOrganizacion", tipoOrganizacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Denominacion", denominacion ?? (object)DBNull.Value);
                int idOrganizacion = (int)cmd.ExecuteScalar();

                cmd.CommandText = @"
                    INSERT INTO Pastor (IdOrganizacion, NombrecompletoPastor, DocumentoPastor, DireccionPastor, TelefonoPastor, CelularPastor, CorreoPastor)
                    VALUES (@IdOrganizacion, @NombrecompletoPastor, @DocumentoPastor, @DireccionPastor, @TelefonoPastor, @CelularPastor, @CorreoPastor)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@NombrecompletoPastor", nombrecompletoPastor ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DocumentoPastor", documentoPastor ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DireccionPastor", direccionPastor ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TelefonoPastor", telefonoPastor ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CelularPastor", celularPastor ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CorreoPastor", correoPastor ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Lider (IdOrganizacion, NombrecompletoLider, DocumentoLider, DireccionLider, TelefonoLider, CelularLider, CorreoLider)
                    VALUES (@IdOrganizacion, @NombrecompletoLider, @DocumentoLider, @DireccionLider, @TelefonoLider, @CelularLider, @CorreoLider)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@NombrecompletoLider", nombrecompletoLider ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DocumentoLider", documentoLider ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DireccionLider", direccionLider ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TelefonoLider", telefonoLider ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CelularLider", celularLider ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CorreoLider", correoLider ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO Oportunidades (IdOrganizacion, Boy, Girls)
                    VALUES (@IdOrganizacion, @Boy, @Girl)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@Boy", boy);
                cmd.Parameters.AddWithValue("@Girl", girl);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    INSERT INTO DatosTemporadaCM (IdOrganizacion, ZonaCM, EquipoCM, PaisCM, TemporadaCM)
                    VALUES (@IdOrganizacion, @ZonaCM, @EquipoCM, @PaisCM, @Temporada)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdOrganizacion", idOrganizacion);
                cmd.Parameters.AddWithValue("@ZonaCM", UsuarioConectado.Zona ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EquipoCM", UsuarioConectado.Equipo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PaisCM", UsuarioConectado.Pais ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Temporada", temporada ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            // Verificar si hay filas en el DataGridView para validar
            if (dgvSeleccionados.RowCount == 0)
            {
                MessageBox.Show("No hay filas para validar. Seleccione un archivo primero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

                    string nombreOrganizacion = row.Cells["NombreOrganizacion"].Value?.ToString();
                    if (nombreOrganizacion != null && ExisteEnTabla(connection, "Organizacion", "NombreOrganizacion", nombreOrganizacion))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        rowHighlighted = true;
                    }

                    string nombreCompletoPastor = row.Cells["NombrecompletoPastor"].Value?.ToString();
                    if (nombreCompletoPastor != null && ExisteEnTabla(connection, "Pastor", "NombrecompletoPastor", nombreCompletoPastor))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        rowHighlighted = true;
                    }

                    string documentoPastor = row.Cells["DocumentoPastor"].Value?.ToString();
                    if (documentoPastor != null && ExisteEnTabla(connection, "Pastor", "DocumentoPastor", documentoPastor))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        rowHighlighted = true;
                    }

                    if (!rowHighlighted)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }

                bool hayFilaEnRojo = dgvSeleccionados.Rows.Cast<DataGridViewRow>()
                                      .Any(r => r.DefaultCellStyle.BackColor == Color.Red);

                btnGuardar.Enabled = !hayFilaEnRojo;
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

        private void DgvSeleccionados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSeleccionados.Rows[e.RowIndex];

                string[] datos = new string[21];
                for (int i = 0; i < datos.Length; i++)
                {
                    datos[i] = row.Cells[i].Value?.ToString() ?? string.Empty;
                }

                Evaluacion evaluacionForm = new Evaluacion();
                evaluacionForm.SetDatos(datos);
                evaluacionForm.Show();
            }
        }

        private void TemporadasCM()
        {
            // Cargar el valor de CodigoTemporada en el ComboBox
            cboTemporada.Items.Add(UsuarioConectado.CodigoTemporada);
            if (cboTemporada.Items.Count > 0)
            {
                cboTemporada.SelectedIndex = 0; // Seleccionar el primer (y único) elemento
            }
        }

        private void Limpieza()
        {
            // Limpiar el DataGridView
            dgvSeleccionados.DataSource = null;
            dgvSeleccionados.Rows.Clear();
            dgvSeleccionados.Columns.Clear();

        }
    }
}
