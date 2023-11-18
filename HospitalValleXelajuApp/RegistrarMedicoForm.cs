using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class RegistrarMedicoForm : Form
    {
        private Conexion conexion;

        public RegistrarMedicoForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener los datos ingresados por el usuario
            string numeroColegiado = txtNumeroColegiado.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellidos = txtApellidos.Text.Trim();
            string especialidad = txtEspecialidad.Text.Trim();

            if (string.IsNullOrEmpty(numeroColegiado) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(especialidad))
            {
                MessageBox.Show("Por favor, complete todos los campos requeridos.", "Registro de Médico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.


                // Verificar si ya existe un médico con el mismo número de colegiado
                string query = "SELECT COUNT(*) FROM Medicos WHERE NumeroColegiado = @NumeroColegiado";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@NumeroColegiado", numeroColegiado);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe un médico registrado con el mismo número de colegiado.", "Registro de Médico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insertar el nuevo médico en la base de datos
                query = "INSERT INTO Medicos (NumeroColegiado, Nombre, Apellidos, Especialidad) VALUES (@NumeroColegiado, @Nombre, @Apellidos, @Especialidad)";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@NumeroColegiado", numeroColegiado);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@Especialidad", especialidad);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("El médico ha sido registrado exitosamente.", "Registro de Médico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar al médico. Por favor, intente nuevamente.", "Registro de Médico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar registrar al médico. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion(); // Cerrar la conexión después de ejecutar la consulta.
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
