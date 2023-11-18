using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace HospitalValleXelajuApp
{
    public partial class TrasladarPacientesForm : Form
    {
        private Conexion conexion;
        public class ComboBoxItem
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string name, int value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Name; // Define cómo se mostrará el nombre en el ComboBox
            }
        }
        public TrasladarPacientesForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        // Método para cargar las camas disponibles en la planta seleccionada en el formulario
        private void CargarCamasDisponibles(int codigoPlanta)
        {
            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                string queryCamas = "SELECT CódigoCama FROM Camas WHERE CódigoPlanta = @CódigoPlanta AND Estado = 'Disponible'";
                using (OleDbCommand cmd = new OleDbCommand(queryCamas, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoPlanta", codigoPlanta);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigoCama = (int)reader["CódigoCama"];
                            cmbCamas.Items.Add(codigoCama);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener las camas disponibles de la planta seleccionada. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion(); // Cerrar la conexión después de ejecutar la consulta.
            }
        }

        private void cmbPlantas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiar la lista de camas y cargar las camas disponibles de la planta seleccionada
            cmbCamas.Items.Clear();

            if (cmbPlantas.SelectedItem != null)
            {
                int codigoPlanta = ((ComboBoxItem)cmbPlantas.SelectedItem).Value;
                CargarCamasDisponibles(codigoPlanta);
            }
        }

        private void btnTrasladar_Click(object sender, EventArgs e)
        {
            // Obtener el paciente seleccionado, la cama seleccionada y la nueva planta seleccionada
            if (cmbPlantas.SelectedItem == null || cmbCamas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una planta y una cama disponible.", "Trasladar Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int codigoPlantaNueva = ((ComboBoxItem)cmbPlantas.SelectedItem).Value;
            int codigoCamaNueva = (int)cmbCamas.SelectedItem;

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

                            // Verificar si la cama nueva seleccionada ya está ocupada por otro paciente
                            string queryCamasNueva = "SELECT COUNT(*) FROM Pacientes WHERE CodigoCamaAsignada = @CódigoCama AND CodigoPlantaAsignada = @CódigoPlanta";
                            using (OleDbCommand cmdCamasNueva = new OleDbCommand(queryCamasNueva, conexion.con))
                            {
                                cmdCamasNueva.Parameters.AddWithValue("@CódigoCama", codigoCamaNueva);
                                cmdCamasNueva.Parameters.AddWithValue("@CódigoPlanta", codigoPlantaNueva);

                                int count = (int)cmdCamasNueva.ExecuteScalar();
                                if (count > 0)
                                {
                                    MessageBox.Show("La cama nueva seleccionada ya está ocupada por otro paciente.", "Trasladar Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Actualizar la base de datos con el traslado del paciente
                            string queryActualizarPaciente = "UPDATE Pacientes SET CodigoCamaAsignada = @CódigoCamaNueva, CodigoPlantaAsignada = @CódigoPlantaNueva WHERE CódigoPaciente = @CódigoPaciente";
                            using (OleDbCommand cmdActualizarPaciente = new OleDbCommand(queryActualizarPaciente, conexion.con))
                            {
                                cmdActualizarPaciente.Parameters.AddWithValue("@CódigoCamaNueva", codigoCamaNueva);
                                cmdActualizarPaciente.Parameters.AddWithValue("@CódigoPlantaNueva", codigoPlantaNueva);
                                cmdActualizarPaciente.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);

                                int rowsAffected = cmdActualizarPaciente.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("El paciente ha sido trasladado exitosamente.", "Trasladar Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo trasladar al paciente. Por favor, intente nuevamente.", "Trasladar Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar trasladar al paciente. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
