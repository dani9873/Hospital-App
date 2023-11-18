using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class DarAltaPacienteForm : Form
    {
        private Conexion conexion;

        public DarAltaPacienteForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        private void btnDarAlta_Click(object sender, EventArgs e)
        {
            // Obtener el paciente seleccionado
            if (cmbPlantas.SelectedItem == null || cmbCamas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una planta y una cama disponible.", "Dar de Alta a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string codigoPaciente = "P1"; // Obtener el código del paciente seleccionado (implementar según necesidades)

            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                // Obtener la información de la cama actual del paciente
                string query = "SELECT CodigoCamaAsignada, CodigoPlantaAsignada FROM Pacientes WHERE CódigoPaciente = @CódigoPaciente";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int codigoCamaActual = (int)reader["CodigoCamaAsignada"];
                            int codigoPlantaActual = (int)reader["CodigoPlantaAsignada"];

                            // Actualizar la base de datos con el alta del paciente
                            string queryActualizarPaciente = "UPDATE Pacientes SET CodigoCamaAsignada = NULL, CodigoPlantaAsignada = NULL, FechaSalida = @FechaSalida WHERE CódigoPaciente = @CódigoPaciente";
                            using (OleDbCommand cmdActualizarPaciente = new OleDbCommand(queryActualizarPaciente, conexion.con))
                            {
                                cmdActualizarPaciente.Parameters.AddWithValue("@FechaSalida", DateTime.Now); // Fecha y hora actual como fecha de salida
                                cmdActualizarPaciente.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);

                                int rowsAffected = cmdActualizarPaciente.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("El paciente ha sido dado de alta exitosamente.", "Dar de Alta a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo dar de alta al paciente. Por favor, intente nuevamente.", "Dar de Alta a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar dar de alta al paciente. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
