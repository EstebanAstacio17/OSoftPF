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
using System.Reflection.Emit;

namespace OSoftPF
{
    public partial class MenuPrincipal : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        // Propiedad para almacenar el idusuario
        public int IdUsuario { get; set; }
        public MenuPrincipal(int idUsuario)
        {
            InitializeComponent();

            IdUsuario = idUsuario;

            FillLabels();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            VistaDeAdministrador();

            AdministrarControlUsuarios();
        }

        private void MenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Registrar salida en la tabla Sesiones
            RegistrarSalidaSesion(IdUsuario, lblUsuario.Text);

            Application.Exit(); // Cierra la aplicación por completo cuando se cierra este formulario
        }
        
       
        private void FillLabels()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT NombreCompleto, ApellidoCompleto, Usuario, Permiso, Zona, Equipo, Rol, Pais FROM Usuarios WHERE idusuario = @idUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Obtiene la primera palabra del NombreCompleto y ApellidoCompleto
                                string nombreCompleto = reader["NombreCompleto"].ToString();
                                string apellidoCompleto = reader["ApellidoCompleto"].ToString();
                                string primerNombre = nombreCompleto.Split(' ')[0];
                                string primerApellido = apellidoCompleto.Split(' ')[0];

                                // Asigna los valores de las columnas a las etiquetas
                                lblNombre.Text = primerNombre;
                                lblApellido.Text = primerApellido;
                                lblUsuario.Text = reader["Usuario"].ToString();
                                lblPermiso.Text = reader["Permiso"].ToString();
                                lblId.Text = "ID: " + IdUsuario.ToString();
                                lblHerramientas.Text = $"{reader["Zona"]} - {reader["Equipo"]} - {reader["Rol"]} - {reader["Pais"]}";
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron datos para el usuario actual.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
            }
        }


        private void btnControlUsuarios_Click(object sender, EventArgs e)
        {
            // Verificar el permiso del label
            if (lblPermiso.Text == "Administrador" || lblPermiso.Text == "Coordinador")
            {
                // Verificar el rol del usuario en la base de datos
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT Rol FROM usuarios WHERE idusuario = @idUsuario";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string codigoRol = reader["Rol"].ToString();
                                    if (codigoRol == "CE")
                                    {
                                        // Si el permiso es "Administrador" o "Coordinador" y el rol es "CE", permitir la ejecución
                                        ControlUsuarios controlUsuarios = new ControlUsuarios();
                                        controlUsuarios.ShowDialog();
                                    }
                                    else
                                    {
                                        // Si el rol no es "CE", deshabilitar el botón
                                        MessageBox.Show("No tiene los permisos necesarios para acceder a esta función.");
                                        btnControlUsuarios.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
                }
            }
            else
            {
                // Si el permiso no es "Administrador" o "Coordinador", deshabilitar el botón
                MessageBox.Show("No tiene los permisos necesarios para acceder a esta función.");
                btnControlUsuarios.Enabled = false;
            }

        }

        private void labelPermiso_TextChanged(object sender, EventArgs e)
        {
            // Verifica la visibilidad del botón cada vez que el texto del label cambie
            VistaDeAdministrador();
        }

        private void VistaDeAdministrador()
        {
            // Verifica si el texto del label es igual a "Administrador"
            if (lblPermiso.Text == "Administrador")
            {
                // Si es igual, muestra el botón y lo habilita
                btnAdministrador.Visible = true;
                btnAdministrador.Enabled = true;
            }
            else
            {
                // Si no es igual, oculta el botón y lo deshabilita
                btnAdministrador.Visible = false;
                btnAdministrador.Enabled = false;
            }
        }

        private void btnAdministrador_Click(object sender, EventArgs e)
        {
            Administrador administrador = new Administrador();
            administrador.ShowDialog();
        }


        private void AdministrarControlUsuarios()
        {// Verificar el permiso del label y el rol del usuario al cargar el formulario
            if (lblPermiso.Text == "Administrador" || lblPermiso.Text == "Coordinador")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT Rol FROM usuarios WHERE idusuario = @idUsuario";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string codigoRol = reader["Rol"].ToString();
                                    if (codigoRol != "CE")
                                    {
                                        // Si el rol no es "CE", deshabilitar el botón
                                        btnControlUsuarios.Enabled = false;
                                    }
                                }
                                else
                                {
                                    // Si no se encuentra el usuario, deshabilitar el botón
                                    btnControlUsuarios.Enabled = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
                    btnControlUsuarios.Enabled = false;
                }
            }
            else
            {
                // Si el permiso no es "Administrador" o "Coordinador", deshabilitar el botón
                btnControlUsuarios.Enabled = false;
            }
        }


        private void RegistrarSalidaSesion(int idUsuario, string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Sesiones (IdUsuario, Usuario, Accion) VALUES (@IdUsuario, @Usuario, @Accion)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@Usuario", username);
                        cmd.Parameters.AddWithValue("@Accion", "Salida");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la sesión: " + ex.Message);
            }
        }

    }
}
