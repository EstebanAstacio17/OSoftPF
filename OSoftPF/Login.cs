using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OSoftPF
{
    public partial class Login : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        
        public Login()
        {
            InitializeComponent();

            // Inicialmente el botón está deshabilitado
            btnLogin.Enabled = false;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SinEsquinas();

            LimitarTextBoxes();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validar credenciales del usuario aquí
            string username = txtUser.Text;
            string password = txtPassword.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuarios WHERE usuario = @username", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string estado = reader["Estado"].ToString();
                                if (estado != "Activo")
                                {
                                    MessageBox.Show("El usuario no está activo.");
                                    return;
                                }

                                string storedPassword = reader["password"].ToString();
                                if (storedPassword != password)
                                {
                                    MessageBox.Show("La contraseña es incorrecta.");
                                    return;
                                }

                                // Si todas las validaciones son correctas, obtener el idUsuario y registrar la entrada
                                int idUsuario = Convert.ToInt32(reader["idusuario"]);

                                // Registrar entrada en la tabla Sesiones
                                RegistrarEntradaSesion(idUsuario, username);

                                // Abrir el menú principal
                                MenuPrincipal menuPrincipal = new MenuPrincipal(idUsuario);
                                this.Hide();
                                menuPrincipal.ShowDialog();
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("El usuario no existe.");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void TextBoxes_TextChanged(object sender, EventArgs e)
        {
            // Verificar si ambos TextBox tienen valores
            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtUser.Text))
            {
                // Habilitar el botón y cambiar su color a verde
                btnLogin.Enabled = true;
                btnLogin.BackColor = Color.Green;
            }
            else
            {
                // Deshabilitar el botón y restablecer su color
                btnLogin.Enabled = false;
                btnLogin.BackColor = Color.SteelBlue;
            }
        }

        private void LimitarTextBoxes()
        {
            // Configura los TextBox para que tengan un límite de 20 caracteres
            txtUser.MaxLength = 20;
            txtPassword.MaxLength = 30;
        }

        private void SinEsquinas()
        {
            // Set the form's region to a rounded rectangle
            IntPtr hRgn = CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25);
            SetWindowRgn(this.Handle, hRgn, true);
        }

        // Para Redondear Esquinas
        // P/Invoke declarations
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        [DllImport("User32.dll", EntryPoint = "SetWindowRgn")]
        private static extern int SetWindowRgn(
            IntPtr hWnd,       // handle to window
            IntPtr hRgn,       // handle to region
            bool bRedraw       // redraw option
        );

        private void LabelPortaForza_Click(object sender, EventArgs e)
        {
            // URL a la que deseas navegar
            string url = "https://portaforza.com/";

            try
            {
                // Abre la URL en el navegador predeterminado
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el enlace. " + ex.Message);
            }
        }



        private void RegistrarEntradaSesion(int idUsuario, string username)
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
                        cmd.Parameters.AddWithValue("@Accion", "Entrada");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la sesión: " + ex.Message);
            }
        }

        private void TxtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter ingresado es una letra
            if (char.IsLetter(e.KeyChar))
            {
                // Convertir a mayúscula
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            else if (!char.IsControl(e.KeyChar))
            {
                // Si no es una letra o un carácter de control (como Backspace), cancelar el evento
                e.Handled = true;
            }
        }

    }
}

