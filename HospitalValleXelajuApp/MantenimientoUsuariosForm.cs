using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class MantenimientoUsuariosForm : Form
    {
        private Conexion conexion;

        public MantenimientoUsuariosForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void MantenimientoUsuariosForm_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        // Método para cargar los usuarios en el formulario
        private void CargarUsuarios()
        {
            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.


                // Obtener los usuarios registrados
                string queryUsuarios = "SELECT CódigoUsuario, NombreUsuario, NombreCompleto, Puesto, NumeroTelefono, Rol FROM Usuarios";
                using (OleDbCommand cmd = new OleDbCommand(queryUsuarios, conexion.con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigoUsuario = (int)reader["CódigoUsuario"];
                            string nombreUsuario = reader["NombreUsuario"].ToString();
                            string nombreCompleto = reader["NombreCompleto"].ToString();
                            string puesto = reader["Puesto"].ToString();
                            string numeroTelefono = reader["NumeroTelefono"].ToString();
                            string rol = reader["Rol"].ToString();
                            dgvUsuarios.Rows.Add(codigoUsuario, nombreUsuario, nombreCompleto, puesto, numeroTelefono, rol);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener los usuarios. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion(); // Cerrar la conexión después de ejecutar la consulta.
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Abrir el formulario para agregar un nuevo usuario
            /*AgregarUsuarioForm agregarUsuarioForm = new AgregarUsuarioForm();
            agregarUsuarioForm.ShowDialog();*/

            // Actualizar la lista de usuarios en el DataGridView
            dgvUsuarios.Rows.Clear();
            CargarUsuarios();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un usuario
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para editar.", "Editar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el código del usuario seleccionado
            int codigoUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);

            // Abrir el formulario para editar el usuario
            /*EditarUsuarioForm editarUsuarioForm = new EditarUsuarioForm(codigoUsuario);
            editarUsuarioForm.ShowDialog();*/

            // Actualizar la lista de usuarios en el DataGridView
            dgvUsuarios.Rows.Clear();
            CargarUsuarios();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un usuario
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Eliminar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el código del usuario seleccionado
            int codigoUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);

            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                // Eliminar el usuario de la base de datos
                string query = "DELETE FROM Usuarios WHERE CódigoUsuario = @CódigoUsuario";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoUsuario", codigoUsuario);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("El usuario ha sido eliminado exitosamente.", "Eliminar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvUsuarios.Rows.Clear();
                        CargarUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el usuario. Por favor, intente nuevamente.", "Eliminar Usuario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar el usuario. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion(); // Cerrar la conexión después de ejecutar la consulta.
            }
        }
    }
}
