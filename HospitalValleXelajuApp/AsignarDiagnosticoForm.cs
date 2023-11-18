using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class AsignarDiagnosticoForm : Form
    {
        private Conexion conexion;

        public AsignarDiagnosticoForm()
        {
            InitializeComponent();
            conexion = new Conexion();
        }
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
            // Método para cargar los diagnósticos disponibles en el formulario
            private void CargarDiagnosticosDisponibles()
        {
            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                // Obtener los diagnósticos disponibles
                string queryDiagnosticos = "SELECT CódigoDiagnostico, Descripcion FROM Diagnosticos";
                using (OleDbCommand cmd = new OleDbCommand(queryDiagnosticos, conexion.con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codigoDiagnostico = (int)reader["CódigoDiagnostico"];
                            string descripcionDiagnostico = reader["Descripcion"].ToString();
                            cmbDiagnosticos.Items.Add(new ComboBoxItem(descripcionDiagnostico, codigoDiagnostico));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener los diagnósticos disponibles. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void cmbDiagnosticos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiar la lista de pacientes y cargar los pacientes disponibles
            cmbPacientes.Items.Clear();

            if (cmbDiagnosticos.SelectedItem != null)
            {
                int codigoDiagnostico = ((ComboBoxItem)cmbDiagnosticos.SelectedItem).Value;
                CargarPacientesDisponibles(codigoDiagnostico);
            }
        }

        // Método para cargar los pacientes disponibles en el formulario
        private void CargarPacientesDisponibles(int codigoDiagnostico)
        {
            try
            {
                conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                // Obtener los pacientes con el diagnóstico seleccionado
                string queryPacientes = "SELECT P.CódigoPaciente, P.Nombre, P.Apellidos FROM Pacientes P INNER JOIN PacientesDiagnosticos PD ON P.CódigoPaciente = PD.CódigoPaciente WHERE PD.CódigoDiagnostico = @CódigoDiagnostico";
                using (OleDbCommand cmd = new OleDbCommand(queryPacientes, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoDiagnostico", codigoDiagnostico);

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
        
                // Obtener el diagnóstico y paciente seleccionados
                if (cmbDiagnosticos.SelectedItem == null || cmbPacientes.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione un diagnóstico y un paciente disponibles.", "Asignar Diagnóstico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int codigoDiagnostico = ((ComboBoxItem)cmbDiagnosticos.SelectedItem).Value;
                int codigoPaciente = ((ComboBoxItem)cmbPacientes.SelectedItem).Value;

                try
                {
                    conexion.AbrirConexion(); // Abrir la conexión antes de ejecutar la consulta.

                    // Verificar si el diagnóstico ya está asignado al paciente
                    string query = "SELECT COUNT(*) FROM PacientesDiagnosticos WHERE CódigoDiagnostico = @CódigoDiagnostico AND CódigoPaciente = @CódigoPaciente";
                    using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                    {
                        cmd.Parameters.AddWithValue("@CódigoDiagnostico", codigoDiagnostico);
                        cmd.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);

                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("El diagnóstico ya está asignado al paciente seleccionado.", "Asignar Diagnóstico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insertar el nuevo diagnóstico del paciente en la base de datos
                    query = "INSERT INTO PacientesDiagnosticos (CódigoPaciente, CódigoDiagnostico) VALUES (@CódigoPaciente, @CódigoDiagnostico)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                    {
                        cmd.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);
                        cmd.Parameters.AddWithValue("@CódigoDiagnostico", codigoDiagnostico);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El diagnóstico ha sido asignado al paciente exitosamente.", "Asignar Diagnóstico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo asignar el diagnóstico al paciente. Por favor, intente nuevamente.", "Asignar Diagnóstico a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar asignar el diagnóstico al paciente. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
