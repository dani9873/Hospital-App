using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class RegistrarDiagnosticoForm : Form
    {
        private Conexion conexion;

        public RegistrarDiagnosticoForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener la descripción del diagnóstico ingresada por el usuario
            string descripcionDiagnostico = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(descripcionDiagnostico))
            {
                MessageBox.Show("Por favor, ingrese una descripción para el diagnóstico.", "Registro de Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.


                // Verificar si ya existe un diagnóstico con la misma descripción
                string query = "SELECT COUNT(*) FROM Diagnosticos WHERE Descripcion = @Descripcion";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", descripcionDiagnostico);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe un diagnóstico registrado con la misma descripción.", "Registro de Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insertar el nuevo diagnóstico en la base de datos
                query = "INSERT INTO Diagnosticos (Descripcion) VALUES (@Descripcion)";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", descripcionDiagnostico);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("El diagnóstico ha sido registrado exitosamente.", "Registro de Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar el diagnóstico. Por favor, intente nuevamente.", "Registro de Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar registrar el diagnóstico. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
