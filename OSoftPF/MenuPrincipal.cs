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
    public partial class MenuPrincipal : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;

        public MenuPrincipal(int idUsuario)
        {
            InitializeComponent();

            UsuarioConectado.IdUsuario = idUsuario;

            FillLabels();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            VistaDeAdministrador();

            AdministrarControlUsuarios();

            ConfiguracionBotonConfiguracion();

            MostrarCodigoTemporadaReciente();
        }

        private void MenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistrarSalidaSesion(UsuarioConectado.IdUsuario, UsuarioConectado.Usuario);
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
                        cmd.Parameters.AddWithValue("@idUsuario", UsuarioConectado.IdUsuario);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Almacena los valores en la clase estática UsuarioInfo
                                UsuarioConectado.NombreCompleto = reader["NombreCompleto"].ToString();
                                UsuarioConectado.ApellidoCompleto = reader["ApellidoCompleto"].ToString();
                                UsuarioConectado.Usuario = reader["Usuario"].ToString();
                                UsuarioConectado.Permiso = reader["Permiso"].ToString();
                                UsuarioConectado.Zona = reader["Zona"].ToString();
                                UsuarioConectado.Equipo = reader["Equipo"].ToString();
                                UsuarioConectado.Rol = reader["Rol"].ToString();
                                UsuarioConectado.Pais = reader["Pais"].ToString();

                                // Obtiene la primera palabra del NombreCompleto y ApellidoCompleto
                                string primerNombre = UsuarioConectado.NombreCompleto.Split(' ')[0];
                                string primerApellido = UsuarioConectado.ApellidoCompleto.Split(' ')[0];

                                // Asigna los valores de las columnas a las etiquetas
                                lblNombre.Text = primerNombre;
                                lblApellido.Text = primerApellido;
                                lblUsuario.Text = UsuarioConectado.Usuario;
                                lblPermiso.Text = UsuarioConectado.Permiso;
                                lblId.Text = "ID: " + UsuarioConectado.IdUsuario.ToString();
                                lblHerramientas.Text = $"{UsuarioConectado.Zona} - {UsuarioConectado.Equipo} - {UsuarioConectado.Rol} - {UsuarioConectado.Pais}";
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
            if (UsuarioConectado.Permiso == "Administrador" || UsuarioConectado.Permiso == "Coordinador")
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
                            cmd.Parameters.AddWithValue("@idUsuario", UsuarioConectado.IdUsuario);
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
            if (UsuarioConectado.Permiso == "Administrador")
            {
                // Si es igual, muestra el botón y lo habilita
                btnAdministracion.Visible = true;
                btnAdministracion.Enabled = true;
            }
            else
            {
                // Si no es igual, oculta el botón y lo deshabilita
                btnAdministracion.Visible = false;
                btnAdministracion.Enabled = false;
            }
        }

        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            Administrador administrador = new Administrador();
            administrador.ShowDialog();
        }

        private void AdministrarControlUsuarios()
        {
            // Verificar el permiso del label y el rol del usuario al cargar el formulario
            if (UsuarioConectado.Permiso == "Administrador" || UsuarioConectado.Permiso == "Coordinador")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT Rol FROM usuarios WHERE idusuario = @idUsuario";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idUsuario", UsuarioConectado.IdUsuario);
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

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Configuracion configuracion = new Configuracion();
            configuracion.ShowDialog();
        }

        private void ConfiguracionBotonConfiguracion()
        {
            // Verifica si el usuario es "juastacio" y tiene permiso de "Administrador"
            if (UsuarioConectado.Usuario == "JUASTACIO" && UsuarioConectado.Permiso == "Administrador")
            {
                // Si cumple las condiciones, muestra el botón y lo habilita
                btnConfiguracion.Visible = true;
                btnConfiguracion.Enabled = true;
            }
            else
            {
                // Si no cumple las condiciones, oculta el botón y lo deshabilita
                btnConfiguracion.Visible = false;
                btnConfiguracion.Enabled = false;
            }
        }

        private void btnMI_Click(object sender, EventArgs e)
        {
            if (UsuarioConectado.Permiso == "Coordinador" || UsuarioConectado.Permiso == "Administrador")
            {
                if (UsuarioConectado.Rol == "CMI" || UsuarioConectado.Rol == "CE")
                {
                    GestionCM openCM = new GestionCM();
                    openCM.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No tiene los permisos necesarios para acceder a esta función.");
                }
            }
            else
            {
                MessageBox.Show("No tiene los permisos necesarios para acceder a esta función.");
            }
        }

        private void MostrarCodigoTemporadaReciente()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 CodigoTemporada FROM Temporada ORDER BY IdTemporada DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string codigoTemporada = reader["CodigoTemporada"].ToString();
                                lblTemporada.Text = codigoTemporada;
                                UsuarioConectado.CodigoTemporada = codigoTemporada; // Guarda el valor en la variable de clase
                            }
                            else
                            {
                                lblTemporada.Text = "No hay temporadas disponibles";
                                UsuarioConectado.CodigoTemporada = null; // Resetea la variable de clase si no hay temporadas
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el código de la temporada más reciente: " + ex.Message);
            }
        }

        private void btnCD_Click(object sender, EventArgs e)
        {
            if (UsuarioConectado.Permiso == "Coordinador" || UsuarioConectado.Permiso == "Administrador")
            {
                if (UsuarioConectado.Rol == "CD" || UsuarioConectado.Rol == "CE")
                {
                    GestionCM openCM = new GestionCM();
                    openCM.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No tiene los permisos necesarios para acceder a esta función.");
                }
            }
            else
            {
                MessageBox.Show("No tiene los permisos necesarios para acceder a esta función.");
            }
        }
    }
}
