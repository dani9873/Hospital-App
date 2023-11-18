using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class AsignarMedicosForm : Form
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
        public AsignarMedicosForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }

        // Método para cargar los médicos disponibles en el formulario
        private void CargarMedicosDisponibles()
        {
            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                // Obtener los médicos disponibles
                string queryMedicos = "SELECT CódigoMedico, Nombre, Apellidos FROM Medicos";
                using (OleDbCommand cmd = new OleDbCommand(queryMedicos, conexion.con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigoMedico = (int)reader["CódigoMedico"];
                            string nombreMedico = reader["Nombre"].ToString();
                            string apellidosMedico = reader["Apellidos"].ToString();
                            cmbMedicos.Items.Add(new ComboBoxItem($"{nombreMedico} {apellidosMedico}", codigoMedico));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener los médicos disponibles. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void cmbMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiar la lista de pacientes y cargar los pacientes disponibles
            cmbPacientes.Items.Clear();

            if (cmbMedicos.SelectedItem != null)
            {
                int codigoMedico = ((ComboBoxItem)cmbMedicos.SelectedItem).Value;
                CargarPacientesDisponibles(codigoMedico);
            }
        }

        // Método para cargar los pacientes disponibles en el formulario
        private void CargarPacientesDisponibles(int codigoMedico)
        {
            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                // Obtener los pacientes asignados al médico seleccionado
                string queryPacientes = "SELECT P.CódigoPaciente, P.Nombre, P.Apellidos FROM Pacientes P INNER JOIN VisitasMedicas V ON P.CódigoPaciente = V.CódigoPaciente WHERE V.CódigoMedico = @CódigoMedico";
                using (OleDbCommand cmd = new OleDbCommand(queryPacientes, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoMedico", codigoMedico);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigoPaciente = Convert.ToInt32(reader["CódigoPaciente"]);
                            string nombrePaciente = reader["Nombre"].ToString();
                            string apellidosPaciente = reader["Apellidos"].ToString();
                            cmbPacientes.Items.Add(new ComboBoxItem($"{nombrePaciente} {apellidosPaciente}", codigoPaciente));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener los pacientes disponibles. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            // Obtener el médico y paciente seleccionados
            if (cmbMedicos.SelectedItem == null || cmbPacientes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un médico y un paciente disponibles.", "Asignar Médico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int codigoMedico = ((ComboBoxItem)cmbMedicos.SelectedItem).Value;
            int codigoPaciente = ((ComboBoxItem)cmbPacientes.SelectedItem).Value;

            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.


                // Verificar si el médico ya está asignado al paciente
                string query = "SELECT COUNT(*) FROM VisitasMedicas WHERE CódigoMedico = @CódigoMedico AND CódigoPaciente = @CódigoPaciente";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoMedico", codigoMedico);
                    cmd.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("El médico ya está asignado al paciente seleccionado.", "Asignar Médico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insertar la nueva visita médica en la base de datos
                query = "INSERT INTO VisitasMedicas (CódigoMedico, CódigoPaciente, FechaVisita) VALUES (@CódigoMedico, @CódigoPaciente, @FechaVisita)";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoMedico", codigoMedico);
                    cmd.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);
                    cmd.Parameters.AddWithValue("@FechaVisita", DateTime.Now); // Fecha y hora actual como fecha de la visita

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("El médico ha sido asignado al paciente exitosamente.", "Asignar Médico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo asignar al médico al paciente. Por favor, intente nuevamente.", "Asignar Médico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar asignar al médico al paciente. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
