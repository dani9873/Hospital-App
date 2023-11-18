using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class Form1 : Form
    {
        private Conexion conexion;
        public Form1()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void linkRestablecerContrasenia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Lógica para restablecer contraseña (implementar según necesidades)
            // ...
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
                string usuario = txtUsuario.Text.Trim();
                string contrasenia = txtContrasenia.Text.Trim();

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasenia))
                {
                    MessageBox.Show("Por favor, ingrese su nombre de usuario y contraseña.", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.


                    string query = "SELECT Rol FROM Usuarios WHERE NombreUsuario = @Usuario AND Contraseña = @Contrasenia";
                    using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Contrasenia", contrasenia);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string rol = result.ToString();

                            // Verificar el rol del usuario para determinar qué formulario mostrar a continuación
                            if (rol.Contains("Administrador"))
                            {
                                MainForm mainForm = new MainForm();
                                mainForm.Show();
                                this.Hide();
                            }
                            else if (rol == "Registrador")
                            {
                                MessageBox.Show("El usuario registrado no tiene acceso al mantenimiento de usuarios.", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Credenciales inválidas. Por favor, verifique su nombre de usuario y contraseña.", "Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar iniciar sesión. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.CerrarConexion(); // Cerrar la conexión después de ejecutar la consulta.
                }
        }
    }
}
