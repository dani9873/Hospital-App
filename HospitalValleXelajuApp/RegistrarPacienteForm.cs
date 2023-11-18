using HospitalValleXelajuApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalValleXelajuApp
{
    public partial class RegistrarPacienteForm : Form
    {
        private Conexion conexion;
        private Dictionary<string, string> camasDisponibles;
        // Diccionario para almacenar las camas disponibles
        public RegistrarPacienteForm()
        {
            InitializeComponent();
            conexion = new Conexion();
            camasDisponibles = new Dictionary<string, string>(); // Corregir el tipo de datos del diccionario
            CargarCamasDisponibles();
        }

        private void CargarCamasDisponibles()
        {
            try
            {
                conexion.AbrirConexion();

                cmbCamas.Items.Clear(); // Limpiar el ComboBox antes de cargar las camas disponibles
                camasDisponibles.Clear(); // Limpiar el diccionario de camas disponibles

                // Obtener las camas disponibles
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
                            camasDisponibles.Add(codigoCama, nombreCama);
                        }
                    }
                }

                cmbCamas.DisplayMember = "Value"; // Establecer la propiedad DisplayMember para mostrar el nombre de la cama en el ComboBox
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar obtener las camas disponibles. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion.con != null && conexion.con.State == System.Data.ConnectionState.Open)
                {
                    conexion.CerrarConexion();
                }
            }
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtDPI.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            cmbCamas.SelectedIndex = -1; // Deseleccionar cualquier cama en el ComboBox
        }



        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btn_cerrar_sesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
                // Obtener los datos ingresados por el usuario
                string codigo = txtCodigo.Text.Trim();
                string dpi = txtDPI.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellidos = txtApellidos.Text.Trim();
                DateTime fechaNacimiento = dtpFechaNacimiento.Value;

                if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(dpi) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || cmbCamas.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, complete todos los campos requeridos y seleccione una cama.", "Registro de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar la condición del código
                if (!codigo.StartsWith("P") || codigo.Substring(1).Length == 0 || !codigo.Substring(1).All(char.IsDigit))
                {
                    MessageBox.Show("El código debe comenzar con la letra 'P' seguida de números.", "Registro de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    conexion.AbrirConexion();

                    // Verificar si ya existe un paciente con el mismo código
                    string query = "SELECT COUNT(*) FROM Pacientes WHERE CódigoPaciente = @CódigoPaciente";
                    using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                    {
                        cmd.Parameters.AddWithValue("@CódigoPaciente", codigo);

                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Ya existe un paciente registrado con el mismo código.", "Registro de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Obtener la cama seleccionada
                    string codigoCama = ((KeyValuePair<string, string>)cmbCamas.SelectedItem).Key;

                    // Insertar el nuevo paciente en la base de datos
                    query = "INSERT INTO Pacientes (CódigoPaciente, DPI, Nombre, Apellidos, FechaNacimiento, CódigoCamaAsignada) VALUES (@CódigoPaciente, @DPI, @Nombre, @Apellidos, @FechaNacimiento, @CódigoCamaAsignada)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conexion.con))
                    {
                        cmd.Parameters.AddWithValue("@CódigoPaciente", codigo);
                        cmd.Parameters.AddWithValue("@DPI", dpi);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        cmd.Parameters.AddWithValue("@CódigoCamaAsignada", codigoCama);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("El paciente ha sido registrado exitosamente.", "Registro de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Actualizar el estado de la cama a no disponible
                            query = "UPDATE Camas SET Estado = False WHERE CódigoCama = @CódigoCama";
                            using (OleDbCommand updateCmd = new OleDbCommand(query, conexion.con))
                            {
                                updateCmd.Parameters.AddWithValue("@CódigoCama", codigoCama);
                                updateCmd.ExecuteNonQuery();
                            }

                            // Actualizar las camas disponibles en el ComboBox
                            CargarCamasDisponibles();

                            // Limpiar los campos después del registro exitoso
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar al paciente. Por favor, intente nuevamente.", "Registro de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar registrar al paciente. Detalles del error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.CerrarConexion();
                }
            }        
    }
}