using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class AsignarCamasForm : Form
    {
        private Conexion conexion;
        private Dictionary<string, string> camasDisponibles;

        public AsignarCamasForm()
        {
            InitializeComponent();
            conexion = new Conexion();
            camasDisponibles = new Dictionary<string, string>(); // Corregir el tipo de datos del diccionario
            CargarPlantasYCamasDisponibles();
            CargarPlantasDisponibles();
            CargarPacientesDisponibles();
        }
        private void CargarPlantasDisponibles()
        {
            try
            {
                conexion.AbrirConexion();
                string queryPlantas = "SELECT CódigoPlanta, Nombre FROM Plantas";
                using (OleDbCommand cmd = new OleDbCommand(queryPlantas, conexion.con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string codigoPlanta = reader["CódigoPlanta"].ToString();
                            string nombrePlanta = reader["Nombre"].ToString();
                            string plantanombre = $"Planta{codigoPlanta}";
                            cmbPlantas.Items.Add(new KeyValuePair<string, string>(codigoPlanta, plantanombre));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener las plantas disponibles. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void CargarPacientesDisponibles()
        {
            try
            {
                conexion.AbrirConexion();
                string queryPacientes = "SELECT CódigoPaciente, Nombre, Apellidos FROM Pacientes";
                using (OleDbCommand cmd = new OleDbCommand(queryPacientes, conexion.con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string codigoPaciente = reader["CódigoPaciente"].ToString();
                            string nombre = reader["Nombre"].ToString();
                            string apellidos = reader["Apellidos"].ToString();
                            string nombreapellidos = $"{nombre} {apellidos}";
                            cmbPacientes.Items.Add(new KeyValuePair<string, string>(codigoPaciente, nombreapellidos));
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
        // Método para cargar las plantas y camas disponibles en el formulario
        private void CargarPlantasYCamasDisponibles()
        {
            try
            {
                conexion.AbrirConexion();
                // Limpiar la lista de camas y cargar las camas disponibles de la planta seleccionada
                cmbCamas.Items.Clear();
                camasDisponibles.Clear(); // Limpiar el diccionario de camas disponibles

                string queryCamas = "SELECT CódigoCama, CódigoPlanta FROM Camas WHERE Estado = True";
                using (OleDbCommand cmd = new OleDbCommand(queryCamas, conexion.con))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string codigoCama = reader["CódigoCama"].ToString();
                            string codigoPlanta = reader["CódigoPlanta"].ToString();
                            string nombreCama = $"Cama {codigoCama} (Planta {codigoPlanta})";
                            cmbCamas.Items.Add(new KeyValuePair<string, string>(codigoCama, nombreCama));
                            camasDisponibles.Add(codigoCama, codigoPlanta);
                        }
                    }
                }
                cmbCamas.DisplayMember = "Value"; // Establecer la propiedad DisplayMember para mostrar el nombre de la cama en el ComboBox
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener las camas disponibles de la planta seleccionada. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            // Obtener el paciente seleccionado y la cama seleccionada
            if (cmbPlantas.SelectedItem == null || cmbCamas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una planta y una cama disponible.", "Asignar Cama a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string codigoPlanta = ((KeyValuePair<string, string>)cmbPlantas.SelectedItem).Key;
            string codigoCama = ((KeyValuePair<string, string>)cmbCamas.SelectedItem).Key;

            string codigoPaciente = ((KeyValuePair<string, string>)cmbPacientes.SelectedItem).Key; // Obtener el código del paciente seleccionado (implementar según necesidades)
            try
            {
                conexion.AbrirConexion();
                // Verificar si la cama seleccionada ya está ocupada por otro paciente
                string query = "SELECT COUNT(*) FROM Pacientes WHERE CodigoCamaAsignada = @CódigoCama";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoCama", codigoCama);
                    cmd.Parameters.AddWithValue("@CódigoPlanta", codigoPlanta);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("La cama seleccionada ya está ocupada por otro paciente.", "Asignar Cama a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Actualizar la base de datos con la asignación de cama para el paciente
                query = "UPDATE Pacientes SET CodigoCamaAsignada = @CódigoCama WHERE CódigoPaciente = @CódigoPaciente";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                {
                    cmd.Parameters.AddWithValue("@CódigoCama", codigoCama);
                    cmd.Parameters.AddWithValue("@CódigoPaciente", codigoPaciente);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("La cama ha sido asignada exitosamente al paciente.", "Asignar Cama a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo asignar la cama al paciente. Por favor, intente nuevamente.", "Asignar Cama a Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar asignar la cama al paciente. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void cmbCamas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el índice de la planta correspondiente a la cama seleccionada
            if (cmbCamas.SelectedItem != null)
            {
                string codigoCamaSeleccionada = ((KeyValuePair<string, string>)cmbCamas.SelectedItem).Key;
                if (camasDisponibles.TryGetValue(codigoCamaSeleccionada, out string codigoPlantaSeleccionada))
                {
                    // Buscar el índice de la planta en el ComboBox cmbPlantas
                    for (int i = 0; i < cmbPlantas.Items.Count; i++)
                    {
                        if (((KeyValuePair<string, string>)cmbPlantas.Items[i]).Key == codigoPlantaSeleccionada)
                        {
                            cmbPlantas.SelectedIndex = i;
                            break; // Importante agregar el break para salir del bucle una vez encontrado el índice
                        }
                    }
                }
            }
        }
    }
}
